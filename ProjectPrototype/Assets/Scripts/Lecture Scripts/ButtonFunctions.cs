using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    UpgradeShop shop;
    int shopCostMulti;

    void Start()
    {
        shop = GameObject.FindWithTag("Shop").GetComponent<UpgradeShop>();
        shopCostMulti = 2;

    }


    public void Resume()
    {
        GameManager.instance.StateUnpause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.StateUnpause();
    }

    public void General()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuGeneral);
    }

    public void Audio()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuAudio);
    }

    public void Video()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuVideo);
    }

    public void Back()
    {
        GameManager.instance.CloseMenu();
    }

    // Make this take in a paramater of either strings or scene, based on the scene load that scene
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public void Player()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuPlayerUpgrade);
    }

    public void PlayerSpeed()
    {
        if ((shop.PSMCurrUpgradeCount < shop.PSMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.PSMCost))
        {
            GameManager.instance.UpdateGoldCount(-shop.PSMCost);
            GameManager.instance.playerScript.PlayerSpeed *= shop.PlayerSpeedMultiplier;
            shop.PSMCurrUpgradeCount++;
            shop.PSMCost *= shopCostMulti;
        }
        else if (shop.PSMCurrUpgradeCount >= shop.PSMMaxUpgradeAmount)
        {
            Debug.Log("Max Upgrades");
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.PSMCost)
        {
            Debug.Log("No Gold");
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void PlayerMaxGold()
    {
        if((shop.PMGCMCurrUpgradeCount < shop.PMGCMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.PMGCMCost))
        {
            GameManager.instance.playerScript.MaxGoldCarry *= shop.PlayerMaxGoldCarryMultiplier;
            shop.PMGCMCurrUpgradeCount++;
            shop.PMGCMCost *= shopCostMulti;
            GameManager.instance.UpdateMaxCarryCount();
            GameManager.instance.UpdateGoldCount(-shop.PMGCMCost);
        }
        else if (shop.PMGCMCurrUpgradeCount >= shop.PMGCMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.PMGCMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void PlayerMaxCrystal()
    {
        if ((shop.PMCCMCurrUpgradeCount < shop.PMCCMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.PMCCMCost))
        {
            GameManager.instance.playerScript.MaxCrystalCarry *= shop.PlayerMaxCrystalCarryMultiplier;
            shop.PMCCMCurrUpgradeCount++;
            shop.PMCCMCost *= shopCostMulti;
            GameManager.instance.UpdateMaxCarryCount();
            GameManager.instance.UpdateGoldCount(-shop.PMCCMCost);
        }
        else if (shop.PMCCMCurrUpgradeCount >= shop.PMCCMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.PMCCMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    } 

    public void PlayerPingCooldown()
    {
        if ((shop.PCDCurrUpgradeCount < shop.PCDMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.PCDCost))
        {
            shop.PCDCurrUpgradeCount++;
            shop.PCDCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().PingCooldown /= shop.PingCooldownDivider;
            GameManager.instance.UpdateGoldCount(-shop.PCDCost);
        }
        else if (shop.PCDCurrUpgradeCount >= shop.PCDMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.PCDCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void Mining()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuMiningUpgrade);
    }

    public void MiningSpeed()
    {
        if ((shop.MSMCurrUpgradeCount < shop.MSMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.MSMCost))
        {
            shop.MSMCurrUpgradeCount++;
            shop.MSMCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().MiningSpeed *= shop.MiningSpeedMultiplier;
            GameManager.instance.UpdateGoldCount(-shop.MSMCost);
        }
        else if (shop.MSMCurrUpgradeCount >= shop.MSMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.MSMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void MiningStrength()
    {
        if((shop.MSIBCurrUpgradeCount < shop.MSIBMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.MSIBCost))
        {
            shop.MSIBCurrUpgradeCount++;
            shop.MSIBCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().MiningStrength++;
            GameManager.instance.UpdateGoldCount(-shop.MSIBCost);
        }
        else if (shop.MSIBCurrUpgradeCount >= shop.MSIBMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.MSIBCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void Weapon()
    {
        GameManager.instance.CloseMenu();
        GameManager.instance.AddMenuToList(GameManager.instance.MenuWeaponUpgrade);
    }

    public void WeaponDamage()
    {
        if ((shop.WDMCurrUpgradeCount < shop.WDMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.WDMCost))
        {
            shop.WDMCurrUpgradeCount++;
            shop.WDMCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().WeaponDamage += shop.WeaponDamageMultiplier;
            GameManager.instance.UpdateGoldCount(-shop.WDMCost);
        }
        else if (shop.WDMCurrUpgradeCount >= shop.WDMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.WDMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void WeaponShootRate()
    {
        if ((shop.WSRMCurrUpgradeCount < shop.WSRMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.WSRMCost))
        {
            shop.WSRMCurrUpgradeCount++;
            shop.WSRMCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().ShootRate *= shop.WeaponShootRateMultiplier;
            GameManager.instance.UpdateGoldCount(-shop.WSRMCost);
        }
        else if (shop.WSRMCurrUpgradeCount >= shop.WSRMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.WSRMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }

    public void WeaponRange()
    {
        if((shop.WRMCurrUpgradeCount < shop.WRMMaxUpgradeAmount) && (GameManager.instance.playerScript.Gold >= shop.WRMCost))
        {
            shop.WRMCurrUpgradeCount++;
            shop.WRMCost *= shopCostMulti;
            GameManager.instance.player.GetComponent<MineTool>().Range *= shop.WeaponRangeMultiplier;
            GameManager.instance.UpdateGoldCount(-shop.WRMCost);
        }
        else if (shop.WRMCurrUpgradeCount >= shop.WRMMaxUpgradeAmount)
        {
            StartCoroutine(GameManager.instance.MaxUpgrades());
        }
        else if (GameManager.instance.playerScript.Gold < shop.WRMCost)
        {
            StartCoroutine(GameManager.instance.NotEnoughGold());
        }
    }
}
