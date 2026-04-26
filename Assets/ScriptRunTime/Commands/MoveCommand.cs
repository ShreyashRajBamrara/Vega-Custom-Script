using UnityEngine;
using System.Collections;

public class MoveCommand : ICommand
{
    private string objectName;
    private float value;
    private string direction;
    private ExecutionMode mode;

    public MoveCommand(string obj, string dir, float val, ExecutionMode executionMode)
    {
        objectName = obj;
        direction = dir;
        value = val;
        mode = executionMode;
    }

    public IEnumerator Execute(GameAPI api)
    {
        yield return api.Move(objectName, direction, value, mode);
    }
}