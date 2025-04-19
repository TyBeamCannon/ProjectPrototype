using UnityEngine;

public class Ore : MonoBehaviour, IMine
{
    public enum OreType { Gold, Crystal };
    [SerializeField] OreType type;
    [SerializeField] int oreAmount;

    public void Mine(int damage)
    {
        int actualDamage = Mathf.Min(damage, oreAmount);
        oreAmount -= actualDamage;

        switch (type)
        {
            case OreType.Crystal:
                GameManager.instance.UpdateCrystalCount(-actualDamage);
                break;
            case OreType.Gold:
                GameManager.instance.UpdateGoldCount(-actualDamage);
                break;
        }


        if (oreAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int OreAmount => oreAmount;

}
