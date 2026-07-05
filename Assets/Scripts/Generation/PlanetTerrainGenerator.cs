using UnityEngine;

public static class PlanetTerrainGenerator
{
    public static PlanetSurface GenerateSurface(CelestialBody body)
    {
        int size = body.surfaceSize;
        PlanetSurface surface = new PlanetSurface(size, size);

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                TerrainType type = GenerateTerrainByNoise(body.type, x, y, size);
                surface.tiles[x, y] = new TerrainTile(type);
            }
        }

        return surface;
    }

    static TerrainType GenerateTerrainByNoise(CelestialBodyType planetType, int x, int y, int size)
    {
        float scale = 0.06f;

        float elevation = Mathf.PerlinNoise(x * scale, y * scale);
        float ridges = Mathf.PerlinNoise((x + 300) * scale * 2f, (y + 300) * scale * 2f);
        float moisture = Mathf.PerlinNoise((x + 100) * scale, (y + 100) * scale);
        float heat = Mathf.PerlinNoise((x + 200) * scale, (y + 200) * scale);

        // Ridge-based mountains (creates chains instead of blobs)
        if (ridges > 0.85f)
            return TerrainType.Mountains;

        if (planetType == CelestialBodyType.IcePlanet)
        {
            if (elevation < 0.25f) return TerrainType.Barren;
            if (Random.value < 0.03f) return TerrainType.Crater;
            return TerrainType.Ice;
        }

        if (planetType == CelestialBodyType.OceanPlanet)
        {
            if (elevation > 0.65f) return TerrainType.Island;
            if (heat > 0.87f) return TerrainType.Volcano;
            return TerrainType.Ocean;
        }

        if (planetType == CelestialBodyType.VolcanicPlanet)
        {
            if (heat > 0.78f) return TerrainType.Volcano;
            if (Random.value < 0.05f) return TerrainType.Crater;
            return TerrainType.Barren;
        }

        if (planetType == CelestialBodyType.BarrenPlanet)
        {
            if (Random.value < 0.11f) return TerrainType.Crater;
            return TerrainType.Barren;
        }

        if (planetType == CelestialBodyType.RockyPlanet)
        {
            if (elevation < 0.25f) return TerrainType.Mountains;
            if (moisture > 0.5f) return TerrainType.Forest;
            if (moisture > 0.9f) return TerrainType.Ocean;
            return TerrainType.Plains;
        }

        if (planetType == CelestialBodyType.Moon || planetType == CelestialBodyType.Asteroid)
        {
            if (Random.value < 0.3f) return TerrainType.Crater;
            return TerrainType.Barren;
        }

        return TerrainType.Barren;
    }
}