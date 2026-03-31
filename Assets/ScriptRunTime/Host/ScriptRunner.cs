using UnityEngine;
using System.IO;

public class ScriptRunner : MonoBehaviour
{
    public string scriptFile;

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, scriptFile);

        Interpreter interpreter = new Interpreter();
        interpreter.Run(path);
    }
}