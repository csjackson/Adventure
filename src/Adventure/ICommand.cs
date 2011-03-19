using System;
namespace Adventure
{
    public interface ICommand
    {
        bool IsValid(string input);
        void Execute(string input);
    }
}
