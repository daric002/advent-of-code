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

            var intersections = GetIntersections(wire1, wire2);

            var shortestPath = GetWirePaths(intersections, wire1, wire2);

            var manhattanDistances = GetManhattanDistances(intersections);
            Console.WriteLine(shortestPath.Min());
        }

        private static List<int> GetWirePaths(List<Tuple<int, int>> intersections, List<Tuple<int, int>> wire1, List<Tuple<int, int>> wire2)
        {
            var paths = new List<int>();

            foreach (var intersection in intersections)
            {
                int pathForWire1 = GetPath(intersection, wire1);
                var pathForWire2 = GetPath(intersection, wire2);

                paths.Add(pathForWire1 + pathForWire2);
            }

            return paths;
        }

        private static int GetPath(Tuple<int,int> intersection, List<Tuple<int, int>> wire)
        {
            int path = 0;
            for (int i = 1; i < wire.Count; i++)
            {
                //x ligger mellan två x koordinater eller på linjen och mellan de båda y koordinaterna
                if ((wire[i].Item1 >= intersection.Item1 && wire[i - 1].Item1 <= intersection.Item1 &&
                    wire[i].Item2 >= intersection.Item2 && wire[i - 1].Item2 <= intersection.Item2) ||
                    (wire[i].Item1 <= intersection.Item1 && wire[i - 1].Item1 >= intersection.Item1 &&
                    wire[i].Item2 <= intersection.Item2 && wire[i - 1].Item2 >= intersection.Item2))
                {
                    for(int j = i; j > 0; j--)
                    {
                        path += Math.Abs(wire[j].Item2 - wire[j - 1].Item2) + Math.Abs(wire[j].Item1 - wire[j - 1].Item1);
                    }
                    path -= (Math.Abs(wire[i].Item1 - intersection.Item1)) + (Math.Abs(wire[i].Item2 - intersection.Item2));
                    break;
                } 
            }

            return path;
        }

        private static List<int> GetManhattanDistances(List<Tuple<int, int>> intersections)
        {
            var distances = new List<int>();
            foreach(var intersection in intersections)
            {
                var distance = Math.Abs(intersection.Item1) + Math.Abs(intersection.Item2);
                if (distance > 0)
                    distances.Add(distance);
            }
            return distances;
        }

        private static List<Tuple<int, int>> GetIntersections(List<Tuple<int, int>> wire1, List<Tuple<int, int>> wire2)
        {
            var intersections = new HashSet<Tuple<int, int>>();
            for (int i=1; i < wire1.Count; i++)
            {
                for(int j=1; j < wire2.Count; j++)
                {
                    var p2 = wire1[i];
                    var p = wire1[i - 1];
                    var q2 = wire2[j];
                    var q = wire2[j - 1];


                    var intersection = GetIntersection(p, p2, q, q2);
                    if (intersection != null)
                        intersections.Add(intersection);
                }
            }
            return intersections.ToList(); ;
        }

        private static Tuple<int, int> GetIntersection(Tuple<int, int> vector1start, Tuple<int, int> vector1end, Tuple<int, int> vector2start, Tuple<int, int> vector2end)
        {
            var delta1x = vector1end.Item1 - vector1start.Item1;
            var delta1y = vector1end.Item2 - vector1start.Item2;
            var delta2x = vector2end.Item1 - vector2start.Item1;
            var delta2y = vector2end.Item2 - vector2start.Item2;

            var determinant = (delta1x * delta2y) - (delta1y * delta2x);
            if (determinant == 0) return null;

            var ab = (float)((vector2start.Item1 - vector1start.Item1) * delta2y - (vector2start.Item2 - vector1start.Item2) * delta2x) / (float)determinant;
            var cd = (float)((vector2start.Item1 - vector1start.Item1) * delta1y - (vector2start.Item2 - vector1start.Item2) * delta1x) / (float)determinant;

            if (ab < 0 || ab > 1)
                return null;

            if (cd < 0 || cd > 1)
                return null;

            var intersectionX = (int)(vector1start.Item1 + ab * delta1x);
            var intersectionY = (int)(vector1start.Item2 + ab * delta1y);

            return new Tuple<int, int>(intersectionX, intersectionY);

        }

        private static int Determinant(Tuple<int, int> a, Tuple<int, int> b)
        {
            return a.Item1 * b.Item2 - a.Item2 * b.Item1;
        }

        private static int GetSkalärprodukt(Tuple<int,int> a, Tuple<int,int> b)
        {
            return a.Item1 * b.Item1 + a.Item2 * b.Item2;
        }
        private static Tuple<int,int> GetSkalärprodukt(Tuple<int, int> a, int b)
        {
            return new Tuple<int,int> (a.Item1 * b,  a.Item2 * b);
        }

        private static Tuple<int,int> Add(Tuple<int, int> a, Tuple<int, int> b)
        {
            return new Tuple<int, int>(a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        private static Tuple<int, int> Subtract(Tuple<int, int> a, Tuple<int, int> b)
        {
            return new Tuple<int, int>(a.Item1 - b.Item1, a.Item2 - b.Item2);
        }

        private static List<Tuple<int,int>> GetVectors(List<Tuple<Direction, int>> commands)
        {
            return CoordinatesFactory.GetCoordinates(commands);
        }

        private static List<List<Tuple<Direction, int>>> GetCommandsFromFile()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var commands = new List<Tuple<Direction, int>>();
            string line;
            var wires = new List<List<Tuple<Direction, int>>>();

            while ((line = file.ReadLine()) != null)
            {
                commands = line.Split(',').Select(c => new Tuple<Direction, int>((Direction)c[0], int.Parse(c.Substring(1)))).ToList();
                wires.Add(commands);
            }
            return wires;
        }
    }
}
