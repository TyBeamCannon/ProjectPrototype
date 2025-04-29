using UnityEngine;

public class UpgradeShop : MonoBehaviour, IInteract
{
    enum upgradeType { player, mining, weapon };

    [SerializeField] upgradeType type;

    [Header("---- Player Upgrades ----")]
    [SerializeField] float playerSpeedMultiplier;
    [SerializeField] int psmMaxUpgradeAmount;
    [SerializeField] int psmCost;
    [SerializeField] int playerMaxGoldCarryMultiplier;
    [SerializeField] int pmgcmMaxUpgradeAmount;
    [SerializeField] int pmgcmCost;
    [SerializeField] int playerMaxCrystalCarryMultiplier;
    [SerializeField] int pmccmMaxUpgradeAmount;
    [SerializeField] int pmccmCost;
    [SerializeField] float pingCooldownDivider;
    [SerializeField] int pcdMaxUpgradeAmount;
    [SerializeField] int pcdCost;

    [Header("---- Mining Upgrades ----")]
    [SerializeField] float miningSpeedMultiplier;
    [SerializeField] int msmMaxUpgradeAmount;
    [SerializeField] int msmCost;
    [SerializeField] int miningStrengthIncreaseBy;
    [SerializeField] int msibMaxUpgradeAmount;
    [SerializeField] int msibCost;
    [SerializeField] int msMaxUpgradeAmount;
    [SerializeField] int msCost;

    [Header("---- Weapon Upgrades ----")]
    [SerializeField] float weaponDamageMultiplier;
    [SerializeField] int wdmMaxUpgradeAmount;
    [SerializeField] int wdmCost;
    [SerializeField] float weaponShootRateMultiplier;
    [SerializeField] int wsrmMaxUpgradeAmount;
    [SerializeField] int wsrmCost;
    [SerializeField] float weaponRangeMultiplier;
    [SerializeField] int wrmMaxUpgradeAmount;
    [SerializeField] int wrmCost;

    public void Interact()
    {
        GameManager.instance.StatePause(GameManager.instance.MenuPlayerUpgrade);
    }

    float PlayerSpeedMultiplier { get { return playerSpeedMultiplier; } }
    int PSMMaxUpgradeAmount { get  { return psmMaxUpgradeAmount; } }
    int PSMCost { get { return psmCost; } }
    int PlayerMaxGoldCarryMultiplier { get { return playerMaxGoldCarryMultiplier;} }
    int PMGCMaxUpgradeAmount { get { return pmgcmMaxUpgradeAmount; } }
    int PMGCCost {  get { return pmgcmCost; } }
    int PlayerMaxCrystalCarryMultiplier { get { return playerMaxCrystalCarryMultiplier;} }
    int PMCCMMaxUpgradeAmount { get { return pmccmMaxUpgradeAmount; } }
    int PMCCMCost { get { return pmccmCost; } }
    float PingCooldownDivider { get { return pingCooldownDivider;} }
    int PCDMaxUpgradeAmount { get { return pcdMaxUpgradeAmount; } }
    int PCDCost { get { return pcdCost; } }
    float MiningSpeedMultiplier { get { return miningSpeedMultiplier; } }
    int MSMMaxUpgradeAmount { get { return msmMaxUpgradeAmount; } }
    int MSMCost { get { return msmCost; } }
    int MiningStrengthIncreaseBy { get { return miningStrengthIncreaseBy; } }
    int MSIBMaxUpgradeAmount { get { return msibMaxUpgradeAmount; } }
    int MSIBCost { get { return msibCost; } }
    float WeaponDamageMultiplier { get { return weaponDamageMultiplier; } }
    int WDMMaxUpgradeAmount { get { return wdmMaxUpgradeAmount; } }
    int WDMCost { get { return wdmCost; } }
    float WeaponShootRateMultiplier { get {return weaponShootRateMultiplier;} }
    int WSRMMaxUpgradeAmount { get { return wsrmMaxUpgradeAmount; } }
    int WSRMCost { get {return wsrmCost; } }
    float WeaponRangeMultiplier { get { return weaponRangeMultiplier; } }
    int WRMMaxUpgradeAmount { get { return wrmMaxUpgradeAmount; } }
    int WRMCost { get { return wrmCost; } }
}
