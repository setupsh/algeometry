using System;
using UnityEngine;
public class FixedSizeArrayAttribute: PropertyAttribute {
        public int Size {get; private set;}

        public FixedSizeArrayAttribute(int size) {
            Size = size;
        }
}