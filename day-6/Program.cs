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
            Console.WriteLine(orbitCountChecksum);
        }

        private static int GetNumberOfOrbits(Dictionary<string, Orbit> orbitTree)
        {
            int numberOfOrbits = 0;
            foreach (var orbitKeyValue in orbitTree)
            {
                numberOfOrbits += CountOrbits(orbitKeyValue.Value, orbitTree);
            }


            return numberOfOrbits;

        }

        private static int CountOrbits(Orbit orbit, Dictionary<string, Orbit> orbitTree)
        {
            if (orbit.OrbitsAround == null)
                return 0;
            return 1 + CountOrbits(orbitTree[orbit.OrbitsAround], orbitTree);

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
