using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractClassSample : MonoBehaviour {
    [System.Serializable]
    public abstract class BaseClass {
        public int memberInBase;
    }

    [System.Serializable]
    public class ClassA : BaseClass {
        public Color memberInA;
    }

    [System.Serializable]
    public class ClassB : BaseClass {
        public GameObject memberInB;
    }

    [System.Serializable]
    public class ClassC : BaseClass {
        [Range (0f, 1f)] public float memberInC;
    }

    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass item;
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public List<BaseClass> list;
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass[] array;
}
