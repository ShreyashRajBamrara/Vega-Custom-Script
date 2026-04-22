using UnityEngine;

public class GameAPI
{
    private MovementAPI movementAPI;
    private RotationAPI rotationAPI;
    private PrintAPI printAPI;

    public GameAPI()
    {
        movementAPI = new MovementAPI(CoroutineRunner.Instance);
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

    public void Move(string objectName, string direction, float value, ExecutionMode executionMode)
    {
        // Debug.Log("GameAPI Move called");
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogError("Object not found: " + objectName);
            return;
        }

        Vector3 dir = GetDirection(direction);

        if (dir == Vector3.zero)
            return;

        if (executionMode == ExecutionMode.Instant)
        {

            movementAPI.MoveInstant(obj, dir, value);
        }
        else
        {
            movementAPI.MoveSmooth(obj, dir, value);
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
