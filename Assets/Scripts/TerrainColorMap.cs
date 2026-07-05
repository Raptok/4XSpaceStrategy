using UnityEngine;

public static class TerrainColorMap
{
    public static Color Get(TerrainType type)
    {
        switch (type)
        {
            case TerrainType.Plains: return new Color(0.4f, 0.8f, 0.4f);
            case TerrainType.Mountains: return new Color(0.5f, 0.5f, 0.5f);
            case TerrainType.Forest: return new Color(0.1f, 0.6f, 0.2f);
            case TerrainType.Ice: return new Color(0.7f, 0.9f, 1f);
            case TerrainType.Volcano: return new Color(0.9f, 0.2f, 0.1f);
            case TerrainType.Desert: return new Color(0.9f, 0.8f, 0.4f);
            case TerrainType.Ocean: return new Color(0.2f, 0.4f, 0.8f);
            case TerrainType.Island: return new Color(0.3f, 0.7f, 0.5f);
            case TerrainType.Crater: return new Color(0.4f, 0.4f, 0.45f);
            case TerrainType.Barren: return new Color(0.8f, 0.8f, 0.8f);
        }

        return Color.magenta;
    }
}