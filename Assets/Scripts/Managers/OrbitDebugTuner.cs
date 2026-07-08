using UnityEngine;

public class OrbitDebugTuner : MonoBehaviour
{
    [Header("Debug Controls")]
    public Color selectedHighlightColor = Color.yellow;

    private OrbitController selectedController;
    private Renderer selectedRenderer;
    private Color originalColor;

    private void Update()
    {
        if (PlanetUI.Instance != null && PlanetUI.Instance.gameObject.activeSelf)
            return; // Don't process tuner when UI is open

        HandleSelection();
        HandleRadiusAdjustment();
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OrbitController oc = hit.transform.GetComponent<OrbitController>();
                if (oc != null)
                {
                    // Deselect previous
                    if (selectedController != null && selectedRenderer != null)
                        selectedRenderer.material.color = originalColor;

                    selectedController = oc;
                    selectedRenderer = hit.transform.GetComponent<Renderer>();

                    if (selectedRenderer != null)
                    {
                        originalColor = selectedRenderer.material.color;
                        selectedRenderer.material.color = selectedHighlightColor;
                    }

                    Debug.Log($"Selected {hit.transform.name} | Radius: {oc.orbitRadius}");
                }
            }
        }
    }

    private void HandleRadiusAdjustment()
    {
        if (selectedController == null) return;

        float change = 0f;
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
            change = 2f;
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            change = -2f;

        if (change != 0)
        {
            float newRadius = selectedController.orbitRadius + change;
            selectedController.SetRadius(Mathf.Max(3f, newRadius)); // Prevent too close
            Debug.Log($"Adjusted radius to {selectedController.orbitRadius}");
        }
    }
}