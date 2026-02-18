using System;
using System.Collections.Generic;

public class Lexer
{
    private readonly string source;
    private readonly List<Token> tokens = new List<Token>();

    private int start =0;
    private int current =0;
    private int line =1;

    private static readonly Dictionary<string,TokenType> keywords =
        new Dictionary<string, TokenType>()
        {
            {"move",TokenType.Move},
            {"print",TokenType.Print}
        };

    public Lexer(string source)
    {
        this.source=source;
    }
}