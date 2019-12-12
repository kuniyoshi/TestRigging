using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TestRigging.SimpleIk
{

    public partial class SimpleIk
    {

        class IkWorker
        {

            enum IkState
            {

                First,

                Second,

                Last,

            }

            Queue<Bone> Bones { get; }

            Bone Last { get; }

            Vector3 Target { get; }

            bool _isDoneFirst;

            Bone _parent;

            IkState _state;

            Stack<Bone> BackingStack { get; }

            internal IkWorker(IReadOnlyCollection<Bone> bones, Vector3 target)
            {
                Assert.IsTrue(bones.Count > 1, "bones.Count > 1");

                Last = bones.Last();
                Bones = new Queue<Bone>(bones.Reverse().Skip(1));
                Target = target;
                BackingStack = new Stack<Bone>();
            }

            internal void Step()
            {
                switch (_state)
                {
                    case IkState.First:
                        StepFirst();

                        break;

                    case IkState.Second:
                        StepSecond();

                        break;

                    case IkState.Last:
                        StepLast();

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            void StepFirst()
            {
                Debug.Log($"{nameof(StepFirst)}: {Last.gameObject}", Last);
                Last.LookAt(Target);

                Last.MoveToTouch(Target);

                _parent = Last;
                BackingStack.Push(Last);
                _state = IkState.Second;
            }

            void StepLast()
            {
                if (!BackingStack.Any())
                {
                    return;
                }

                var bone = BackingStack.Pop();
                Debug.Log($"{nameof(StepLast)}: {bone.gameObject}", bone);
                bone.GoBack();
            }

            void StepSecond()
            {
                Assert.IsTrue(Bones.Any(), "Bones.Any()");
                var bone = Bones.Dequeue();
                Debug.Log($"{nameof(StepSecond)}: {bone.gameObject} looks {_parent.gameObject}", bone);

                bone.LookAt(_parent.Start.position);
                bone.MoveToTouch(_parent.Start.position);
                _parent = bone;
                BackingStack.Push(bone);

                if (!Bones.Any())
                {
                    _state = IkState.Last;
                }
            }

        }

    }

}
