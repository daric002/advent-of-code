using System;
using System.Collections.Generic;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
