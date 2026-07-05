using UnityEngine;

public class MouseRaycastDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Raycast hit NOTHING");
            }
        }
    }
}