using UnityEngine;
using UnityEngine.Assertions;

namespace TestRigging.SimpleIk
{

    public class Bone
        : MonoBehaviour
    {

        public Transform Start;

        public Transform End;

        public Bone Parent;

        public Bone Child;

        void Awake()
        {
//            Assert.IsNotNull(Parent, "Parent != null");
            Assert.IsNotNull(Start, "Start != null");
            Assert.IsNotNull(End, "End != null");
        }

        public void MoveToTouch(Vector3 target)
        {
            var delta = target - End.position;
            transform.position = transform.position + delta;
        }

        public void GoBack()
        {
            var parentPosition =
                Parent == null
                    ? Vector3.zero
                    : Parent.End.position;

            var delta = parentPosition - Start.position;
            transform.position = transform.position + delta;
        }

        public void LookAt(Vector3 target)
        {
            transform.rotation = Quaternion.LookRotation(
                target - Start.position
            );
        }

    }

}
