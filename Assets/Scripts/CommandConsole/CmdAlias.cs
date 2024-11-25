using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Alias")]
public class CmdAlias : Command
{
    public override string[] Execute(string[] args)
    {
        CommandConsole console = FindObjectOfType<CommandConsole>();
        Command cmd = console.registeredCommands.FirstOrDefault(command => command.Aliases.Contains(args[0]));

        if (cmd == null)
            return new[] { $"Command '{args[0]}' does not exist" };

        string[] msg = new[] { $"Aliases of command '{args[0]}':" };
        string[] aliases = cmd.Aliases.ToArray();

        return msg.Concat(aliases).ToArray();
    }

    public override string[] Execute()
    {
        return new[] { "Wrong arguments for that command" };
    }
}
