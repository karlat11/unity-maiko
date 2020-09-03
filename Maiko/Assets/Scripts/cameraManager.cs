using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform target;

    private float cameraDistanceMax = 20f;
    private float cameraDistanceMin = 5f;
    private float cameraDistance = 10f;
    private float scrollSpeed = 2.5f;
    private float movementSpeed = 5f;
    private float padding = 50f;
    private float smoothSpeed = 10f;
    private Vector3 targetPos;

    private void Start()
    {
        cameraDistance = Camera.main.orthographicSize;
        targetPos = transform.position;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
            Camera.main.orthographicSize = cameraDistance;
        }

        if (Input.mousePosition.x > Screen.width - padding) targetPos.x += movementSpeed * Time.deltaTime;
        else if (Input.mousePosition.x < padding) targetPos.x -= movementSpeed * Time.deltaTime;

        if (Input.mousePosition.y > Screen.height - padding) targetPos.y += movementSpeed * Time.deltaTime;
        else if (Input.mousePosition.y < padding) targetPos.y -= movementSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;

        if (Camera.main.orthographicSize > cameraDistance) Camera.main.orthographicSize--;
        else if (Camera.main.orthographicSize < cameraDistance) Camera.main.orthographicSize++;
    }
}
