using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/God Mode")]
public class CmdGodMode : Command
{
    public override void Execute(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogError("Wrong arguments for that command");
            return;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHPDecorator>().isInvincible = bool.Parse(args[0]);
    }
}
