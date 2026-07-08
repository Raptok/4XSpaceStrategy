using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRing : MonoBehaviour
{
    [Header("Orbit Ring Settings")]
    public int segments = 128;              // Higher = smoother circle
    public float lineWidth = 0.1f;
    public Color lineColor = new Color(0.5f, 0.8f, 1f, 0.6f); // Nice blue-ish

    private LineRenderer lr;
    private Transform parentBody;           // The star or planet this ring orbits
    private float currentRadius;

    public void Build(Transform parent, float radius)
    {
        parentBody = parent;
        currentRadius = radius;

        lr = GetComponent<LineRenderer>();

        lr.loop = true;
        lr.positionCount = segments;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default")); // Simple material
        lr.startColor = lineColor;
        lr.endColor = lineColor;

        DrawRing();

        // Make sure the ring follows the parent (star/planet)
        transform.SetParent(parentBody);
        transform.localPosition = Vector3.zero;
    }

    private void DrawRing()
    {
        if (lr == null) return;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * currentRadius,
                0f,
                Mathf.Sin(angle) * currentRadius
            );
            lr.SetPosition(i, pos);
        }
    }

    // Call this if radius changes later
    public void UpdateRadius(float newRadius)
    {
        currentRadius = newRadius;
        DrawRing();
    }
}