using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{
    private GameAPI api = new GameAPI();

    public void Run(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Script file not found: " + path);
            return;
        }

        Debug.Log("Running Vega Script");

        // Read full script
        string code = File.ReadAllText(path);

        Debug.Log("=== SOURCE ===");
        Debug.Log(code);

        // 1. Lexer
        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.ScanTokens();

        Debug.Log("=== TOKENS ===");
        foreach (var t in tokens)
        {
            Debug.Log(t);
        }

        // 2. Parser
        CommandParser parser = new CommandParser(tokens);
        List<ICommand> commands = parser.Parse();

        Debug.Log("=== COMMAND COUNT === " + commands.Count);

        // 3. Execute Commands
        foreach (var cmd in commands)
        {
            cmd.Execute(api);
        }
    }
}