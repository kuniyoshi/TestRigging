namespace TestRigging.SimpleIk.Internal
{

    class Stepper
    {

        float _singleStepDuration { get; }

        float? _startedAt;

        internal Stepper(float singleStepDuration)
        {
            _singleStepDuration = singleStepDuration;
        }

        internal void Clear()
        {
            _startedAt = null;
        }

        internal bool IsEnough(float time)
        {
            return _startedAt.HasValue
                   && time > _startedAt.Value + _singleStepDuration;
        }

        internal void Start(float time)
        {
            _startedAt = time;
        }

    }

}
