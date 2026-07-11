using UnityEngine;

public class PlanetSurface
{
    public int width;
    public int height;
    public TerrainTile[,] tiles;

    public PlanetSurface(int w, int h)
    {
        width = w;
        height = h;
        tiles = new TerrainTile[width, height];
        Debug.Log($"Created rectangular surface {width}x{height}");
    }
}