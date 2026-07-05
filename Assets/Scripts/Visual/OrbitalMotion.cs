using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    public float orbitRadius = 5f;
    public float baseSpeed = 30f;
    public float sizeSpeedMultiplier = 1f;

    float orbitAngle;

    void Start()
    {
        baseSpeed /= Mathf.Sqrt(orbitRadius);
    }

    void Update()
    {
        orbitAngle += baseSpeed * sizeSpeedMultiplier * Time.deltaTime;

        float radians = orbitAngle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians) * orbitRadius;
        float z = Mathf.Sin(radians) * orbitRadius;

        transform.localPosition = new Vector3(x, 0, z);
    }
}