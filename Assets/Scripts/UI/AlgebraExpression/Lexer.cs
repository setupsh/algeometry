using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class Lexer {
    public static List<Token> Tokenize(string source) {
        List<Token> tokens = new List<Token>();
            
        int cursor = 0;
        while (cursor < source.Length) {
            try {
                Match match = Lexemes.tokenPatterns
                    .Select(pattern => pattern.Match(source, cursor))
                    .Where(match => match.Success && match.Index == cursor)
                    .Aggregate((best, current) => best.Length >= current.Length ? best : current);
                if (match.Groups[^1].Name != "SEPARATOR")
                    tokens.Add(new Token(match.Groups[^1].Name, match.Value));
                cursor += match.Length;
            }
            catch {
                Debug.Log($"Tokenizer error: unexpected at \'{source[cursor]}\' : {cursor}. Couldn't find this keyword");
                return null;
            }
            //magic    
        }
        return tokens;
    }

    public static string ToRow(this List<Token> tokens) {
        string result = "";
        foreach (var token in tokens) {
            result += $"[{token.Name}]";
        }
        return result;
    }
}
