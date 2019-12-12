namespace TestRigging.SimpleIk
{

    public partial class SimpleIk
    {

        class Work
        {

            internal static Work Idle { get; } = new Work(0.5f, State.Idle);

            internal static Work SetTarget { get; } = new Work(0.5f, State.SetTarget);

            internal static Work Run { get; } = new Work(3f, State.Run);

            internal State State { get; }

            float _duration { get; }

            float _startedAt;

            Work(float duration, State state)
            {
                _duration = duration;
                State = state;
            }

            public void Clear()
            {
                _startedAt = 0f;
            }

            public void Start(float time)
            {
                _startedAt = time;
            }

            public bool DidFinish(float time)
            {
                return time > _startedAt + _duration;
            }

        }

    }

}
