
using UnityEngine;
using System.Collections;

public class RotationAPI
{
    private MonoBehaviour runner;

    public RotationAPI(MonoBehaviour coroutineRunner)
    {
        runner = coroutineRunner;
    }

    public void RotateInstant(GameObject obj,Vector3 axis,float angle)
    {
        obj.transform.Rotate(axis *angle);
    }
   
   public void RotateSmooth(GameObject obj,Vector3 axis,float angle)
    {
        runner.StartCoroutine(SmoothRotate(obj,axis,angle,1f));
    }

    private IEnumerator SmoothRotate(GameObject obj, Vector3 axis, float angle, float duration)
    {
        float elapsed = 0f;
        float rotated = 0f;

        while (elapsed < duration)
        {
            float step = (angle / duration) * Time.deltaTime;
            obj.transform.Rotate(axis * step);

            rotated += step;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}