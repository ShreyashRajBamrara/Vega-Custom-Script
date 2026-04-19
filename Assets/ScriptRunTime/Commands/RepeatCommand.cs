using System.Collections;
using System.Collections.Generic;

public class RepeatCommand : ICommand
{
    private int count;
    private List<ICommand> commands;

    public RepeatCommand(int count, List<ICommand> commands)
    {
        this.count = count;
        this.commands = commands;
    }

    public IEnumerator Execute(GameAPI api)
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var cmd in commands)
            {
                yield return cmd.Execute(api);
            }
        }
    }
}