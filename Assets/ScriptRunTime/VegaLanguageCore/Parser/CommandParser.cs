using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandParser
{
    private List<Token> tokens;
    private int current = 0;

    public CommandParser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public List<ICommand> Parse()
    {
        List<ICommand> commands = new List<ICommand>();

        while (!IsAtEnd())
        {
            ICommand cmd = ParseCommand();
            if (cmd != null)
                commands.Add(cmd);
        }

        return commands;
    }

    private ICommand ParseCommand()
    {
        if (Match(TokenType.MOVE))
        {
            Token entity = Consume(TokenType.IDENTIFIER, "Expected entity name");
            Token direction = Consume(TokenType.IDENTIFIER, "Expected direction");
            Token value = Consume(TokenType.NUMBER, "Expected number");

            float v = (float)(double)value.Literal;

            Debug.Log($"Parsed MOVE: {entity.Lexeme} {direction.Lexeme} {v}");

            return new MoveCommand(entity.Lexeme, direction.Lexeme, v, ExecutionMode.Instant);
        }

        if (Match(TokenType.PRINT))
        {
            Token msg = Consume(TokenType.IDENTIFIER, "Expected message");

            Debug.Log($"Parsed PRINT: {msg.Lexeme}");

            return new PrintCommand(msg.Lexeme);
        }

        if (Match(TokenType.REPEAT))
        {
            Token number = Consume(TokenType.NUMBER, "Expected repeat count");
            int count = (int)(double)number.Literal;

            List<ICommand> innerCommands = new List<ICommand>();

            while (!Check(TokenType.END) && !IsAtEnd())
            {
                ICommand cmd = ParseCommand();
                if (cmd != null)
                    innerCommands.Add(cmd);
            }

            Consume(TokenType.END, "Expected 'end' after repeat block");

            return new RepeatCommand(count, innerCommands);
        }

        if (Match(TokenType.ROTATE))
        {
            Token entity = Consume(TokenType.IDENTIFIER, "Expected entity name");
            Token direction = Consume(TokenType.IDENTIFIER, "Expected direction");
            Token value = Consume(TokenType.NUMBER, "Expected number");

            float v = (float)(double)value.Literal;

            Debug.Log($"Parsed ROTATE: {entity.Lexeme} {direction.Lexeme} {v}");

            return new RotateCommand(entity.Lexeme, direction.Lexeme, v, ExecutionMode.Instant);
        }

        if (Match(TokenType.WAIT))
        {
            Token time = Consume(TokenType.NUMBER, "Expected wait time");
            float t = (float)(double)time.Literal;

            return new WaitCommand(t);
        }

        if (Match(TokenType.LOOP))
        {
            List<ICommand> innerCommands = new List<ICommand>();

            while (!Check(TokenType.END) && !IsAtEnd())
            {
                ICommand cmd = ParseCommand();
                if (cmd != null)
                    innerCommands.Add(cmd);
            }

            Consume(TokenType.END, "Expected 'end' after loop block");

            return new LoopCommand(innerCommands);
        }

        Advance();
        return null;
    }

    private bool Match(TokenType type)
    {
        if (Check(type))
        {
            Advance();
            return true;
        }
        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();

        Debug.LogError("Parse Error: " + message);
        return null;
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return tokens[current].Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) current++;
        return tokens[current - 1];
    }

    private bool IsAtEnd()
    {
        return tokens[current].Type == TokenType.EOF;
    }
}