using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static float timeScale = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            timeScale = timeScale == 1f ? 5f : 1f;

        Time.timeScale = timeScale;
    }
}