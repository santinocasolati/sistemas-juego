using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Help")]
public class CmdHelp : Command
{
    public override string[] Execute(string[] args)
    {
        CommandConsole console = FindObjectOfType<CommandConsole>();
        Command cmd = console.registeredCommands.FirstOrDefault(command => command.Aliases.Contains(args[0]));

        if (cmd == null)
            return new[] { $"Command '{args[0]}' does not exist" };

        return new[] { cmd.HelpMsg };
    }

    public override string[] Execute()
    {
        CommandConsole console = FindObjectOfType<CommandConsole>();
        List<string> helpMsg = new();

        foreach (Command c in console.registeredCommands)
        {
            helpMsg.Add(c.HelpMsg);
        }

        return helpMsg.ToArray();
    }
}
