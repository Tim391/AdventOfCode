using System;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day10
    {
        private string _input;
        public int Answer2()
        {
            _input = "3113322113";
            for (int i = 0; i < 50; i++)
            {
                _input = Iterate(_input);
                Console.WriteLine($"current loop: {i}");
            }
            return _input.Length;
        }

        private string Iterate(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                int count = 1;
                char c = input[i];
                while (i + 1 < input.Length && c == input[i + 1])
                {
                    i++;
                    count++;
                }
                sb.Append(count);
                sb.Append(c);

            }
            return sb.ToString();
        }
    }
}