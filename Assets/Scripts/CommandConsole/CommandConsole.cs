using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CommandConsole : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Command[] registeredCommands;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Execute()
    {
        if (inputField.text.Length > 0)
            ExecuteCommand(inputField.text);
    }

    private void ExecuteCommand(string input)
    {
        string[] splitInput = input.Split(' ');
        string inputCommand = splitInput[0];
        string[] args = splitInput.Length > 1 ? splitInput[1..] : new string[0];

        Command selectedCommand = registeredCommands.FirstOrDefault(c => c.Aliases.Contains(inputCommand));

        if (selectedCommand != null)
            selectedCommand.Execute(args);
        else
            Debug.LogError($"Command '{inputCommand}' not recognized");

        inputField.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.parent.gameObject.SetActive(false);
    }
}
