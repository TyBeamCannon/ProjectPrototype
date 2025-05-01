using UnityEngine;

public class Ore : MonoBehaviour, IMine
{
    public enum OreType { Gold, Crystal };
    [SerializeField] OreType type;
    [SerializeField] int oreAmount;

    public void Mine(int damage)
    {
        int actualDamage = Mathf.Min(damage, oreAmount);

        switch (type)
        {
            case OreType.Crystal:
                if (GameManager.instance.playerScript.Crystal < GameManager.instance.playerScript.MaxCrystalCarry)
                {
                    oreAmount -= actualDamage;
                    GameManager.instance.UpdateCrystalCount(actualDamage);
                }
                break;
            case OreType.Gold:
                if (GameManager.instance.playerScript.Gold < GameManager.instance.playerScript.MaxGoldCarry)
                {
                    oreAmount -= actualDamage;
                    GameManager.instance.UpdateGoldCount(actualDamage);
                }
                break;
        }


        if (oreAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int OreAmount => oreAmount;

}
