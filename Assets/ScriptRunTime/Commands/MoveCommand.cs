using UnityEngine;

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

    public void Execute(GameAPI api)
    {
        Debug.Log("Executing MoveCommand: " + objectName);
        api.Move(objectName, direction, value, mode);
    }

    public MoveCommand(string obj, float val)
    {
        this.objectName = obj;
        this.direction = "FORWARD";
        this.value = val;
        this.mode = ExecutionMode.Instant;
    }
}