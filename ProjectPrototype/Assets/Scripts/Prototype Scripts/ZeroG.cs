using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class ZeroG : MonoBehaviour, IDamage
{
    [SerializeField] Mesh sphereMesh;
    [SerializeField] Mesh capsuleMesh;
    [SerializeField] LayerMask ignoreLayer;

    [Header("Player Stats")]
    [SerializeField] int HP;
    [SerializeField] int maxGoldCarry;
    [SerializeField] int maxCrystalCarry;
    [SerializeField] float interactRange;

    int goldAmount;
    int crystalAmount;

    [Header("---- Grav Player Stats ----")]

    [SerializeField] CharacterController controller;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] int gravity;

    Vector3 moveDir;
    Vector3 playerVel;

    int jumpCount;


    // This controls the players speed in the Zero G environment
    [Header("Thrust Settings")]
    [SerializeField] float thrustForce;
    [SerializeField] float strafeForce;
    [SerializeField] float ascendForce;
    [SerializeField] float maxSpeed;

    AudioSource thrusterAudio;

    // How fast the player can look around
    [Header("Look Settings")]
    [SerializeField] float lookSensitivity;
    [SerializeField] float mouseSmoothTime;

    // Private variables for internal use
    Rigidbody rb;

    Camera playerCam;
    Vector2 smoothMouseDelta;
    float verticalLookRotation;

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
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactRange, Color.white);

        if (controller.enabled)
        {
            if (!GameManager.instance.IsPaused)
            {
                GravMovement();
                HandleMouseLook();
            }
        }
        else
        {

            if (GameManager.instance.isPaused)
            {
                thrusterAudio.Stop();
            }
            else
            {
                HandleMouseLook();

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Stabilize();
                }
            }
        }
        
        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("Use button pressed");
            if (!GameManager.instance.isPaused)
            {
                Debug.Log("Game not paused");
                Interact();
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
        //verticalLookRotation = 0f;
        //playerCam.transform.localEulerAngles = Vector3.zero;



    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        updatePlayerUI();
        StartCoroutine(flashDamageScreen());

        if (HP <= 0)
        {
            // You Lose !
            GameManager.instance.YouLose();
        }
    }
    void GravMovement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
        }

        moveDir = (Input.GetAxis("Horizontal") * transform.right) +
                  (Input.GetAxis("Vertical") * transform.forward);


        //transform.position += moveDir * speed * Time.deltaTime;

        controller.Move(moveDir * speed * Time.deltaTime);

        GravJump();

        playerVel.y -= gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }

    void GravJump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
        }
    }

    void GravSprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
        }
    }
    public void updatePlayerUI()
    {
        GameManager.instance.healthMeter.fillAmount = (float)HP / HPOrig;
    }

    IEnumerator flashDamageScreen()
    {
        GameManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamageScreen.SetActive(false);
    }

    public void PlayerGrav(bool gravActive)
    {
        controller.enabled = gravActive;



        if (gravActive)
        {
            Stabilize();
            GetComponent<MeshFilter>().mesh = capsuleMesh;
            playerCam.transform.position += new Vector3(0, .5f, 0);
        }
        else if (!gravActive)
        {
            playerVel.y = 0;
            GetComponent<MeshFilter>().mesh = sphereMesh;
            playerCam.transform.position -= new Vector3(0, .5f, 0);
        }

    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange, ~ignoreLayer))
        {
            Debug.Log(hit.collider.name);
            IInteract interacted = hit.collider.GetComponent<IInteract>();

            if (interacted != null)
            {
                Debug.Log("Interactable Hit");
                interacted.Interact();
            }

        }
    }


    public int MaxGoldCarry { get { return maxGoldCarry; } set { maxGoldCarry = value;  } } 
    public int Gold { get { return goldAmount; } set { goldAmount = value; } }
    public int MaxCrystalCarry { get { return maxCrystalCarry; } set { maxCrystalCarry = value; } }
    public int Crystal {  get { return crystalAmount; } set { crystalAmount = value; } }
}
