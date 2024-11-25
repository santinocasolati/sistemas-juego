using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject, ICommand
{
    [SerializeField] private string _helpMsg;
    [SerializeField] private List<string> _aliases;

    public List<string> Aliases { get { return _aliases; } }
    public string HelpMsg { get { return _helpMsg; } }

    public abstract string[] Execute();
    public abstract string[] Execute(string[] args);
}
