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
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            playerDir = (gameManager.instance.player.transform.position - transform.position);

            agent.SetDestination(gameManager.instance.player.transform.position);

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
        if(other.CompareTag("Player"))
        {
            playerInRange = true;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void takeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(FlashRed());

        agent.SetDestination(gameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
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
