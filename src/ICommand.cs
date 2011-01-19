using System;
namespace Adventure
{
    interface ICommand
    {
        bool IsValid(string input);
        void Execute(string input);
    }
}
