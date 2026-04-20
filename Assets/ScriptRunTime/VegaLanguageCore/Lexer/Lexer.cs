using System;
using System.Collections.Generic;

public class Lexer
{
    private readonly string source;

    private readonly List<Token> tokens = new List<Token>();

    private int start = 0;
    private int current = 0;
    private int line = 1;

    // Keyword mapping
    private static readonly Dictionary<string, TokenType> keywords =
    new Dictionary<string, TokenType>()
{
    { "move", TokenType.MOVE },
    { "print", TokenType.PRINT },
    { "repeat", TokenType.REPEAT },
    { "end", TokenType.END },
    { "rotate", TokenType.ROTATE },
    { "wait", TokenType.WAIT },
    { "loop", TokenType.LOOP },
    { "spawn", TokenType.SPAWN },
    { "at", TokenType.AT }  
};

    public Lexer(string source)
    {
        this.source = source;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(TokenType.EOF, "", null, line));
        return tokens;
    }

    private void ScanToken()
    {
        char c = Advance();

        switch (c)
        {
            case '(':
                AddToken(TokenType.LEFT_PAREN);
                break;

            case ')':
                AddToken(TokenType.RIGHT_PAREN);
                break;

            case '+':
                AddToken(TokenType.PLUS);
                break;

            case '-':
                AddToken(TokenType.MINUS);
                break;

            case '*':
                AddToken(TokenType.STAR);
                break;

            case '/':
                AddToken(TokenType.SLASH);
                break;

            case ' ':
            case '\r':
            case '\t':
                // Ignore whitespace
                break;

            case '\n':
                line++;
                break;

            default:
                if (char.IsDigit(c))
                {
                    ScanNumber();
                }
                else if (char.IsLetter(c) || c == '_')
                {
                    ScanIdentifier();
                }
                else
                {
                    Console.WriteLine($"Unexpected character '{c}' at line {line}");
                }
                break;
        }
    }
    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (source[current] != expected) return false;

        current++;
        return true;
    }

    private void ScanNumber()
    {
        while (char.IsDigit(Peek()))
            Advance();

        // Handle decimal numbers
        if (Peek() == '.' && char.IsDigit(PeekNext()))
        {
            Advance(); // consume '.'

            while (char.IsDigit(Peek()))
                Advance();
        }

        string text = source.Substring(start, current - start);
        double value = double.Parse(text);

        AddToken(TokenType.NUMBER, value);
    }

    private void ScanIdentifier()
    {
        while (char.IsLetterOrDigit(Peek()) || Peek() == '_')
            Advance();

        string text = source.Substring(start, current - start);
        string lower = text.ToLower().Trim();

        if (keywords.ContainsKey(lower))
        {
            AddToken(keywords[lower]);
        }
        else
        {
            AddToken(TokenType.IDENTIFIER);
        }
    }

    private bool IsAtEnd()
    {
        return current >= source.Length;
    }

    private char Advance()
    {
        return source[current++];
    }

    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return source[current];
    }

    private char PeekNext()
    {
        if (current + 1 >= source.Length) return '\0';
        return source[current + 1];
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object literal)
    {
        string text = source.Substring(start, current - start);
        tokens.Add(new Token(type, text, literal, line));
    }
}
