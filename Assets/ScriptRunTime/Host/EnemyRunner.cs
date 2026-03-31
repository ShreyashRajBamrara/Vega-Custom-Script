using UnityEngine;
using System.IO;

public class EnemyRunner : MonoBehaviour
{
    public string scriptFile = "enemy.vega";

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, scriptFile);

        Interpreter interpreter = new Interpreter();
        interpreter.Run(path);
    }
}