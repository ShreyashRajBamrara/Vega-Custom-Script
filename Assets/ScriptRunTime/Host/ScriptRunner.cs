using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScriptRunner : MonoBehaviour
{
    public string scriptFile;

    private Interpreter interpreter;

    void Start()
    {
        interpreter = new Interpreter();
        StartCoroutine(RunScript());
    }

    IEnumerator RunScript()
    {
        string path = Path.Combine(Application.streamingAssetsPath, scriptFile);

        Debug.Log("Loading script from: " + path);

        List<ICommand> commands = interpreter.Run(path);

        if (commands == null)
        {
            Debug.LogError("Failed to load commands.");
            yield break;
        }

        Debug.Log("Starting Script Execution...");

        yield return StartCoroutine(interpreter.RunCoroutine(commands));

        Debug.Log("Script Execution Finished.");
    }
}