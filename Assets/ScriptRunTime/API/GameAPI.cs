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

    public void Rotate(string objectName, string axis, float angle, ExecutionMode executionMode)
    {
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogError("Object not found: " + objectName);
            return;
        }

        Vector3 axisVector = GetAxis(axis);

        if (axisVector == Vector3.zero)
            return;

        if (executionMode == ExecutionMode.Instant)
        {
            rotationAPI.RotateInstant(obj, axisVector, angle);
        }
        else
        {
            rotationAPI.RotateSmooth(obj, axisVector, angle);
        }
    }

    private Vector3 GetDirection(string direction)
    {
        switch (direction.ToUpper())
        {
            case "FORWARD": return Vector3.forward;
            case "BACKWARD": return Vector3.back;
            case "LEFT": return Vector3.left;
            case "RIGHT": return Vector3.right;
            case "UP": return Vector3.up;
            case "DOWN": return Vector3.down;

            default:
                Debug.LogError("Invalid direction: " + direction);
                return Vector3.zero;
        }
    }

    private Vector3 GetAxis(string axis)
    {
        switch (axis.ToUpper())
        {
            case "X": return Vector3.right;
            case "Y": return Vector3.up;
            case "Z": return Vector3.forward;

            default:
                Debug.LogError("Invalid axis: " + axis);
                return Vector3.zero;
        }
    }
}
