using UnityEngine;

public class mineable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int health;

    public void MineHit(int damage)
    {
        health -= damage;
        if(health <= 0 )
        {
            Destroy(gameObject);
        }
    }


}
