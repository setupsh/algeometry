using UnityEngine;

public sealed class Token {
        public string Name {get; private set;}
        public string Value {get; private set;} 

        public Token(string name, string value) {
            Name = name;
            Value = value;
        }

        public override string ToString() {
            return $"[{Name}] : \'{Value.Replace("\n", "\\n")}\'";
        }
}
