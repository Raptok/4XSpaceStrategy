using UnityEngine;
using UnityEngine.UI;

public class SurfaceTileUI : MonoBehaviour
{
    public TerrainTile tileData;
    public Image image;

    Color baseColor;

    public void Init(TerrainTile tile)
    {
        tileData = tile;
        image = GetComponent<Image>();

        baseColor = TerrainColorMap.Get(tile.type);
        image.color = baseColor;
    }

    public void OnClick()
    {
        PlanetUI.Instance.SelectTile(tileData, this);
    }

    public void SetHighlight(bool state)
    {
        image.color = state ? Color.yellow : baseColor;
    }
}