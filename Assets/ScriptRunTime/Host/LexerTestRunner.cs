using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LexerTestRunner : MonoBehaviour
{
    void Start()
    {
        TestLexerAndParser();
    }

    void TestLexerAndParser()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "test.vega");

        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return;
        }

        string code = File.ReadAllText(path);

        Debug.Log("=== SOURCE ===");
        Debug.Log(code);

        // LEXER
        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.ScanTokens();

        Debug.Log("=== TOKENS ===");
        foreach (var token in tokens)
        {
            Debug.Log(token);
        }

        // PARSER
        CommandParser parser = new CommandParser(tokens);
        var commands = parser.Parse();

        Debug.Log("=== COMMAND COUNT === " + commands.Count);
    }
}