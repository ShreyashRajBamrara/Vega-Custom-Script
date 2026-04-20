using System.Collections;
using UnityEngine;

public class SpawnCommand : ICommand
{
    private string prefabName;
    private string locationName;

    public SpawnCommand(string prefab, string location)
    {
        prefabName = prefab;
        locationName = location;
    }

    public IEnumerator Execute(GameAPI api)
    {
        Debug.Log("Executing Spawn: " + prefabName + " at " + locationName);

        api.Spawn(prefabName, locationName);

        yield return null;
    }
}