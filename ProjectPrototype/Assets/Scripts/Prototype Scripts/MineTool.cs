using UnityEngine;

public class MineTool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject toolModelSpace;
    public GameObject toolModelGrav;

    [Header("Mine Settings")]
    [SerializeField] float miningRange;
    int miningStrength;
    [SerializeField] Transform laserOrigin;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] float damagePerSecond;
    [SerializeField] LayerMask miningLayer;

    IMine currentTarget;
    bool isMining = false;

    [Header("Weapon Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileForce;
    [SerializeField] float fireRange;
    [SerializeField] int damage;
    [SerializeField] GameObject hitFX;
    [SerializeField] float fireRate;

    float nextFireTime = 0f;
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
    [SerializeField] float pingCooldown;

    float pingTimer = 0f;
    AudioSource pingAudioSource;

    [Header("References")]
    [SerializeField] Camera playerCam;

    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] audMine;
    [Range(0, 1)][SerializeField] float audMineVol;
    [SerializeField] AudioClip[] audShoot;
    [Range(0, 1)][SerializeField] float audShootVol;



    // Bools
    bool isAiming = false;
    
    void Start()
    {
        miningStrength = 1;
        pingAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsPaused) {
        // If the player is holding down the right mouse button, then isAiming is true
        isAiming = Input.GetMouseButton(1);

            if (isAiming)
            {
                StopLaser();

                if (Input.GetMouseButton(0) && Time.time >= nextFireTime && toolModelSpace.GetComponent<MeshFilter>().sharedMesh != null)
                {
                    nextFireTime = Time.time + fireRate;
                    FireProjectile();
                }
            }
            else
            {
                if (!GetComponentInParent<ZeroG>().controller.enabled && toolModelSpace.GetComponent<MeshFilter>().sharedMesh != null)
                {
                    if (Input.GetMouseButton(0))
                    {
                        isMining = true;
                        TryMine();
                    }
                    else
                    {
                        aud.Stop();
                        isMining = false;
                        StopLaser();
                    }
                }
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
                laserLine.enabled = true;
                laserLine.SetPosition(0, laserOrigin.position);
                currentTarget.Mine(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));

                miningDamageBuffer += damagePerSecond * Time.deltaTime;

                if(miningDamageBuffer >= 1f)
                {
                    int damageToApply = Mathf.FloorToInt(miningDamageBuffer);
                    currentTarget.Mine(damageToApply);
                    miningDamageBuffer -= damageToApply;

                    aud.PlayOneShot(audMine[Random.Range(0, audMine.Length)], audMineVol);
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

        aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);

        if ( playerCam == null)
        {
            Debug.LogWarning("Player camera not assigned!");
            return;
        }

        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, fireRange))
        {   
            if(hit.collider.TryGetComponent<IDamage>(out var damageTarget))
            {
                damageTarget.TakeDamage(damage);
            }

            if (hitFX != null)
            {
                Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
            }

            Debug.Log("Hit: " + hit.collider.name);
        }
    }

    void StopLaser()
    {
        laserLine.enabled = false;
        currentTarget = null;
    }

    

    public float PingCooldown { get { return pingCooldown; } set { pingCooldown = value; } }

    public float MiningSpeed { get { return damagePerSecond; } set { damagePerSecond = value; } }
    public int MiningStrength { get { return miningStrength; } set { miningStrength = value; } }

    public int WeaponDamage { get { return damage; } set { damage = value; } }
    public float ShootRate { get { return fireRate; } set { fireRate = value; } }
    public float Range { get { return fireRange; } set { fireRange = value; } }
}
