using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSample : MonoBehaviour {

    public interface IBaseInterfaceA { }
    public interface IBaseInterfaceB { }

    [System.Serializable]
    public class ClassA : IBaseInterfaceA {
        public Color memberInA;
    }

    [System.Serializable]
    public class ClassB : IBaseInterfaceA, IBaseInterfaceB {
        public GameObject memberInB;
    }

    [System.Serializable]
    public class ClassC : IBaseInterfaceB {
        [Range (0f, 1f)] public float memberInC;
    }

    [SerializeReference, ReferenceTypeSelector (typeof (IBaseInterfaceA))] public IBaseInterfaceA itemA;
    [SerializeReference, ReferenceTypeSelector (typeof (IBaseInterfaceB))] public IBaseInterfaceB itemB;
    [SerializeReference, ReferenceTypeSelector (typeof (IBaseInterfaceA))] public List<IBaseInterfaceA> list;
    [SerializeReference, ReferenceTypeSelector (typeof (IBaseInterfaceA))] public IBaseInterfaceA[] array;
}
