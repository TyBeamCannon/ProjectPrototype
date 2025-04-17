using Unity.VisualScripting;
using UnityEngine;

public class oreBehavior : MonoBehaviour, mineable
{
    [SerializeField] int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MineHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.instance.UpdateGameGoal(-1);
            gameManager.instance.UpdateOreMeter();
        }
    }
}
