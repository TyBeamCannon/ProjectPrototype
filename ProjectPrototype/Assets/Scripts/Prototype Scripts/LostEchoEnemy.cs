using UnityEngine;
using System.Collections;

public class LostEchoEnemy : MonoBehaviour, IDamage
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float killDistance;

    [Header("FX")]
    [SerializeField] GameObject ichorBurstPrefab;
    [SerializeField] Transform fxSpawnPoint;

    [Header("Ragdoll")]
    [SerializeField] GameObject ragdollPrefab;
    [SerializeField] float destroyDelay = 5f;

    private Transform player;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        transform.position += dirToPlayer * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, dirToPlayer, Time.deltaTime * rotateSpeed);

        float dist = Vector3.Distance(transform.position, player.position);

        if(dist <= killDistance)
        {
            player.GetComponent<IDamage>()?.TakeDamage(9999);
        }

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        PlayIchorFX();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (ragdollPrefab != null)
        {
            GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            Destroy(ragdoll, destroyDelay);
        }

        Destroy(gameObject);

    }

    void PlayIchorFX()
    {
        if(ichorBurstPrefab != null && fxSpawnPoint != null)
        {
            GameObject fx = Instantiate(ichorBurstPrefab, fxSpawnPoint.position, Quaternion.identity);
            Destroy(fx, 2f);
        }
    }
}
