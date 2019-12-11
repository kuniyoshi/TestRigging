using System;
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

        IkWorker _worker;

        Stepper _stepper { get; } = new Stepper(0.1f);

        List<Bone> _workingBones;

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
            Assert.IsNotNull(Target, "Target != null");

            _workingBones = Bones
                .Where(bone => bone != null)
                .ToList();

            Assert.IsTrue(_workingBones.Any(), "_workingBones.Any()");
        }

        void Start()
        {
            _works.Value.Start(Time.time);
        }

        void Update()
        {
            UpdateState();

            switch (_works.Value.State)
            {
                case State.Idle:
                    // do nothing
                    break;

                case State.SetTarget:
                    LastTargetPosition = Target.position;

                    break;

                case State.Run:
                    WorkIk();

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void UpdateState()
        {
            var current = _works.Value;
            var didFinish = current.DidFinish(Time.time);

            if (!didFinish)
            {
                return;
            }

            current.Clear();
            var next = _works.GoNext();
            next.Start(Time.time);

            Debug.Log(
                $"{Time.time}: {current.State} -> {next.State}",
                gameObject
            );

            if (next.State == State.Run)
            {
                _stepper.Clear();
                _stepper.Start(Time.time);

                _worker = new IkWorker(_workingBones, LastTargetPosition);
            }
        }

        void WorkIk()
        {
            var isEnough = _stepper.IsEnough(Time.time);

            if (!isEnough)
            {
                return;
            }

            _stepper.Start(Time.time);

            _worker.Step();

        }

    }

}
