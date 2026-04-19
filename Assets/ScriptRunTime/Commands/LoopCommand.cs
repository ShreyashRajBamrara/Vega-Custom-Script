using System.Collections;
using System.Collections.Generic;

public class LoopCommand : ICommand
{
    private List<ICommand> commands;

    public LoopCommand(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public IEnumerator Execute(GameAPI api)
    {
        while (true)
        {
            foreach (var cmd in commands)
            {
                yield return cmd.Execute(api);
            }
        }
    }
}