using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSample : MonoBehaviour {
    [System.Serializable]
    public class BaseClass {
        public int memberInBase;
    }

    [System.Serializable]
    public class ClassA : BaseClass {
        public Color memberInA;
    }

    [System.Serializable] // cannot serialize in Unity
    public class ClassB<T> : BaseClass {
        public T memberInD;
    }

    [System.Serializable]
    public class ClassC : ClassA {
        [Range (1f, 10f)] public float memberInE;
    }

    [System.Serializable]
    public class ClassD : ClassB<Vector3> { }

    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass item;
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass), "FilterTest")] public BaseClass filterdItem;

    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public List<List<BaseClass>> listList; // cannot serialize in Unity
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public List<BaseClass> list;
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass)), ReorderableList] public List<BaseClass> reorderableList;

    static IEnumerable<System.Type> FilterTest () {
        yield return typeof (BaseClass); // not subtype of BaseClass, will filter out.
        yield return typeof (GameObject); // not subtype of BaseClass, will filter out.
        yield return typeof (ClassA);
        yield return typeof (ClassB<int>); // cannot serialize in Unity, will filter out.
        yield return typeof (ClassC);
    }
}
