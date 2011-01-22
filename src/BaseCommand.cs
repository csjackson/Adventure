using System;
using System.Linq;

namespace Adventure
{
    public abstract class BaseCommand
    {
        protected bool IsFirstWord(string input, string word)
        {
            return input.Trim().ToLower().Split(' ')[0] == word;
        }
        protected string GetAllButFirstWord(string input)
        {
            return string.Join(" ", input.Trim().Split(' ').Skip(1));
        }
    }
}
