using System.Collections.Generic;
using Algebra;
using System;

public class AlgebraExpressionParser {
    private List<Token> tokens;
    private int index;
        
    private Token Current => index < tokens.Count ? tokens[index] : null;

    public AlgebraExpression Parse(List<Token> tokens) {
        this.tokens = tokens;
        index = 0;
        return ParseExpression();
    }

    private AlgebraExpression ParseExpression(int priority = 0) {
        Token token = Current;
        if (token == null) return null;
        
        index++;
        AlgebraExpression left = ParsePrefix(token);
        
        while (Current != null && priority < GetPriority(Current.Name)) {
            token = Current;
            index++;
            left = ParseInfix(left, token);
        }
        return left;
    }

    private AlgebraExpression ParsePrefix(Token token) {
        switch (token.Name) {
            case TokenNames.LeftBracket:
                var expression = ParseExpression();
                Expect(TokenNames.RightBracket);
                return expression;
            
            case TokenNames.Num:
                double value = double.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture);
                return new NumberExpression(value);
            
            case TokenNames.Sub:
                AlgebraExpression node = ParseExpression(50);
                return new SubtractExpression(new NumberExpression(0), node);
            
            case TokenNames.Variable:
                string name = token.Value;
                if (Current != null && Current.Name == TokenNames.LeftBracket) {
                    index++;
                    AlgebraExpression argument = ParseExpression();
                    Expect(TokenNames.RightBracket);
                    return name switch {
                        TokenNames.Sqrt => new SqrtExpression(argument),
                        TokenNames.Sin => new FunctionExpression(argument, Math.Sin, TokenNames.Sin),
                        _ => throw new SystemException($"Function not found: {name}")
                    };
                }
                return new VariableExpression(() => 0, name);
        }
        throw new SystemException($"Unexpected token: {token.Name} at value {token.Value}");
    }

    private AlgebraExpression ParseInfix(AlgebraExpression left, Token token) {
        int priority = GetPriority(token.Name);
        if (token.Name == TokenNames.Pow) {
            priority -= 1;
        }
        
        var right = ParseExpression(priority);
        
        return token.Name switch {
            TokenNames.Add => new SumExpression(left, right),
            TokenNames.Sub => new SubtractExpression(left, right),
            TokenNames.Mul => new MulExpression(left, right),
            TokenNames.Div => new FractionExpression(left, right),
            TokenNames.Pow => new PowExpression(left, right),
            _ => throw new Exception($"Unknown operator: {token.Name}")
        };
    }

    private int GetPriority(string op) {
        return op switch {
            TokenNames.Add => 10,
            TokenNames.Sub => 10,
            TokenNames.Mul => 20,
            TokenNames.Div => 20,
            TokenNames.Pow => 30,
            _ => 0
        };
    }

    private void Expect(string name) {
        if (Current == null || Current.Name != name)
            throw new System.Exception($"Expected {name}, but got {(Current?.Name ?? "EOF")}");
        index++;
    }
}