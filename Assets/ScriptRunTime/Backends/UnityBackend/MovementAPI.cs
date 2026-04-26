using System.Collections;
using UnityEngine;

public class MovementAPI
{
    public void MoveInstant(GameObject obj, Vector3 direction, float value)
    {
        obj.transform.position += direction * value;
    }

    public IEnumerator MoveSmooth(GameObject obj, Vector3 direction, float value)
    {
        Vector3 start = obj.transform.position;
        Vector3 target = start + direction * value;

        float duration = 1f;
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