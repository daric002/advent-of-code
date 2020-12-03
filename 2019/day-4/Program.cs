using System;
using System.Collections.Generic;
using System.Linq;

namespace day_4
{
    class Program
    {

        private static string Input = "271973-785961";
        private static string[] Dubbles = { "11", "22", "33", "44", "55", "66", "77", "88", "99" };
        static void Main(string[] args)
        {
            GetRange(Input, out int max, out int  min);

            List<string> passwords = GetPasswords(min, max);
            Console.WriteLine(passwords.Count);
        }

        private static List<string> GetPasswords(int min, int max)
        {
            var passwords = new List<string>();
            for(int i=min; i <= max; i++)
            {
                var password = i.ToString();
                if (password.Length != 6) continue;
                if (!HasDubble(password)) continue;
                if (!Increasing(password)) continue;
                if (!HasOneDubble(password)) continue;

                Console.WriteLine(password);
                passwords.Add(password);
                
            }
            return passwords;
        }

        private static bool HasOneDubble(string password)
        {

            foreach(var dubble in Dubbles)
            {
                var index = password.IndexOf(dubble);
                if (index < 0) 
                    continue;
                if (index - 1 >= 0)
                {
                    if (password[index - 1] == dubble[0])
                        continue;
                }
                if (index + dubble.Length < password.Length)
                {
                    if (password[index + dubble.Length] == dubble[0])
                        continue;
                }
                return true;
            }
            
            return false;
        }

        private static bool Increasing(string password)
        {
            for(int i =1; i<password.Length; i++)
            {
                if (int.Parse(password[i].ToString()) < int.Parse(password[i - 1].ToString())) 
                    return false;
            }
            return true;
        }

        private static bool HasDubble(string password)
        {
            foreach(var dubble in Dubbles)
            {
                if (password.Contains(dubble))
                {
                    return true;
                }
                    
            }
            return false;
        }

        private static void GetRange(string input, out int max, out int min)
        {
            var ranges = input.Split('-').Select(i => int.Parse(i)).ToList();
            min = ranges[0];
            max = ranges[1];
        }
    }
}
