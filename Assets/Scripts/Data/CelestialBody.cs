using System.Collections.Generic;

public class CelestialBody
{
    public string name;
    public CelestialBodyType type;
    public ResourceDeposit resources;

    public int surfaceSize;
    public PlanetSurface surface;

    public List<CelestialBody> moons = new();

    public CelestialBody(CelestialBodyType type)
    {
        this.type = type;
        this.name = type.ToString();
        this.resources = new ResourceDeposit();
    }
}