using System.Collections.Generic;
using System.Linq;

namespace day_6
{
    public class OrbitCounter
    {
        private Dictionary<string, Orbit> _orbitTree { get; set; }

        public OrbitCounter(Dictionary<string, Orbit> orbitTree)
        {
            _orbitTree = orbitTree;
        }

        public int GetNumberOfOrbitsTo(string rootOrbit)
        {
            int numberOfOrbits = 0;
            foreach (var orbitKeyValue in _orbitTree)
            {
                numberOfOrbits += CountOrbits(rootOrbit, orbitKeyValue.Value);
            }

            return numberOfOrbits;
        }

        internal string CommonOrbit(Orbit fromOrbit, Orbit toOrbit)
        {
            List<string> orbitWayFrom = GetOrbitWay(fromOrbit);

            List<string> orbitWayTo = GetOrbitWay(toOrbit);

            var common = orbitWayFrom.Intersect(orbitWayTo);
            return common.First();
        }

        private List<string> GetOrbitWay(Orbit orbit)
        {
            List<string> orbitWay = new List<string>();

            NextOrbit(orbitWay, orbit);

            return orbitWay;

        }

        private void NextOrbit(List<string> orbitWay, Orbit orbit)
        {
            if (orbit.Name == "COM")
            {
                orbitWay.Add(orbit.Name);
                return;
            }

            orbitWay.Add(orbit.Name);
            NextOrbit(orbitWay, _orbitTree[orbit.OrbitsAround]);
        }

        internal int CountOrbits(string rootOrbit, Orbit orbit)
        {
            if (orbit.Name == rootOrbit)
                return 0;
            return 1 + CountOrbits(rootOrbit, _orbitTree[orbit.OrbitsAround]);

        }


    }
}
