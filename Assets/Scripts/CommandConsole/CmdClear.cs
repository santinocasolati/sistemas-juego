using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Clear Console")]
public class CmdClear : Command
{
    public override string[] Execute(string[] args)
    {
        return new[] { "Wrong arguments for that command" };
    }

    public override string[] Execute()
    {
        CommandConsole console = FindObjectOfType<CommandConsole>();
        console.Clear();

        return new[] { "Console cleared" };
    }
}
