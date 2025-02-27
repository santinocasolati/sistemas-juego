using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Kill Player")]
public class CmdKillPlayer : Command
{
    public override string[] Execute(string[] args)
    {
        return new[] { "Wrong arguments for that command" };
    }

    public override string[] Execute()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<IHealth>().Damage(9999999999);
        return new[] { "Player Killed" };
    }
}
