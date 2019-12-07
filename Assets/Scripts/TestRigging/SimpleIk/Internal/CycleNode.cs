namespace TestRigging.SimpleIk.Internal
{

    class CycleNode<T>
    {

        internal T Next { get; }

        internal T Value { get; }

        internal CycleNode(T value, T next)
        {
            Value = value;
            Next = next;
        }

    }

}
