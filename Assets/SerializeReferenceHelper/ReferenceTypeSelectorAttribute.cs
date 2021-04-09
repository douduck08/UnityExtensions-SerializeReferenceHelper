using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage (AttributeTargets.Field)]
public class ReferenceTypeSelectorAttribute : PropertyAttribute {
    public Type baseType;
    public ReferenceTypeSelectorAttribute (Type baseType) {
        this.baseType = baseType;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer (typeof (ReferenceTypeSelectorAttribute))]
public class ReferenceTypeSelectorDrawer : PropertyDrawer {

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
        var height = EditorGUI.GetPropertyHeight (property, label, true);
        if (property.hasVisibleChildren && property.isExpanded) {
            var targetObject = SerializedPropertyUtil.GetTargetObject (property);
            if (targetObject != null) {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }
        return height;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        var typeSelector = attribute as ReferenceTypeSelectorAttribute;
        var targetObject = SerializedPropertyUtil.GetTargetObject (property);

        if (targetObject != null && property.hasVisibleChildren) {
            ShowFoldout (position, property, label, typeSelector.baseType, targetObject);
        } else {
            ShowInline (position, property, label, typeSelector.baseType, targetObject);
        }
    }

    void ShowInline (Rect position, SerializedProperty property, GUIContent label, Type baseType, object targetObject) {
        var labalPosition = new Rect (position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        var buttonPosition = new Rect (position.x + EditorGUIUtility.labelWidth + 2, position.y, position.width - EditorGUIUtility.labelWidth - 2, position.height);
        EditorGUI.LabelField (labalPosition, label);
        ShowInstanceMenu (buttonPosition, property, baseType, targetObject);
    }

    void ShowFoldout (Rect position, SerializedProperty property, GUIContent label, Type baseType, object targetObject) {
        EditorGUI.PropertyField (position, property, label, true);
        if (property.isExpanded) {
            var buttonPosition = new Rect (position.x, position.y + position.height - EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.indentLevel += 1;
            buttonPosition = EditorGUI.IndentedRect (buttonPosition);
            EditorGUI.indentLevel -= 1;
            ShowInstanceMenu (buttonPosition, property, baseType, targetObject);
        }
    }

    void ShowInstanceMenu (Rect position, SerializedProperty property, Type baseType, object targetObject) {
        var isAbstract = baseType.IsAbstract;
        var subTypes = baseType.Assembly.GetTypes ().Where (t => baseType.IsAssignableFrom (t) && t != baseType && !t.IsAbstract);
        var buttonText = (targetObject == null) ? "null" : targetObject.GetType ().Name;

        if (GUI.Button (position, buttonText)) {
            var context = new GenericMenu ();
            context.AddItem (new GUIContent ("Set null"), false, () => {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties ();
            });

            if (isAbstract) {
                context.AddDisabledItem (new GUIContent (baseType.Name));
            } else {
                context.AddItem (new GUIContent (baseType.Name), false, () => {
                    property.managedReferenceValue = Activator.CreateInstance (baseType);
                    property.serializedObject.ApplyModifiedProperties ();
                });
            }
            context.AddSeparator ("");

            foreach (var subType in subTypes) {
                context.AddItem (new GUIContent (subType.Name), false, () => {
                    property.managedReferenceValue = Activator.CreateInstance (subType);
                    property.serializedObject.ApplyModifiedProperties ();
                });
            }

            var pos = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight, 0, 0);
            context.DropDown (pos);
        }
    }
}
#endif
