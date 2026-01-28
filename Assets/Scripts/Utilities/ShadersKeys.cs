using UnityEngine;
using System.Collections.Generic;
public static class ShadersKeys {
    //DASHED LINE SHADER
    public static class DashedLine {
        public static readonly string Length = "_Length";
        public static readonly string TimeMultiplier = "_TimeMultiplier";
    }
}

public enum MaterialType {
    Lit, Dashed, Sprite
}

public static class Materials {

    private static bool initialized;
    private static readonly Dictionary<MaterialType, Material> _materials
        = new Dictionary<MaterialType, Material>();

    private static void Ensure() {
        if (initialized) return;
        initialized = true;
        _materials[MaterialType.Lit]  = Load("URPLit");
        _materials[MaterialType.Dashed] = Load("DashedLine");
        _materials[MaterialType.Sprite] = Load("SpriteDefault");
    }

    private static Material Load(string name) {
        var mat = Resources.Load<MaterialHolder>($"MaterialHolders/{name}").Material;
        if (mat == null)
            throw new System.Exception($"Material '{name}' not found");
        return mat;
    }

    public static Material Get(MaterialType type) {
        Ensure();
        return _materials[type];
    }
}
