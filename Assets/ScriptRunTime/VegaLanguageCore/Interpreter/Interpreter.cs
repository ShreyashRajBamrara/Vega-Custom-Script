using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{
    private GameAPI api = new GameAPI();

    public List<ICommand> Run(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Script file not found: " + path);
            return null;
        }

        string code = File.ReadAllText(path);

        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.ScanTokens();

        CommandParser parser = new CommandParser(tokens);
        List<ICommand> commands = parser.Parse();

        return commands;
    }
    public IEnumerator RunCoroutine(List<ICommand> commands)
    {
        foreach (var cmd in commands)
        {
            yield return cmd.Execute(api);
        }
    }
}