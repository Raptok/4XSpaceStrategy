using UnityEngine;
using System.Collections.Generic;

public class CelestialBody
{
    public string name;
    public CelestialBodyType type;
    public ResourceDeposit resources;

    public int surfaceSize;
    public PlanetSurface surface;

    public List<CelestialBody> moons = new();

    [System.NonSerialized]   // Don't save this in data
    public GameObject visualObject;   // NEW - reference to the spawned visual

    public CelestialBody(CelestialBodyType type)
    {
        this.type = type;
        this.name = type.ToString();
        this.resources = new ResourceDeposit();
    }
}