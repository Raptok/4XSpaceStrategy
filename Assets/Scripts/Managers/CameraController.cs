using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float heightSpeed = 5f;           // Decreased for better feel
    public float minHeight = 8f;
    public float maxHeight = 80f;

    private float targetHeight;               // NEW for smooth movement

    private void Start()
    {
        targetHeight = transform.position.y;
    }

    private void Update()
    {
        // WASD Pan
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v) * panSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Mouse Wheel - Smooth Height
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetHeight += scroll * heightSpeed * -25f;
            targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);
        }

        // Smoothly move toward target height
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetHeight, 8f * Time.deltaTime); // Smooth factor
        transform.position = pos;

        // Optional: Keep camera tilted down
        transform.rotation = Quaternion.Euler(55f, transform.eulerAngles.y, 0);
    }
}