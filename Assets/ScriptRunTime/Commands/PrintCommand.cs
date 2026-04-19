using System.Collections;
using UnityEngine;

public class PrintCommand : ICommand
{
    private string message;

    public PrintCommand(string msg)
    {
        message = msg;
    }

    public IEnumerator Execute(GameAPI api)
    {
        api.Print(message);
        yield return null;
    }
}