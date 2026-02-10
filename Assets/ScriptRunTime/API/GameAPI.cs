using UnityEngine;

public class GameAPI
{
    public void Print(string msg)
    {
        Debug.Log("[VegaScript]"+msg);
    }

    public void Move(string objectName,float z)
    {
        GameObject obj = GameObject.Find(objectName);
        if(obj == null)
        {
            Debug.LogError("object not found: "+ objectName);
            return;
        }
        obj.transform.Translate(0,0,z);
    }
}