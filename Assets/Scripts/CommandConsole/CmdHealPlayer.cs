using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Heal Player")]
public class CmdHealPlayer : Command
{
    public override void Execute(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogError("Wrong arguments for that command");
            return;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<IHealth>().Heal(float.Parse(args[0]));
    }
}
