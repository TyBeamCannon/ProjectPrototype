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

        if(Input.GetKeyDown(mineKey))
        {
            if(isCombatMode)
            {
                FireProjectile();
            }
            else
            {
                TryMine();
            }
        }
    }

    void TryMine()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, miningRange))
        {
            IMine ore = hit.collider.GetComponent<IMine>();
            if(ore != null)
            {
                ore.Mine(miningDamage);
            }
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
}
