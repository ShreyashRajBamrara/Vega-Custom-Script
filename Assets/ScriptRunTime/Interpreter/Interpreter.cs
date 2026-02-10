
using System;
using System.IO;
using UnityEngine;

public class Interpreter
{
    private GameAPI api = new GameAPI();

    public void Run(string path)
    {
        if(!File.Exists(path))
        {
            Debug.LogError("Script file not found" + path);
            return;
        }
        Debug.Log("Running vega script");
        string[] lines = File.ReadAllLines(path);

        foreach(string line in lines)
        {
            if(string.IsNullOrWhiteSpace(line))continue;

            ICommand cmd =Parse(line);
            cmd?.Execute(api);
        }
    }

    private ICommand Parse(string line)
    {
        string[] parts = line.Split(' ');
        try
        {
            switch(parts[0])
            {
                case "PRINT":
                    return new PrintCommand(string.Join(" ",parts,1,parts.Length-1));

                case "MOVE":
                    return new MoveCommand(parts[1],float.Parse(parts[2]));

                default:
                    Debug.Log("Unknown Command"+parts[0]);
                    return null;

            }
        }
        catch(Exception e)
        {
            Debug.LogError("Parse Error :" + e.Message);
            return null;
        }
    }
}