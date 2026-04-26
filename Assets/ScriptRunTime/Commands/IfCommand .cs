using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : ICommand
{
    private string objectA;
    private string objectB;
    private string conditionType; 
    private List<ICommand> innerCommands;

    public IfCommand(string a, string b, string type, List<ICommand> commands)
    {
        objectA = a;
        objectB = b;
        conditionType = type;
        innerCommands = commands;
    }

    public IEnumerator Execute(GameAPI api)
    {
        while (true)
        {
            bool result = false;

            if (conditionType == "at")
                result = api.IsAtLocation(objectA, objectB);

            else if (conditionType == "touches")
                result = api.IsTouching(objectA, objectB);

            if (result)
            {
                foreach (var cmd in innerCommands)
                {
                    yield return cmd.Execute(api);
                }

                yield break;
            }

            yield return null;
        }
    }
}