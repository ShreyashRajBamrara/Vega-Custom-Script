using System.Collections;
using UnityEngine;

public class EndGameCommand : ICommand
{
    public IEnumerator Execute(GameAPI api)
    {
        api.EndGame();
        yield break;
    }
}