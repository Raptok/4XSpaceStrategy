using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlanetUI : MonoBehaviour
{
    public static PlanetUI Instance;

    public TMP_Text titleText;
    public TMP_Text infoText;
    TMP_Text tileInfoText;
    public PlanetGridVisualizer gridVisualizer;

    [Header("Closing Settings")]
    public Button closeButton;

    private bool justOpened = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        // ESC to close
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
            return;
        }

        // Click outside
        if (Input.GetMouseButtonDown(0) && !justOpened)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Hide();
            }
        }

        if (justOpened)
            justOpened = false;
    }

    public void Show(CelestialBody body)
    {
        if (body == null)
        {
            Debug.LogWarning("Tried to show null CelestialBody");
            return;
        }

        // === Populate UI ===
        titleText.text = body.name ?? body.type.ToString();

        infoText.text =
            $"Type: {body.type}\n" +
            $"Surface Size: {body.surfaceSize}x{body.surfaceSize}";

        // Show the planet surface grid
        if (gridVisualizer != null)
        {
            gridVisualizer.ShowSurface(body.surface);
        }
        else
        {
            Debug.LogWarning("gridVisualizer is not assigned on PlanetUI!");
        }

        gameObject.SetActive(true);
        justOpened = true;           // Prevent immediate close

        Debug.Log("Showing UI for: " + body.type);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        justOpened = false;
        Debug.Log("Planet UI Closed");
    }

    public void SelectTile(TerrainTile tile, SurfaceTileUI ui)
    {
        ShowTileInfo(tile);
    }

    public void ShowTileInfo(TerrainTile tile)
    {
        if (infoText != null)
        {
            infoText.text =
                $"Terrain: {tile.type}\n" +
                $"Occupied: {tile.occupied}";
        }
    }
}