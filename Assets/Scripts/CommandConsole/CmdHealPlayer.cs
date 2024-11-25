using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Heal Player")]
public class CmdHealPlayer : Command
{
    public override string[] Execute(string[] args)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<IHealth>().Heal(float.Parse(args[0]));
        return new[] { $"Player healed by {float.Parse(args[0])} points" };
    }

    public override string[] Execute()
    {
        return new[] { "Wrong arguments for that command" };
    }
}
