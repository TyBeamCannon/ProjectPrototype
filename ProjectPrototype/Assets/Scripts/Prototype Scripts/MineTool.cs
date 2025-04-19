using UnityEngine;

public class MineTool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Tool mode
    [Header("Tool Mode")]
    [SerializeField] private KeyCode toggleModeKey = KeyCode.F;
    bool isCombatMode = false;

    [Header("Mine Settings")]
    [SerializeField] float miningRange;
    [SerializeField] int miningDamage;
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

    [Header("References")]
    [SerializeField] Camera playerCam;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleModeKey))
        {
            isCombatMode = !isCombatMode;
            Debug.Log(isCombatMode ? "Combat Mode" : "Mining Mode");
        }

        // Laser mining (hold down to fire a mining laser damaging the ore over time)
        if(!isCombatMode && Input.GetKey(mineKey))
        {
            isMining = true;
            TryMine();
        }
        else
        {
            isMining = false;
            StopLaser();
        }

        // For when you are in combat mode
        if(isCombatMode && Input.GetKeyDown(mineKey))
        {
            FireProjectile();
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

            currentTarget = hit.collider.GetComponent<IMine>();
            if(currentTarget != null)
            {
                currentTarget.Mine(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
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
        if(projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Projectile or FirePoint not assigned!");
            return;
        }

        GameObject shot = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(firePoint.forward * projectileForce, ForceMode.Impulse);
        }
    }

    void StopLaser()
    {
        laserLine.enabled = false;
        currentTarget = null;
    }
}
