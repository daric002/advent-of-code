using System;
using System.Linq;
using System.Collections.Generic;

namespace intcode
{
    public class Intcode
    {
        public List<int> instructions {get;set;}
        public void Run()
        {
            RunProgram();
        }

        private void RunProgram()
        {
            int i = 0;
            while (i <= instructions.Count)
            {
                var opCodeAsString = instructions[i].ToString("D5");
                var opcode = (Operations)Enum.Parse(typeof(Operations), opCodeAsString.Substring(opCodeAsString.Length - 2));
                switch (opcode)
                {
                    case Operations.Add:
                        AddOperation(opCodeAsString, i);
                        i += 4;
                        break;
                    case Operations.Multiply:
                        MultiplyOperations(opCodeAsString, i);
                        i += 4;
                        break;
                    case Operations.Save:
                        var input  = Convert.ToInt32(Console.ReadLine());
                        Save(input, opCodeAsString, i);
                        i += 2;
                        break;
                    case Operations.Write:
                        Write(opCodeAsString, i);
                        i += 2;
                        break;
                    case Operations.Exit:
                        i = 1000000;
                        break;
                    case Operations.JumpIfTrue:
                        i = JumpIfTrueOperation(opCodeAsString, i);
                        break;
                    case Operations.JumpIfFalse:
                        i = JumpIfFalseOperation(opCodeAsString, i);
                        break;
                    case Operations.LessThan:
                        LessThanOperation(opCodeAsString, i);
                        i += 4;
                        break;
                    case Operations.Equals:
                        EqualsOperation(opCodeAsString, i);
                        i += 4;
                        break;
                    default:
                        Console.WriteLine($"{i}: Wrong opcode");
                        throw new Exception($"{i}: Wrong opcode");
                }
            }
        }

        private void EqualsOperation(string opCodeAsString, int i)
        {
            GetParameters(opCodeAsString, i, out int para1, out int para2);
            var para3 = GetParameter3(opCodeAsString, i);

            instructions[para3] = para1 == para2 ? 1 : 0;
        }

        private void LessThanOperation(string opCodeAsString, int i)
        {
            GetParameters(opCodeAsString, i, out int para1, out int para2);
            var para3 = GetParameter3(opCodeAsString, i);

            instructions[para3] = para1 < para2 ? 1 : 0;
        }

        private int JumpIfFalseOperation(string opCodeAsString, int i)
        {
            GetParameters(opCodeAsString, i, out int para1, out int para2);
            if (para1 == 0)
                return para2;
            return i + 3;
        }

        private int JumpIfTrueOperation(string opCodeAsString, int i)
        {
            GetParameters(opCodeAsString, i, out int para1, out int para2);
            if (para1 != 0)
                return para2;
            return i + 3;
        }

        private void Write(string opcode, int i)
        {
            var parameter1 = GetParameter1(opcode, i);
            Parametermode parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opcode.Substring(opcode.Length - 3, 1));
            if (parametermode3 == Parametermode.Immediate)
            {
                Console.WriteLine($"{i}: {parameter1}");
            }
            else
            {
                Console.WriteLine($"{i}: {instructions[parameter1]}");
            }

        }

        private void Save(int input, string opcode, int i)
        {
            var parameter1 = GetParameter1(opcode, i);
            instructions[parameter1] = input;
        }


        private void AddOperation(string opCode, int i)
        {
            GetParameters(opCode, i, out int parameter1, out int parameter2);
            Parametermode parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCode.Substring(opCode.Length - 5, 1));
            var result = Addition(parameter1, parameter2);

            if (parametermode3 == Parametermode.Immediate)
            {
                instructions[i + 3] = result;
            }
            else
            {
                instructions[instructions[i + 3]] = result;
            }
        }

        private void MultiplyOperations(string opCode, int i)
        {
            GetParameters(opCode, i, out int parameter1, out int parameter2);
            var parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCode.Substring(opCode.Length - 5, 1));
            var result = Multiply(parameter1, parameter2);

            if (parametermode3 == Parametermode.Immediate)
            {
                instructions[i + 3] = result;
            }
            else
            {
                instructions[instructions[i + 3]] = result;
            }
        }

        private int GetParameter3(string opcode, int i)
        {
            var parameter1 = instructions[i + 3];
            return parameter1;
        }

        private int GetParameter1(string opcode, int i)
        {
            var parameter1 = instructions[i + 1];
            return parameter1;
        }

        private void GetParameters(string opcodeAsString, int index, out int parameter1, out int parameter2)
        {
            var paramtermode1 = (Parametermode)Enum.Parse(typeof(Parametermode), opcodeAsString.Substring(opcodeAsString.Length - 3, 1));
            var paramtermode2 = (Parametermode)Enum.Parse(typeof(Parametermode), opcodeAsString.Substring(opcodeAsString.Length - 4, 1));

            parameter1 = paramtermode1 == Parametermode.Immediate ? instructions[index + 1] : instructions[instructions[index + 1]];
            parameter2 = paramtermode2 == Parametermode.Immediate ? instructions[index + 2] : instructions[instructions[index + 2]];
        }

        private int Multiply(int first, int second)
        {
            return first * second;
        }

        private int Addition(int first, int second)
        {
            return first + second;
        }
    }

    public enum Operations
    {
        Add = 1,
        Multiply = 2,
        Save = 3,
        Write = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Exit = 99
    }

    public enum Parametermode
    {
        Position = 0,
        Immediate = 1
    }
}
