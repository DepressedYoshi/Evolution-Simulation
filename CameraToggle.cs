using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float panSpeed = 40f;
    private float zoomSpeed = 10f;
    private float rotationSpeed = 120f;
    [SerializeField] private float maxY = 150f;
    [SerializeField] private float minY = 10f;

    private Vector3 dragOrigin;

    void Update()
    {
        PanCamera();
        ZoomCamera();
        RotateCamera();
    }

    void PanCamera()
    {
        float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;

        // Calculate forward and right movement based on the camera's orientation
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        if (Input.GetKey(KeyCode.LeftShift) && transform.position.y >= minY)
        {
            move.y -= panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space) && transform.position.y <= maxY)
        {
            move.y += panSpeed * Time.deltaTime;
        }
        else
        {
            move.y = 0; // Prevent the camera from moving up/down

        }

        transform.Translate(move, Space.World);


    }

    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 direction = transform.forward * scroll * zoomSpeed;
        transform.Translate(direction, Space.World);

    }

    void RotateCamera()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            transform.RotateAround(transform.position, transform.right, pos.y * rotationSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.up, -pos.x * rotationSpeed * Time.deltaTime);
        }
    }
}
