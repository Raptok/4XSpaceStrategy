using UnityEngine;

public class PlanetClick : MonoBehaviour
{
    public CelestialBody data;

    void OnMouseDown()
    {
        Debug.Log("Clicked: " + gameObject.name);
        PlanetUI.Instance.Show(data);
    }
}