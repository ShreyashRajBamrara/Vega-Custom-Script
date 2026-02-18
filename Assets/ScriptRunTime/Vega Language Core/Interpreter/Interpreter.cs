
using System;
using System.IO;
using UnityEngine;

public class Interpreter
{
    private GameAPI api = new GameAPI();

    public void Run(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Script file not found" + path);
            return;
        }
        Debug.Log("Running vega script");
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            ICommand cmd = Parse(line);
            cmd?.Execute(api);
        }
    }

    private ICommand Parse(string line)
    {
        string objectName = "";
        string direction = "";
        string axis = "";
        float value = 0f;

        string[] parts = line.Split(' ');
        try
        {
            switch (parts[0].ToUpper())
            {
                case "PRINT":
                    return new PrintCommand(string.Join(" ", parts, 1, parts.Length - 1));

                case "MOVE":

                    objectName = parts[1];
                    direction = parts[2].ToUpper();
                    value = float.Parse(parts[3]);

                    ExecutionMode movementMode = ExecutionMode.Instant;

                    if (parts.Length >= 5)
                    {
                        if (parts[4].ToUpper() == "SMOOTH")
                        {
                            movementMode = ExecutionMode.Smooth;
                        }
                    }

                    return new MoveCommand(objectName, direction, value, movementMode);


                case "ROTATE":

                    objectName = parts[1];
                    axis = parts[2].ToUpper();
                    value = float.Parse(parts[3]);

                    ExecutionMode rotationMode = ExecutionMode.Instant;

                    if (parts.Length >= 5)
                    {
                        if (parts[4].ToUpper() == "SMOOTH")
                        {
                            rotationMode = ExecutionMode.Smooth;
                        }
                    }

                    return new RotateCommand(objectName, axis, value, rotationMode);


                default:
                    Debug.Log("Unknown Command" + parts[0]);
                    return null;

            }
        }
        catch (Exception e)
        {
            Debug.LogError("Parse Error :" + e.Message);
            return null;
        }
    }
}