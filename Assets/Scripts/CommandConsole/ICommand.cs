using System.Collections;
using System.Collections.Generic;

public interface ICommand
{
    string[] Execute(string[] args);
}
