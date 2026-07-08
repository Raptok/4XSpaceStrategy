using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitController : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform parentBody;
    public float orbitRadius = 10f;
    public float orbitSpeed = 20f;

    [Header("Visual Ring")]
    public int segments = 120;
    public float lineWidth = 0.1f;
    public Color ringColor = new Color(0.4f, 0.7f, 1f, 0.6f);

    private LineRenderer lineRenderer;
    private float currentAngle = 0f;
    private Transform ringTransform;

    private void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    public void Setup(Transform parent, float radius, float speed)
    {
        parentBody = parent;
        orbitRadius = radius;
        orbitSpeed = speed;

        if (parentBody == null)
        {
            Debug.LogWarning("Setup called with null parent!");
            return;
        }

        currentAngle = Random.Range(0f, 360f);
        CreateOrbitRing();
        UpdatePosition();

        Debug.Log($"Setup complete for {gameObject.name}");
    }

    private void InitializeOrbit()
    {
        currentAngle = Random.Range(0f, 360f);
        CreateOrbitRing();
        UpdatePosition();
    }

    private void Update()
    {
        if (parentBody == null) return;

        currentAngle += orbitSpeed * Time.deltaTime;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Cos(rad) * orbitRadius,
            0f,
            Mathf.Sin(rad) * orbitRadius
        );
        transform.position = parentBody.position + offset;
    }

    private void CreateOrbitRing()
    {
        GameObject ringObj = new GameObject(gameObject.name + "_OrbitRing");
        ringTransform = ringObj.transform;
        ringTransform.SetParent(parentBody); // Parent to star/planet
        ringTransform.localPosition = Vector3.zero;

        LineRenderer ringLR = ringObj.AddComponent<LineRenderer>();
        ringLR.loop = true;
        ringLR.positionCount = segments;
        ringLR.startWidth = lineWidth;
        ringLR.endWidth = lineWidth;
        ringLR.useWorldSpace = false;
        ringLR.material = new Material(Shader.Find("Sprites/Default"));
        ringLR.startColor = ringColor;
        ringLR.endColor = ringColor;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * orbitRadius,
                0f,
                Mathf.Sin(angle) * orbitRadius
            );
            ringLR.SetPosition(i, pos);
        }
    }

    public void SetRadius(float newRadius)
    {
        orbitRadius = newRadius;
        if (ringTransform != null)
        {
            LineRenderer lr = ringTransform.GetComponent<LineRenderer>();
            if (lr != null)
            {
                for (int i = 0; i < segments; i++)
                {
                    float angle = i * Mathf.PI * 2f / segments;
                    Vector3 pos = new Vector3(
                        Mathf.Cos(angle) * newRadius,
                        0f,
                        Mathf.Sin(angle) * newRadius
                    );
                    lr.SetPosition(i, pos);
                }
            }
        }
    }
}