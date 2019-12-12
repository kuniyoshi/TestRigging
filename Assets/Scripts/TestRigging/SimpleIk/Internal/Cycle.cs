using System.Collections.Generic;
using System.Linq;

namespace TestRigging.SimpleIk
{

    public partial class SimpleIk
    {

        class Cycle<T>
            where T : class
        {

            int _currentIndex;

            List<T> _nodes { get; }

            internal T Value =>
                !_nodes.Any()
                    ? null
                    : _nodes[_currentIndex];

            internal T Next =>
                !_nodes.Any()
                    ? null
                    : _nodes[GetNextIndex()];

            internal Cycle(IEnumerable<T> nodes)
            {
                _nodes = new List<T>(nodes);
            }

            internal T GoNext()
            {
                _currentIndex = GetNextIndex();

                return Value;
            }

            int GetNextIndex()
            {
                var candidate = _currentIndex + 1;

                if (candidate < _nodes.Count)
                {
                    return candidate;
                }

                return 0;
            }

        }

    }

}
