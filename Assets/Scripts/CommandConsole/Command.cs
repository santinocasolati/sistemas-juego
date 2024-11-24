using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject, ICommand
{
    [SerializeField] private List<string> _aliases;

    public List<string> Aliases { get { return _aliases; } }

    public abstract void Execute(string[] args);
}
