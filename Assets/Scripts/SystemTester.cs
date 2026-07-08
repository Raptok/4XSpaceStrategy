using UnityEngine;

public class SystemTester : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;        // Assign in Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   // Press R to regenerate
        {
            if (gameManager != null)
                gameManager.GenerateStartingSystem();
            else
                Debug.Log("Assign GameManager in SystemTester!");
        }
    }
}