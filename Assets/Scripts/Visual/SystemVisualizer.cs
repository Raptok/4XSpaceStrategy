using UnityEngine;
using System.Collections.Generic;

public class SystemVisualizer : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject planetPrefab;
    public GameObject asteroidPrefab;

    public float orbitSpacing = 3f;

    public void BuildSystem(List<CelestialBody> system)
    {
        ClearSystem();

        // Spawn star at center
        Instantiate(starPrefab, Vector3.zero, Quaternion.identity, transform);

        float orbitRadius = orbitSpacing;

        foreach (var body in system)
        {
            CreateOrbitRing(orbitRadius);
            SpawnOrbitalBody(body, orbitRadius);
            orbitRadius += orbitSpacing;
        }

    }

    void SpawnOrbitalBody(CelestialBody body, float radius)
    {
        GameObject prefab = GetPrefabForType(body.type);
        GameObject obj = Instantiate(prefab, transform);

        SpawnMoons(obj.transform, body.moons);

        OrbitalMotion motion = obj.GetComponent<OrbitalMotion>();
        if (motion == null)
            motion = obj.AddComponent<OrbitalMotion>();

        motion.orbitRadius = radius;
        motion.sizeSpeedMultiplier = GetSpeedMultiplier(body.type);

        obj.name = body.type.ToString();
        ApplyVisualStyle(obj, body.type);

        PlanetClick click = obj.GetComponent<PlanetClick>();
        if (click == null)
            click = obj.AddComponent<PlanetClick>();

        click.data = body;
    }

    GameObject GetPrefabForType(CelestialBodyType type)
    {
        if (type == CelestialBodyType.Asteroid) return asteroidPrefab;
        return planetPrefab;
    }

    void ApplyVisualStyle(GameObject obj, CelestialBodyType type)
    {
        float scale = 1f;
        Color color = Color.white;

        switch (type)
        {
            case CelestialBodyType.GasGiant:
                scale = 2.5f;
                color = new Color(0.8f, 0.6f, 0.3f);
                break;

            case CelestialBodyType.IcePlanet:
                scale = 1.2f;
                color = Color.cyan;
                break;

            case CelestialBodyType.OceanPlanet:
                scale = 1.2f;
                color = new Color(0.0f, 0.0f, 1.0f);
                break;

            case CelestialBodyType.VolcanicPlanet:
                scale = 1.3f;
                color = new Color(1f, 0.3f, 0.1f);
                break;

            case CelestialBodyType.RockyPlanet:
                scale = 1.1f;
                color = new Color(0.2f, 0.6f, 0.1f);
                break;

            case CelestialBodyType.BarrenPlanet:
                scale = 1.0f;
                color = new Color(0.75f, 0.75f, 0.75f);
                break;

            case CelestialBodyType.Asteroid:
                scale = 0.4f;
                color = Color.gray;
                break;
        }

        obj.transform.localScale *= scale;

        Renderer r = obj.GetComponent<Renderer>();
        if (r != null)
            r.material.color = color;
    }

    void CreateOrbitRing(float radius)
    {
        GameObject ring = new GameObject("OrbitRing");
        ring.transform.parent = transform;

        LineRenderer lr = ring.AddComponent<LineRenderer>();
        lr.widthMultiplier = 0.05f;
        lr.useWorldSpace = false;

        OrbitRing or = ring.AddComponent<OrbitRing>();
        or.Build(radius);
    }

    float GetSpeedMultiplier(CelestialBodyType type)
    {
        switch (type)
        {
            case CelestialBodyType.Asteroid: return 1.5f;
            case CelestialBodyType.Moon: return 1.2f;
            case CelestialBodyType.BarrenPlanet: return 1f;
            case CelestialBodyType.OceanPlanet: return 1f;
            case CelestialBodyType.RockyPlanet: return 1f;
            case CelestialBodyType.IcePlanet: return 0.9f;
            case CelestialBodyType.VolcanicPlanet: return 0.95f;
            case CelestialBodyType.GasGiant: return 0.5f;
        }
        return 1f;
    }

    void SpawnMoons(Transform parent, List<CelestialBody> moons)
    {
        float radius = 0.6f;

        foreach (var moon in moons)
        {
            GameObject m = Instantiate(planetPrefab, parent);
            m.transform.localScale *= 0.4f;

            OrbitalMotion om = m.AddComponent<OrbitalMotion>();
            om.orbitRadius = radius;
            om.baseSpeed *= 3f;

            PlanetClick pc = m.AddComponent<PlanetClick>();
            pc.data = moon;

            radius += 0.5f;
        }
    }

    void ClearSystem()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}