using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCommand : ICommand
{
    private List<ICommand> commands;

    public LoopCommand(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Execute(GameAPI api)
    {
        CoroutineRunner.Instance.StartCoroutine(RunLoop(api));
    }

    private IEnumerator RunLoop(GameAPI api)
    {
        while (true)
        {
            foreach (var cmd in commands)
            {
                cmd.Execute(api);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}