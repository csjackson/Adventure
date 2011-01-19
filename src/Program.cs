using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            do 
            {
                var input = Console.ReadLine();
                if (input.Trim() == "exit") break;
                Console.WriteLine(input);
            }
            while (true); ;
        }
    }
}
