using System.IO;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath,"play.vega");
        Debug.Log("Scripts Path = "+ path);
        Interpreter interpreter = new Interpreter();
        interpreter.Run(path);
    }
}