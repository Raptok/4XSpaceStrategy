using UnityEngine;

public class ShipOrbit : MonoBehaviour
{
    public float orbitRadius;
    public float orbitSpeed;
    public float angle;

    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius;

        transform.localPosition = new Vector3(x, 0, z);
    }
}