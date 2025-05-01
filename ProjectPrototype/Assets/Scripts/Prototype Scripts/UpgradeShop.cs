using UnityEngine;

public class UpgradeShop : MonoBehaviour, IInteract
{

    [Header("---- Player Upgrades ----")]

    [SerializeField] float playerSpeedMultiplier;
    [SerializeField] int psmMaxUpgradeAmount;
    int psmCurrUpgradeCount;
    [SerializeField] int psmCost;

    [SerializeField] int playerMaxGoldCarryMultiplier;
    [SerializeField] int pmgcmMaxUpgradeAmount;
    int pmgcmCurrUpgradeCount;
    [SerializeField] int pmgcmCost;

    [SerializeField] int playerMaxCrystalCarryMultiplier;
    [SerializeField] int pmccmMaxUpgradeAmount;
    int pmccmCurrUpgradeCount;
    [SerializeField] int pmccmCost;

    [SerializeField] float pingCooldownDivider;
    [SerializeField] int pcdMaxUpgradeAmount;
    int pcdCurrUpgradeCount;
    [SerializeField] int pcdCost;

    [Header("---- Mining Upgrades ----")]

    [SerializeField] float miningSpeedMultiplier;
    [SerializeField] int msmMaxUpgradeAmount;
    int msmCurrUpgradeCount;
    [SerializeField] int msmCost;

    [SerializeField] int miningStrengthIncreaseBy;
    [SerializeField] int msibMaxUpgradeAmount;
    int msibCurrUpgradeCount;
    [SerializeField] int msibCost;

    [Header("---- Weapon Upgrades ----")]

    [SerializeField] int weaponDamageMultiplier;
    [SerializeField] int wdmMaxUpgradeAmount;
    int wdmCurrUpgradeCount;
    [SerializeField] int wdmCost;

    [SerializeField] float weaponShootRateMultiplier;
    [SerializeField] int wsrmMaxUpgradeAmount;
    int wsrmCurrUpgradeCount;
    [SerializeField] int wsrmCost;

    [SerializeField] float weaponRangeMultiplier;
    [SerializeField] int wrmMaxUpgradeAmount;
    int wrmCurrUpgradeCount;
    [SerializeField] int wrmCost;

    void Start()
    {
        psmCurrUpgradeCount = pmgcmCurrUpgradeCount = pmccmCurrUpgradeCount = pcdCurrUpgradeCount =
        msmCurrUpgradeCount = msibCurrUpgradeCount =
        wdmCurrUpgradeCount = wsrmCurrUpgradeCount = wrmCurrUpgradeCount = 0;
    }

    public void Interact()
    {
        GameManager.instance.StatePause(GameManager.instance.MenuPlayerUpgrade);
    }

    public float PlayerSpeedMultiplier { get { return playerSpeedMultiplier; } }
    public int PSMMaxUpgradeAmount { get  { return psmMaxUpgradeAmount; } }
    public int PSMCurrUpgradeCount { get { return psmCurrUpgradeCount; } set { psmCurrUpgradeCount = value; } }
    public int PSMCost { get { return psmCost; } set { psmCost = value; } }
    public int PlayerMaxGoldCarryMultiplier { get { return playerMaxGoldCarryMultiplier;} }
    public int PMGCMMaxUpgradeAmount { get { return pmgcmMaxUpgradeAmount; } }
    public int PMGCMCurrUpgradeCount { get { return pmgcmCurrUpgradeCount; } set { pmgcmCurrUpgradeCount = value; } }
    public int PMGCMCost {  get { return pmgcmCost; } set { pmgcmCost = value; } }
    public int PlayerMaxCrystalCarryMultiplier { get { return playerMaxCrystalCarryMultiplier;} }
    public int PMCCMMaxUpgradeAmount { get { return pmccmMaxUpgradeAmount; } }
    public int PMCCMCurrUpgradeCount { get { return pmccmCurrUpgradeCount; } set {  pmccmCurrUpgradeCount = value; } }
    public int PMCCMCost { get { return pmccmCost; } set { pmccmCost = value; } }
    public float PingCooldownDivider { get { return pingCooldownDivider;} }
    public int PCDMaxUpgradeAmount { get { return pcdMaxUpgradeAmount; } }
    public int PCDCurrUpgradeCount { get { return pcdCurrUpgradeCount; } set { pcdCurrUpgradeCount = value; } }
    public int PCDCost { get { return pcdCost; } set { pcdCost = value; } }
    public float MiningSpeedMultiplier { get { return miningSpeedMultiplier; } }
    public int MSMMaxUpgradeAmount { get { return msmMaxUpgradeAmount; } }
    public int MSMCurrUpgradeCount { get { return msmCurrUpgradeCount; } set {  msmCurrUpgradeCount = value; } }
    public int MSMCost { get { return msmCost; } set { msmCost = value; } }
    public int MiningStrengthIncreaseBy { get { return miningStrengthIncreaseBy; } }
    public int MSIBMaxUpgradeAmount { get { return msibMaxUpgradeAmount; } }
    public int MSIBCurrUpgradeCount { get { return msibCurrUpgradeCount; } set {  msibCurrUpgradeCount = value; } }
    public int MSIBCost { get { return msibCost; } set { msibCost = value; } }
    public int WeaponDamageMultiplier { get { return weaponDamageMultiplier; } }
    public int WDMMaxUpgradeAmount { get { return wdmMaxUpgradeAmount; } }
    public int WDMCurrUpgradeCount { get { return wdmCurrUpgradeCount; } set {  wdmCurrUpgradeCount = value; } }
    public int WDMCost { get { return wdmCost; } set { wdmCost = value; } }
    public float WeaponShootRateMultiplier { get {return weaponShootRateMultiplier;} }
    public int WSRMMaxUpgradeAmount { get { return wsrmMaxUpgradeAmount; } }
    public int WSRMCurrUpgradeCount { get { return wsrmCurrUpgradeCount; } set {  wsrmCurrUpgradeCount = value; } }
    public int WSRMCost { get {return wsrmCost; } set { wsrmCost = value; } }
    public float WeaponRangeMultiplier { get { return weaponRangeMultiplier; } }
    public int WRMMaxUpgradeAmount { get { return wrmMaxUpgradeAmount; } }
    public int WRMCurrUpgradeCount { get { return wsrmCurrUpgradeCount; } set { wsrmCurrUpgradeCount = value; } }
    public int WRMCost { get { return wrmCost; } set { wrmCost = value; } }
}
