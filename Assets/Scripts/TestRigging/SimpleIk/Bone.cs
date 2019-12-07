using UnityEngine;
using UnityEngine.Assertions;

namespace TestRigging.SimpleIk
{

    public class Bone
        : MonoBehaviour
    {

        public Transform Start;

        public Transform End;

        void Awake()
        {
            Assert.IsNotNull(Start, "Start != null");
            Assert.IsNotNull(End, "End != null");
        }

    }

}
