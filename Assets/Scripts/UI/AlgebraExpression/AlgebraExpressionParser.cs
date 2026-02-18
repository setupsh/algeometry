using System;
using System.Collections.Generic;
using Algebra;
using UnityEngine;

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
            case ("LEFT_BRACKET"):
                var expression = ParseExpression();
                Expect("RIGHT_BRACKET");
                return expression;
            case ("NUM"):
                double value = double.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture);
                return new NumberExpression(value);
            case ("SUB"):
                AlgebraExpression node = ParseExpression(50);
                return new SubtractExpression(new NumberExpression(0), node);
            case ("VARIABLE"):
                string name = token.Value;
                if (Current != null && Current.Name == "LEFT_BRACKET") {
                    index++;
                    AlgebraExpression argument = ParseExpression();
                    Expect("RIGHT_BRACKET");
                    switch (name) {
                        case "sqrt": return new SqrtExpression(argument);
                        default: throw new SystemException($"Cant found function with name {name}");
                    }
                }
                return new VariableExpression(() => 0, name);
        }
        throw new SystemException();
    }
    private AlgebraExpression ParseInfix(AlgebraExpression left, Token token) {
        int priority = GetPriority(token.Name);
        if (token.Name == "POW") {
            priority -= 1;
        }
        var right = ParseExpression(priority);
        return token.Name switch {
            "ADD"   => new SumExpression(left, right),
            "SUB" => new SubtractExpression(left, right),
            "MUL"   => new MulExpression(left, right),
            "DIV"   => new FractionExpression(left, right),
            _ => throw new Exception($"Unknown operator: {token.Name}")
        };
    }

    private int GetPriority(string _operator) {
        switch (_operator) {
            case ("ADD"): return 10;
            case ("SUB"): return 10;
            case ("MUL"): return 20;
            case ("DIV"): return 20;
            case ("POW"): return 30;
            default: return 0;
        }
    }
    private void Expect(string name) {
        if (Current == null || Current.Name != name)
            throw new System.Exception($"Expected {name}");
        index++;
    }
}
