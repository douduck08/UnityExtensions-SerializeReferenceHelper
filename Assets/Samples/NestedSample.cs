using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestedSample : MonoBehaviour {

    [System.Serializable]
    public class BaseClass {
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
    public class NestClass {
        [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass itemA;
        [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass itemB;
    }

    [System.Serializable]
    public struct NestStruct {
        [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass itemA;
        [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public BaseClass itemB;
    }

    [Header ("Nested in Class")]
    public NestClass nestItemA;
    public List<NestClass> nestListA;
    public NestClass[] nestArrayA;

    [Header ("Nested in Struct")]
    public NestStruct nestItemB;
    public List<NestStruct> nestListB;
    public NestStruct[] nestArrayB;

    [Header ("List in List")]
    [SerializeReference, ReferenceTypeSelector (typeof (BaseClass))] public List<List<BaseClass>> nestList;
}
