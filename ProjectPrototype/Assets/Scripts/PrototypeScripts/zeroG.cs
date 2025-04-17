using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class zeroG : MonoBehaviour, IDamage
{

    [SerializeField] int HP;


    // This controls the players speed in the Zero G environment
    [Header("Thrust Settings")]
    [SerializeField] float thrustForce = 5f;
    [SerializeField] float strafeForce = 4f;
    [SerializeField] float ascendForce = 4f;
    [SerializeField] float maxSpeed = 10f;

    private AudioSource thrusterAudio;

    // How fast the player can look around
    [Header("Look Settings")]
    [SerializeField] float lookSensitivity = 0.5f;
    [SerializeField] float mouseSmoothTime = 0.15f;

    // Private variables for internal use
    private Rigidbody rb;
    private Camera playerCam;
    private Vector2 smoothMouseDelta;
    private float verticalLookRotation;

    int HPOrig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sets the rigid body and camera on start
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>(); 

        // Lock the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        thrusterAudio = GetComponent<AudioSource>();

        HPOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.isPaused)
        {
            thrusterAudio.Stop();
        }
        else {
            HandleMouseLook();

            if (Input.GetKeyDown(KeyCode.R))
            {
                Stabilize();
            }
        }
        
        
    }

    void FixedUpdate()
    {
        HandleMovement();


        bool isThrusting = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl);

        if (isThrusting)
        {
            if(!thrusterAudio.isPlaying)
            {
                thrusterAudio.Play(); 
            }
        }
        else
        {
            if(thrusterAudio.isPlaying)
            {
                thrusterAudio.Stop();
            }
        }

    }

    void HandleMouseLook()
    {

        // Get Raw mouse input
        Vector2 targetMouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * lookSensitivity;

        // smooth the input over time
        smoothMouseDelta = Vector2.Lerp(smoothMouseDelta, targetMouseDelta, 1f / mouseSmoothTime);

        // Rotate the player left/right
        transform.Rotate(Vector3.up * smoothMouseDelta.x);

        // Adjust and clamp vertical camera rotation
        verticalLookRotation -= smoothMouseDelta.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -89f, 89f);
        playerCam.transform.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);
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

    void Stabilize()
    {
        // Instantly stop all movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Re-Center player rotation (no roll/tilt)
        Vector3 flatRotation = transform.eulerAngles;
        flatRotation.z = 0f;
        transform.eulerAngles = flatRotation;

        // Re-center camera vertical look
        verticalLookRotation = 0f;
        playerCam.transform.localEulerAngles = Vector3.zero;

        // Restablize camera if offset
        GameObject.FindWithTag("Player").transform.rotation = new Quaternion(0f,0f, 0f, 0f);


    }

    public void takeDamage(int amount)
    {
        HP -= amount;
        updatePlayerUI();
        StartCoroutine(flashDamageScreen());

        if (HP <= 0)
        {
            // You Lose !
            gameManager.instance.YouLose();
        }
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrig;
    }

    IEnumerator flashDamageScreen()
    {
        gameManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageScreen.SetActive(false);
    }

}
