using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;

    [Header("---- Player Stats ----")]

    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] int gravity;

    Camera playerCam;

    int jumpCount;
    int HPOrig;

    Vector3 moveDir;
    Vector3 playerVel;

    bool isSprinting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterController>().enabled)
        {
            Movement();

            Sprint();
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
        }

        moveDir = (Input.GetAxis("Horizontal") * transform.right) +
                  (Input.GetAxis("Vertical") * transform.forward);


        //transform.position += moveDir * speed * Time.deltaTime;

        controller.Move(moveDir * speed * Time.deltaTime);

        Jump();

        playerVel.y -= gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
        }
    }

    void Sprint()
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
}
