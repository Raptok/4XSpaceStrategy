using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Generation")]
    public SolarSystemGenerator systemGenerator;

    [Header("Visualization")]
    public SystemVisualizer systemVisualizer;   // <-- Add this line

    [Header("Scene Setup")]
    public Transform systemParent;

    private void Start()
    {
        GenerateStartingSystem();
    }

    public void GenerateStartingSystem()
    {
        if (systemGenerator == null)
        {
            Debug.LogError("Assign SolarSystemGenerator!");
            return;
        }

        Debug.Log("=== Generating New Solar System ===");

        var bodies = systemGenerator.GenerateSystem();

        Debug.Log($"Successfully generated {bodies.Count} celestial bodies!");

        // Log details
        for (int i = 0; i < bodies.Count; i++)
        {
            var body = bodies[i];
            Debug.Log($"Body {i + 1}: {body.type} | Size: {body.surfaceSize} | Moons: {body.moons.Count}");
        }

        // === NEW: Visualize the system ===
        if (systemVisualizer != null)
        {
            systemVisualizer.VisualizeSystem(bodies, systemGenerator.currentStarType);
        }
        else
        {
            Debug.LogWarning("SystemVisualizer not assigned in GameManager!");
        }
    }
}