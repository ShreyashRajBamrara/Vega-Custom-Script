public class MoveCommand : ICommand
{
    private string objectName;
    private float value;

    public MoveCommand(string obj,float val)
    {
        objectName = obj;
        value = val;
    }

    public void Execute(GameAPI api)
    {
        api.Move(objectName,value);
    }
}