public class PlanetSurface
{
    public int width;
    public int height;
    public TerrainTile[,] tiles;

    public PlanetSurface(int w, int h)
    {
        width = w;
        height = h;
        tiles = new TerrainTile[w, h];
    }
}