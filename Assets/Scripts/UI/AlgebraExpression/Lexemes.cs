using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Lexemes {
    public static readonly List<Regex> tokenPatterns = new List<Regex> {
        new Regex($@"\G(?<{TokenNames.Add}>\+)", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Sub}>\-)", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Mul}>\*)", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Div}>\/)", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Pow}>\^)", RegexOptions.Compiled),
        new Regex($@"\G(?<{TokenNames.LeftBracket}>\()", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.RightBracket}>\))", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Num}>[0-9]+(\.[0-9]+)?)", RegexOptions.Compiled),
        new Regex($@"\G(?<{TokenNames.Variable}>[a-zA-Zα-ωΑ-Ω]+)", RegexOptions.Compiled), 
        new Regex($@"\G(?<{TokenNames.Separator}>\s+)", RegexOptions.Compiled),
    };
    
}
public static class TokenNames {
    public const string Add = "ADD";
    public const string Sub = "SUB";
    public const string Mul = "MUL";
    public const string Div = "DIV";
    public const string Pow = "POW";
    public const string LeftBracket = "LEFT_BRACKET";
    public const string RightBracket = "RIGHT_BRACKET";
    public const string Num = "NUM";
    public const string Variable = "VARIABLE";
    public const string Separator = "SEPARATOR";
    
    public const string Sqrt = "sqrt";
    public const string Sin = "sin";
    public const string Cos = "cos";
    public const string Tan = "tan";
}
