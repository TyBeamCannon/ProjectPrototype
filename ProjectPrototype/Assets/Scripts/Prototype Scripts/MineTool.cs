using UnityEngine;

public class MineTool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Mine Settings")]
    [SerializeField] float miningRange;
    [SerializeField] KeyCode mineKey = KeyCode.Mouse0;
    [SerializeField] Transform laserOrigin;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] float damagePerSecond;
    [SerializeField] LayerMask miningLayer;

    IMine currentTarget = null;
    bool isMining = false;

    [Header("Weapon Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileForce;

    float miningDamageBuffer = 0f;

    [Header("Laser Flicker")]
    [SerializeField] float flickerSpeed;
    [SerializeField] float flickerIntensity;
    [SerializeField] float baseLaserWidth;

    float flickerTimer = 0;

    [Header("Scanner Ping")]
    [SerializeField] GameObject pingPulsePrefab;
    [SerializeField] Transform pingOrigin;
    [SerializeField] AudioClip pingSound;
    [SerializeField] float pingCooldown = 3f;

    float pingTimer = 0f;
    AudioSource pingAudioSource;

    [Header("References")]
    [SerializeField] Camera playerCam;

    // Bools
    bool isAiming = false;
    
    void Start()
    {
        pingAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // If the player is holding down the right mouse button, then isAiming is true
        isAiming = Input.GetMouseButton(1);

        if(isAiming)
        {
            StopLaser();

            if (Input.GetMouseButtonDown(0))
            {
                FireProjectile();
            }
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                isMining = true;
                TryMine();
            }
            else
            {
                isMining = false;
                StopLaser();
            }
        }

        pingTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Q) && pingTimer >= pingCooldown)
        {
            pingTimer = 0f;

            if(pingPulsePrefab != null && pingOrigin != null)
            {
                Vector3 spawnPos = transform.position;
                Instantiate(pingPulsePrefab, spawnPos, Quaternion.identity);
            }

            if(pingSound != null & pingAudioSource != null)
            {
                pingAudioSource.PlayOneShot(pingSound);
            }
        }


    }

    void TryMine()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

        laserLine.enabled = true;
        laserLine.SetPosition(0, laserOrigin.position);

        if(Physics.Raycast(ray, out RaycastHit hit, miningRange, miningLayer))
        {
            laserLine.SetPosition(1, hit.point);

            flickerTimer += Time.deltaTime * flickerSpeed;
            float flicker = Mathf.Sin(flickerTimer) * flickerIntensity;
            float currentWidth = baseLaserWidth + flicker;

            laserLine.startWidth = currentWidth;
            laserLine.endWidth = currentWidth;

            currentTarget = hit.collider.GetComponent<IMine>();
            if(currentTarget != null)
            {
                currentTarget.Mine(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));

                miningDamageBuffer += damagePerSecond * Time.deltaTime;

                if(miningDamageBuffer >= 1f)
                {
                    int damageToApply = Mathf.FloorToInt(miningDamageBuffer);
                    currentTarget.Mine(damageToApply);
                    miningDamageBuffer -= damageToApply;
                }


            }
        }
        else
        {
            laserLine.SetPosition(1, playerCam.transform.position + playerCam.transform.forward * miningRange);
            currentTarget = null;
        }
    }

    void FireProjectile()
    {
        if(projectilePrefab == null || firePoint == null || playerCam == null)
        {
            Debug.LogWarning("Projectile or FirePoint, or Camera not assigned!");
            return;
        }

        Vector3 shootDirection = playerCam.transform.forward;

        GameObject shot = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(shootDirection * projectileForce, ForceMode.Impulse);
        }
    }

    void StopLaser()
    {
        laserLine.enabled = false;
        currentTarget = null;
    }
}
