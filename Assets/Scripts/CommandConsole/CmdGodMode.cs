using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/God Mode")]
public class CmdGodMode : Command
{
    public override string[] Execute(string[] args)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHPDecorator>().isInvincible = bool.Parse(args[0]);
        return new[] { $"God Mode set to {bool.Parse(args[0])}" };
    }

    public override string[] Execute()
    {
        return new[] { "Wrong arguments for that command" };
    }
}
