using System;
using System.Collections.Generic;
using System.Linq;

namespace day_3
{
    class Program
    {
        private static void Main(string[] args)
        {
            var wireCommands = GetCommandsFromFile();
            var wire1 = GetVectors(wireCommands[0]);
            var wire2 = GetVectors(wireCommands[1]);

            List<Tuple<decimal,decimal>> decimalersections = GetIntersections(wire1, wire2);
            List<decimal> manhattanDistances = GetManhattanDistances(decimalersections);
            Console.WriteLine(manhattanDistances.Min());
        }

        private static List<decimal> GetManhattanDistances(List<Tuple<decimal, decimal>> decimalersections)
        {
            var distances = new List<decimal>();
            foreach(var decimalersection in decimalersections)
            {
                var distance = Math.Abs(decimalersection.Item1) + Math.Abs(decimalersection.Item2);
                if (distance > 0)
                    distances.Add(distance);
            }
            return distances;
        }

        private static List<Tuple<decimal, decimal>> GetIntersections(List<Tuple<decimal, decimal>> wire1, List<Tuple<decimal, decimal>> wire2)
        {
            var decimalersections = new HashSet<Tuple<decimal, decimal>>();
            for (int i=1; i < wire1.Count; i++)
            {
                for(int j=1; j < wire2.Count; j++)
                {
                    var p2 = wire1[i];
                    var p = wire1[i - 1];
                    var q2 = wire2[j];
                    var q = wire2[j - 1];


                    var decimalersection = GetIntersection(p, p2, q, q2);
                    if (decimalersection != null)
                        decimalersections.Add(decimalersection);
                }
            }
            return decimalersections.ToList(); ;
        }

        private static Tuple<decimal,decimal> GetIntersection(Tuple<decimal, decimal> vector1start, Tuple<decimal, decimal> vector1end, Tuple<decimal, decimal> vector2start, Tuple<decimal, decimal> vector2end)
        {
            var delta1x = vector1end.Item1 - vector1start.Item1;
            var delta1y = vector1end.Item2 - vector1start.Item2;
            var delta2x = vector2end.Item1 - vector2start.Item1;
            var delta2y = vector2end.Item2 - vector2start.Item2;

            var determinant = (delta1x * delta2y) - (delta1y * delta2x);
            if (determinant == 0) return null;

            var ab = ((vector2start.Item1 - vector1start.Item1) * delta2y - (vector2start.Item2 - vector1start.Item2) * delta2x) / determinant;
            var cd = ((vector2start.Item1 - vector1start.Item1) * delta1y - (vector2start.Item2 - vector1start.Item2) * delta1x) / determinant;

            if (ab < 0 || ab > 1)
                return null;

            if (cd < 0 || cd > 1)
                return null;

            var decimalersectionX = vector1start.Item1 + ab * delta1x;
            var decimalersectionY = vector1start.Item2 + ab * delta1y;

            return new Tuple<decimal, decimal>(decimalersectionX, decimalersectionY);

        }

        private static decimal Determinant(Tuple<decimal, decimal> a, Tuple<decimal, decimal> b)
        {
            return a.Item1 * b.Item2 - a.Item2 * b.Item1;
        }

        private static decimal GetSkalärprodukt(Tuple<decimal,decimal> a, Tuple<decimal,decimal> b)
        {
            return a.Item1 * b.Item1 + a.Item2 * b.Item2;
        }
        private static Tuple<decimal,decimal> GetSkalärprodukt(Tuple<decimal, decimal> a, decimal b)
        {
            return new Tuple<decimal,decimal> (a.Item1 * b,  a.Item2 * b);
        }

        private static Tuple<decimal,decimal> Add(Tuple<decimal, decimal> a, Tuple<decimal, decimal> b)
        {
            return new Tuple<decimal, decimal>(a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        private static Tuple<decimal, decimal> Subtract(Tuple<decimal, decimal> a, Tuple<decimal, decimal> b)
        {
            return new Tuple<decimal, decimal>(a.Item1 - b.Item1, a.Item2 - b.Item2);
        }

        private static List<Tuple<decimal,decimal>> GetVectors(List<Tuple<Direction, decimal>> commands)
        {
            return CoordinatesFactory.GetCoordinates(commands);
        }

        private static List<List<Tuple<Direction, decimal>>> GetCommandsFromFile()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var commands = new List<Tuple<Direction, decimal>>();
            string line;
            var wires = new List<List<Tuple<Direction, decimal>>>();

            while ((line = file.ReadLine()) != null)
            {
                commands = line.Split(',').Select(c => new Tuple<Direction, decimal>((Direction)c[0], decimal.Parse(c.Substring(1)))).ToList();
                wires.Add(commands);
            }
            return wires;
        }
    }
}
