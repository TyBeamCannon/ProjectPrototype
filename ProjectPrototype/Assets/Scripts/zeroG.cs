using UnityEngine;

public class zeroG : MonoBehaviour
{

    [Header("Thrust Settings")]
    [SerializeField] float thrustForce = 5f;
    [SerializeField] float strafeForce = 4f;
    [SerializeField] float ascendForce = 4f;
    [SerializeField] float maxSpeed = 10f;

    [Header("Look Settings")]
    [SerializeField] float lookSensitivity = 2f;

    private Rigidbody rb;
    private Camera playerCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sets the rigid body and camera on start
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>(); 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        transform.Rotate(0f, mouseX, 0f);

        Vector3 currentRotation = playerCam.transform.localEulerAngles;
        currentRotation.x -= mouseY;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -89f, 89f);
        playerCam.transform.localEulerAngles = new Vector3(currentRotation.x, 0f, 0f);
    }

    void HandleMovement()
    {
        Vector3 forceDir = transform.forward * Input.GetAxis("Vertical") * thrustForce + transform.right * Input.GetAxis("Horizontal") * strafeForce;

        if(Input.GetKey(KeyCode.Space))
        {
            forceDir += transform.up * ascendForce;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            forceDir -= transform.up * ascendForce;
        }

        if(rb.angularVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(forceDir, ForceMode.Acceleration);
        }

    }

}
