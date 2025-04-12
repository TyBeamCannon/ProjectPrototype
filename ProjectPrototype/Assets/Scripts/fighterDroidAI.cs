using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class fighterDroidAI : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;

    [SerializeField] Transform[] shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;

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
    public void takeDamage(int amount)
    {
        HP -= amount;
        StartCoroutine(FlashRed());

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
        int shootSide =  Random.Range(0, shootPos.Length);
        Instantiate(bullet, shootPos[shootSide].position, transform.rotation);
    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
}
