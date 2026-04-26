using System.Collections;
using UnityEngine;

public class GameAPI
{
    private MovementAPI movementAPI;
    private RotationAPI rotationAPI;
    private PrintAPI printAPI;

    public GameAPI()
    {
        movementAPI = new MovementAPI();    
        rotationAPI = new RotationAPI(CoroutineRunner.Instance);
        printAPI = new PrintAPI();
    }
    public void Print(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            printAPI.Print(message);
        }
    }

    public IEnumerator Move(string objectName, string direction, float value, ExecutionMode executionMode)
    {
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogError("Object not found: " + objectName);
            yield break;
        }

        Vector3 dir = GetDirection(direction);

        if (executionMode == ExecutionMode.Instant)
        {
            movementAPI.MoveInstant(obj, dir, value);
            yield return null;
        }
        else
        {
            yield return movementAPI.MoveSmooth(obj, dir, value);
        }
    }

    public void Rotate(string objectName, string direction, float value, ExecutionMode mode)
    {
        Debug.Log("GameAPI Rotate called");

        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogError("Object not found: " + objectName);
            return;
        }

        Vector3 axis = GetAxis(direction);

        if (mode == ExecutionMode.Instant)
        {
            rotationAPI.RotateInstant(obj, axis, value);
        }
        else
        {
            rotationAPI.RotateSmooth(obj, axis, value);
        }
    }

    private Vector3 GetDirection(string dir)
    {
        dir = dir.ToLower();

        switch (dir)
        {
            case "forward": return Vector3.forward;
            case "backward": return Vector3.back;
            case "left": return Vector3.left;
            case "right": return Vector3.right;
            case "up": return Vector3.up;
            case "down": return Vector3.down;
            default:
                Debug.LogError("Invalid direction: " + dir);
                return Vector3.zero;
        }
    }

    private Vector3 GetAxis(string dir)
    {
        dir = dir.ToLower();

        switch (dir)
        {
            case "x": return Vector3.right;
            case "y": return Vector3.up;
            case "z": return Vector3.forward;
            default: return Vector3.up;
        }
    }

    public bool IsAtLocation(string objectName, string locationName)
    {
        GameObject obj = GameObject.Find(objectName);
        GameObject loc = GameObject.Find(locationName);

        if (obj == null || loc == null)
            return false;

        float dist = Vector3.Distance(obj.transform.position, loc.transform.position);

        return dist < 1.5f;
    }
    public void EndGame()
    {
        Debug.Log("Game Ended");
        Time.timeScale = 0f;
    }
    public bool IsTouching(string objA, string objB)
    {
        GameObject a = GameObject.Find(objA);
        GameObject b = GameObject.Find(objB);

        if (a == null || b == null)
            return false;

        Collider colA = a.GetComponent<Collider>();
        Collider colB = b.GetComponent<Collider>();

        if (colA == null || colB == null)
            return false;

        return colA.bounds.Intersects(colB.bounds);
    }

    public void Spawn(string prefabName, string locationName)
    {
        GameObject prefab = null;
        GameObject location = null;

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.name == prefabName)
                prefab = obj;

            if (obj.name == locationName)
                location = obj;
        }

        if (prefab == null)
        {
            Debug.LogError("Prefab not found: " + prefabName);
            return;
        }

        if (location == null)
        {
            Debug.LogError("Location not found: " + locationName);
            return;
        }

        GameObject.Instantiate(prefab, location.transform.position, location.transform.rotation);
    }
}
