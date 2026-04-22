using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : ICommand
{
    private string objectName;
    private string locationName;
    private List<ICommand> innerCommands;

    public IfCommand(string obj, string loc, List<ICommand> commands)
    {
        objectName = obj;
        locationName = loc;
        innerCommands = commands;
    }

    public IEnumerator Execute(GameAPI api)
{
    while (true)
    {
        if (api.IsAtLocation(objectName, locationName))
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