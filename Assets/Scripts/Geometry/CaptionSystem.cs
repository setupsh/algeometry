using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Geometry {
    public class CaptionSystem {
        public static readonly List<string> UpperLatinLetters = Enumerable.Range(65, 25).Select(code => ((char)code).ToString()).ToList();
        public static readonly List<string> LowerLatinLetters = Enumerable.Range(96, 25).Select(code => ((char)code).ToString()).ToList();
        public static readonly List<string> GreekAlphabet = Enumerable.Range(945, 25).Select(code => ((char)code).ToString()).ToList();
        private Dictionary<string, bool> captions =  new Dictionary<string, bool>();
        
        public string GetFreeCaption(List<string> from) {
            foreach (string caption in from) {
                if (captions.TryGetValue(caption, out bool value)) {
                    if (value) continue;
                    return caption;
                }
                else {
                    captions.Add(caption, true);
                    return caption;
                }
            }
            return GetFreeCaption(from.Select(caption => caption + caption).ToList());
        }

        public void FreeCaption(string caption) {
            captions[caption] = false;
        }
    }
}