using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform parentBody;           // Assign the star or planet this orbits
    public float orbitRadius = 5f;
    public float orbitSpeed = 20f;         // Degrees per second

    private float currentAngle = 0f;

    private void Start()
    {
        if (parentBody == null)
        {
            Debug.LogWarning($"No parentBody assigned on {gameObject.name}. Disabling orbit.");
            enabled = false;
            return;
        }

        // Set initial position on a circle
        currentAngle = Random.Range(0f, 360f);
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

        transform.localPosition = offset;   // Use local if parented, or position if not
    }
}