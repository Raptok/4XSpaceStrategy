using UnityEngine;
using System.Collections.Generic;

public class SystemVisualizer : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject planetPrefab;
    public GameObject starPrefab;

    [Header("References")]
    public Transform systemParent;

    public void VisualizeSystem(List<CelestialBody> bodies, StarType starType)
    {
        if (planetPrefab == null || systemParent == null)
        {
            Debug.LogError("Missing references in SystemVisualizer!");
            return;
        }

        // Clear old visuals
        foreach (Transform child in systemParent)
            Destroy(child.gameObject);

        // Spawn Central Star
        GameObject starObj = starPrefab != null
            ? Instantiate(starPrefab, systemParent)
            : GameObject.CreatePrimitive(PrimitiveType.Sphere);

        starObj.name = "Star";
        starObj.transform.localScale = Vector3.one * 3.5f;

        float currentRadius = 8f;   // Start closer to the star

        for (int i = 0; i < bodies.Count; i++)
        {
            var bodyData = bodies[i];

            GameObject visual = Instantiate(planetPrefab, systemParent);
            visual.name = bodyData.type.ToString();

            float scale = Mathf.Max(0.6f, bodyData.surfaceSize * 0.08f); // Smaller base scale
            visual.transform.localScale = Vector3.one * scale;

            // Data & Orbit Motion
            PlanetClick clickHandler = visual.GetComponent<PlanetClick>();
            if (clickHandler != null) clickHandler.data = bodyData;

            OrbitalMotion orbit = visual.GetComponent<OrbitalMotion>();
            if (orbit != null)
            {
                orbit.parentBody = starObj.transform;
                orbit.orbitRadius = currentRadius;
                orbit.orbitSpeed = 25f / Mathf.Max(1f, currentRadius * 0.3f);
            }

            // Orbit Ring
            OrbitRing ring = visual.AddComponent<OrbitRing>();
            ring.Build(starObj.transform, currentRadius);   // This should match the planet's orbitRadius

            // Color
            Renderer rend = visual.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = GetColorForType(bodyData.type);

            // Tighter spacing
            currentRadius += 5f + (bodyData.surfaceSize * 0.25f);
        }
    }

    private Color GetColorForType(CelestialBodyType type)
    {
        switch (type)
        {
            case CelestialBodyType.GasGiant: return new Color(0.9f, 0.7f, 0.4f);
            case CelestialBodyType.IcePlanet: return Color.cyan;
            case CelestialBodyType.VolcanicPlanet: return new Color(0.8f, 0.2f, 0.1f);
            case CelestialBodyType.OceanPlanet: return new Color(0.2f, 0.5f, 0.9f);
            case CelestialBodyType.BarrenPlanet: return Color.gray;
            default: return Color.green;
        }
    }
}