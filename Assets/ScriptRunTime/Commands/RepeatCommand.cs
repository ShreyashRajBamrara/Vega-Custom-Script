using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatCommand : ICommand
{
    private int count;
    private List<ICommand> commands;

    public RepeatCommand(int count, List<ICommand> commands)
    {
        this.count = count;
        this.commands = commands;
    }

    public void Execute(GameAPI api)
    {
        CoroutineRunner.Instance.StartCoroutine(RunLoop(api));
    }

    private IEnumerator RunLoop(GameAPI api)
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var cmd in commands)
            {
                cmd.Execute(api);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}