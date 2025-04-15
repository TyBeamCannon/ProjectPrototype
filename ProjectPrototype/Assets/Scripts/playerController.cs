using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;

    [Header("---- Player Stats ----")]

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] int gravity;

    [Header("---- Gun Stats ----")]

    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;

    int jumpCount;
    int HPOrig;

    float shootTimer;

    Vector3 moveDir;
    Vector3 playerVel;

    bool isSprinting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPOrig = HP;
        updatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist, Color.white);

        Movement();

        Sprint();
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

        shootTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && shootTimer >= shootRate)
        {
            Shoot();
        }
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

    void Shoot()
    {
        shootTimer = 0;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist, ~ignoreLayer))
        {
            Debug.Log(hit.collider.name);

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.takeDamage(shootDamage);
            }
        }
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
