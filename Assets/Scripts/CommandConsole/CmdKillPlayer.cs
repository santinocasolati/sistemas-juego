using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Commands/Kill Player")]
public class CmdKillPlayer : Command
{
    public override void Execute(string[] args)
    {
        if (args.Length != 0)
        {
            Debug.LogError("Wrong arguments for that command");
            return;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<IHealth>().Damage(9999999999);
    }
}
