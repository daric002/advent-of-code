using System.Collections.Generic;

namespace intcode
{
    public class IntcodeReader
    {
        public IntcodeReader()
        {
            
        }

        private static List<int> GetOpcodeFromFile()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var opcodes = new List<int>();
            string line;
            line = file.ReadToEnd();
            opcodes = line.Split(',').Select(int.Parse).ToList();
            return opcodes;
        }

    }

}