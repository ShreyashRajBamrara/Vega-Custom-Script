using UnityEditor;

public class RotateCommand : ICommand
{
    private string objectName;
    private float value;
    private string direction;
    private ExecutionMode mode;

    public RotateCommand(string obj,string dir,float val,ExecutionMode executionMode)
    {
        objectName = obj;
        direction=dir;
        value = val;
        mode = executionMode;
        
    }

    public void Execute(GameAPI api)
    {
        api.Rotate(objectName,direction,value,mode);
    }
}