using System.Collections.Generic;
using System.Linq;
using TestRigging.SimpleIk.Internal;
using UnityEngine;
using UnityEngine.Assertions;

namespace TestRigging.SimpleIk
{

    public class SimpleIk
        : MonoBehaviour
    {

        public List<Bone> Bones;

        public Transform Target;

        Vector3 LastTargetPosition;

        Cycle<Work> _works { get; } = new Cycle<Work>(
            new[]
            {
                Work.Idle,
                Work.SetTarget,
                Work.Run,
            }
        );

        void Awake()
        {
            Assert.IsNotNull(Bones, "Bones != null");
            Assert.IsTrue(Bones.Any(), "Bones.Any()");
        }

        void Start()
        {
            _works.Value.Start(Time.time);
        }

        void Update()
        {
            var current = _works.Value;
            var didFinish = current.DidFinish(Time.time);

            if (didFinish)
            {
                current.Clear();
                var next = _works.GoNext();
                next.Start(Time.time);

                Debug.Log(
                    $"{Time.time}: {current.State} -> {next.State}",
                    gameObject
                );
            }
        }

    }

}
