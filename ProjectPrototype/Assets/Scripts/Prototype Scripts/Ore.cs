using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ore : MonoBehaviour, IMine
{
    enum oreType { gold, crystal };
    [SerializeField] oreType type;

    [SerializeField] int oreAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == oreType.crystal)
        {
            GameManager.instance.UpdateCrystalCount(oreAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mine(int damage)
    {
        oreAmount -= damage;

        if (type == oreType.crystal)
        {
            if (damage > oreAmount)
            {
                GameManager.instance.UpdateCrystalCount(-oreAmount);
            }
            else
            {
                GameManager.instance.UpdateCrystalCount(-damage);
            }
        }

        if (oreAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int OreAmount { get { return oreAmount; } }

}
