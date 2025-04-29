using UnityEngine;

public class UpgradeShop : MonoBehaviour, IInteract
{
    enum upgradeType { player, mining, weapon };

    [SerializeField] upgradeType type;

    [Header("---- Player Upgrades ----")]
    [SerializeField] float playerSpeedMultiplier;
    [SerializeField] int psmMaxUpgradeAmount;
    [SerializeField] float pingCooldownDivider;
    [SerializeField] int pcdmMaxUpgradeAmount;

    [Header("---- Mining Upgrades ----")]
    [SerializeField] float miningSpeedMultiplier;
    [SerializeField] int msmMaxUpgradeAmount;
    [SerializeField] int miningStrengthIncreaseBy;
    [SerializeField] int msMaxUpgradeAmount;

    [Header("---- Weapon Upgrades ----")]
    [SerializeField] float weaponDamageMultiplier;
    [SerializeField] int wdmMaxUpgradeAmount;
    [SerializeField] float weaponShootRateMultiplier;
    [SerializeField] int wsrmMaxUpgradeAmount;
    [SerializeField] float weaponRangeMultiplier;
    [SerializeField] int wrmMaxUpgradeAmount;

    public void Interact()
    {

    }

    float PlayerSpeedMultiplier { get { return playerSpeedMultiplier; } }
    float PingCooldownDivider { get { return pingCooldownDivider;} }
    float MiningSpeedMultiplier { get { return miningSpeedMultiplier; } }
    int MiningStrengthIncreaseBy { get { return miningStrengthIncreaseBy; } }
    float WeaponDamageMultiplier { get { return weaponDamageMultiplier; } }
    float WeaponShootRateMultiplier { get {return weaponShootRateMultiplier;} }
    float WeaponRangeMultiplier { get { return weaponRangeMultiplier; } }
}
