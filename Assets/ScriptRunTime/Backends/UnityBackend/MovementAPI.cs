using System.Collections;
using UnityEngine;

public class MovementAPI
{
    private MonoBehaviour runner;

    public MovementAPI(MonoBehaviour coroutineRunner)
    {
        runner = coroutineRunner;
    }

    public void MoveInstant(GameObject obj, Vector3 direction, float value)
    {
        Debug.Log("MoveInstant called");
        obj.transform.position += direction * value;
        Debug.Log("New Position: " + obj.transform.position);
    }

    public void MoveSmooth(GameObject obj, Vector3 direction, float value)
    {
        runner.StartCoroutine(SmoothMove(obj, direction * value, 1f));
    }

    private IEnumerator SmoothMove(GameObject obj, Vector3 movement, float duration)
    {
        Vector3 start = obj.transform.position;
        Vector3 target = start + movement;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = target;
    }
}