using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSample : MonoBehaviour {
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

    [System.Serializable] // cannot serialize in Unity
    public class ClassD<T> : BaseClass {
        public T memberInD;
    }

    [System.Serializable]
    public class ClassE : ClassC {
        [Range (1f, 10f)] public float memberInE;
    }

    [System.Serializable]
    public class ClassF : ClassD<Vector3> { }

    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass item;
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public List<List<BaseClass>> listList; // cannot serialize in Unity
}
