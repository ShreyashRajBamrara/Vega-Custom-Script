public class PrintCommand : ICommand
{
    private string message;

    public PrintCommand(string msg)
    {
        message = msg;
    }

    public void Execute(GameAPI api)
    {
        api.Print(message);
    }
}