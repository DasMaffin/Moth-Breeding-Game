using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Speed at which the camera moves
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get input for horizontal (A/D) and vertical (W/S) movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        // Move the camera by modifying its transform
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
