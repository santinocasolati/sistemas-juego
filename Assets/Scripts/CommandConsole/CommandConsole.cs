using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CommandConsole : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform logParent;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] public Command[] registeredCommands;

    private List<GameObject> logs = new();

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Execute()
    {
        if (inputField.text.Length > 0) { }
            ExecuteCommand(inputField.text);
    }

    private void ExecuteCommand(string input)
    {
        string[] splitInput = input.Split(' ');
        string inputCommand = splitInput[0];
        string[] args = splitInput.Length > 1 ? splitInput[1..] : new string[0];

        Command selectedCommand = registeredCommands.FirstOrDefault(c => c.Aliases.Contains(inputCommand));

        if (selectedCommand != null)
        {
            if (args.Length > 0)
                LogToConsole(selectedCommand.Execute(args));
            else
                LogToConsole(selectedCommand.Execute());
        } else
        {
            LogToConsole(new[] { $"Command '{inputCommand}' not recognized" });
        }

        inputField.text = "";
    }

    private void LogToConsole(string[] log)
    {
        foreach (string l in log)
        {
            GameObject textLog = Instantiate(logPrefab, logParent);
            textLog.GetComponent<TMP_Text>().text = l;
            logs.Add(textLog);
        }
    }

    internal void Clear()
    {
        foreach (GameObject textLog in logs)
        {
            Destroy(textLog);
        }
    }
}
