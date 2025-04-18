using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;

    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;


    bool playerInRange;

    float shootTimer;

    Color colorOrig;

    Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            playerDir = (GameManager.instance.player.transform.position - transform.position);

            agent.SetDestination(GameManager.instance.player.transform.position);

            agent.baseOffset = (GameManager.instance.player.transform.position.y);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

            shootTimer += Time.deltaTime;

            if (shootTimer >= shootRate)
            {
                Shoot();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if(other.CompareTag("Player"))
        {
            playerInRange = true;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(FlashRed());

        agent.SetDestination(GameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

    void Shoot()
    {
        shootTimer = 0;
        Instantiate(bullet, shootPos.position, transform.rotation);
    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
}
