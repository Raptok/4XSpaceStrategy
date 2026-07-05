using TMPro;
using UnityEngine;

public class PlanetUI : MonoBehaviour
{
    public static PlanetUI Instance;

    public TMP_Text titleText;
    public TMP_Text infoText;
    TMP_Text tileInfoText;
    public PlanetGridVisualizer gridVisualizer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SelectTile(TerrainTile tile, SurfaceTileUI ui)
    {
        ShowTileInfo(tile);
    }

    public void ShowTileInfo(TerrainTile tile)
    {
        infoText.text =
            $"Terrain: {tile.type}\n" +
            $"Occupied: {tile.occupied}";
    }

    public void Show(CelestialBody body)
    {
        titleText.text = body.name;
        infoText.text =
            $"Type: {body.type}\n" +
            $"Surface Size: {body.surfaceSize}x{body.surfaceSize}";

        gridVisualizer.ShowSurface(body.surface);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}