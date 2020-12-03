using System;
using System.Collections.Generic;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var orbitsList = GetOrbits();
            Dictionary<string, Orbit> orbitTree = GetOrbitTree(orbitsList);

            int orbitCountChecksum = GetNumberOfOrbits(orbitTree);
            int orbitCountBetweenYouAndSanta = GetOrbitCountBetween("YOU", "SAN", orbitTree);

            Console.WriteLine(orbitCountBetweenYouAndSanta);
        }

        private static int GetOrbitCountBetween(string from, string to, Dictionary<string, Orbit> orbitTree)
        {
            var fromOrbit = orbitTree[from];
            var toOrbit = orbitTree[to];

            int intersectingOrbit = GetCommonOrbit(orbitTree, fromOrbit, toOrbit);
            return intersectingOrbit;
        }

        private static int GetCommonOrbit(Dictionary<string, Orbit> orbitTree, Orbit fromOrbit, Orbit toOrbit)
        {
            var counter = new OrbitCounter(orbitTree);
            var commonOrbit = counter.CommonOrbit(fromOrbit, toOrbit);

            var numberfromToCommon = counter.CountOrbits(commonOrbit, orbitTree[fromOrbit.OrbitsAround]);

            var numberToToCommon = counter.CountOrbits(commonOrbit, orbitTree[toOrbit.OrbitsAround]);

            return numberfromToCommon + numberToToCommon;
        }



        private static int GetNumberOfOrbits(Dictionary<string, Orbit> orbitTree)
        {
            var counter = new OrbitCounter(orbitTree);
            return counter.GetNumberOfOrbitsTo("COM");
        }


        private static Dictionary<string, Orbit> GetOrbitTree(List<Tuple<string, string>> orbitsList)
        {
            var orbitTree = new Dictionary<string, Orbit>();
            foreach (var orbit in orbitsList)
            {
                if (orbit.Item1 == "COM")
                {
                    orbitTree.Add(orbit.Item1, new Orbit
                    {
                        Name = orbit.Item1,
                    });
                }
                var orbitNode = new Orbit
                {
                    Name = orbit.Item2,
                    OrbitsAround = orbit.Item1
                };
                orbitTree.Add(orbitNode.Name, orbitNode);
            }

            return orbitTree;
        }

        private static List<Tuple<string, string>> GetOrbits()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var orbits = new List<Tuple<string, string>>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var input = line.Split(')');
                orbits.Add(new Tuple<string, string>(input[0], input[1]));
            }
            return orbits;
        }
    }
}
