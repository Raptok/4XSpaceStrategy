public class TerrainTile
{
    public TerrainType type;
    public bool occupied;   // ADD THIS

    public TerrainTile(TerrainType t)
    {
        type = t;
        occupied = false;
    }
}