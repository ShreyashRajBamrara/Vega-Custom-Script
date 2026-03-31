using System.Collections;
using UnityEngine;

public class WaitCommand : ICommand
{
    private float time;

    public WaitCommand(float t)
    {
        time = t;
    }

    public void Execute(GameAPI api)
    {
        CoroutineRunner.Instance.StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(time);
    }
}