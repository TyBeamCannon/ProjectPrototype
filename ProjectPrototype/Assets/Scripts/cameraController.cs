using UnityEngine;

public class cameraController : MonoBehaviour
{

    [SerializeField] int sens;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY;

    float rotX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;

        // Clamp camera on the x-axis
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

        // Rotate the camera on the x-axis to look up and down
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        // Rotate the player on the y-axis to look left and right
        transform.parent.Rotate(Vector3.up * mouseX);
    }

}
