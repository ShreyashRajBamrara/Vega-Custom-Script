using System.Collections;
using UnityEngine;

public class WaitCommand : ICommand
{
    private float time;

    public WaitCommand(float t)
    {
        time = t;
    }

    public IEnumerator Execute(GameAPI api)
    {
        yield return new WaitForSeconds(time);
    }
}