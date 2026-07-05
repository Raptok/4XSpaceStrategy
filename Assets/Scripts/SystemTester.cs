using UnityEngine;
using System.Collections.Generic;

public class SystemTester : MonoBehaviour
{
    public SystemVisualizer visualizer;

    void Start()
    {
        SolarSystemGenerator gen = GetComponent<SolarSystemGenerator>();
        List<CelestialBody> system = gen.GenerateSystem();

        visualizer.BuildSystem(system);
    }
}