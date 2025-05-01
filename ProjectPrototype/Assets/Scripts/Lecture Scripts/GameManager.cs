using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("---- Menus ----")]
    [SerializeField] List<GameObject> menuList;
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuGeneral;
    [SerializeField] GameObject menuAudio;
    [SerializeField] GameObject menuVideo;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuPlayerUpgrade;
    [SerializeField] GameObject menuMiningUpgrade;
    [SerializeField] GameObject menuWeaponUpgrade;


    [SerializeField] TMP_Text gameGoalCountText;
    [SerializeField] GameObject maxUpgradesReached;
    [SerializeField] GameObject notEnoughGold;

    public GameObject playerDamageScreen;

    public bool isPaused;

    [Header("---- Player ----")]
    public GameObject player;
    public ZeroG playerScript;
    [SerializeField] GameObject reticle;

    int maxGoldPlayerCarry;
    int maxCrystalPlayerCarry;

    [Header("---- Meters ----")]
    public Image healthMeter;
    public Image crystalMeter;
    public Image goldMeter;

    [Header("")]

    [SerializeField] int maxCrystalGoal;
    [SerializeField] float crystalDisplaySpeed;
    [SerializeField] float crystalDisplayAmount;
    [SerializeField] float crystalDisplayFillAmount;

    float timeScaleOrig;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<ZeroG>();

        GameObject.FindWithTag("PostProcess").GetComponent<PostProcessVolume>().profile.GetSetting<Grain>().intensity.value = Camera.main.GetComponent<AudioSource>().volume = 0.01f;
        

        timeScaleOrig = Time.timeScale;
        

        maxGoldPlayerCarry = playerScript.MaxGoldCarry;
        maxCrystalPlayerCarry = playerScript.MaxCrystalCarry;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuList.Count == 0)
            {
                StatePause(menuGeneral);
            }
            else if (menuList.Count > 1)
            {
                CloseMenu();
            }
            else if (menuList.Count == 1)
            {
                StateUnpause();
            }

        }

        

    }

    public void AddMenuToList(GameObject menuToAdd)
    {
        menuList.Add(menuToAdd);
        if (menuActive != null && menuActive.activeSelf)
            menuActive.SetActive(false);
        menuActive = menuList[menuList.Count - 1];
        menuActive.SetActive(true);
    }

    public void CloseMenu()
    {
        menuActive.SetActive(false);
        menuActive = null;
        menuList.RemoveAt(menuList.Count - 1);
        if (menuList.Count > 0)
        {
            menuActive = menuList[menuList.Count - 1];
            menuActive.SetActive(true);
        }
    }

    public void StatePause(GameObject menu)
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        AddMenuToList(menu);
    }

    public void StateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(false);
        CloseMenu();
    }

    public void UpdateMaxCarryCount()
    {
        maxCrystalPlayerCarry = playerScript.MaxCrystalCarry;
        maxGoldPlayerCarry = playerScript.MaxGoldCarry;
    }

    public void UpdateCrystalCount(int amount)
    {
        playerScript.Crystal += amount;

        playerScript.Crystal = Mathf.Clamp(playerScript.Crystal, 0 , maxCrystalGoal);

        // The Lerp does not fill all the way when it should -Jeff

        //crystalDisplayFillAmount = Mathf.Lerp(crystalDisplayFillAmount, (float)playerScript.Crystal / playerScript.MaxCrystalCarry, Time.deltaTime * crystalDisplaySpeed);
        //crystalMeter.fillAmount = crystalDisplayFillAmount;

        crystalMeter.fillAmount = (float)playerScript.Crystal / maxCrystalPlayerCarry;

        if (playerScript.Crystal >= maxCrystalGoal)
        {
            StatePause(menuWin);
        }


    }

    public void UpdateGoldCount(int amount)
    {
        playerScript.Gold += amount;
        playerScript.Gold = Mathf.Max(0, playerScript.Gold);

        if (goldMeter != null && maxGoldPlayerCarry > 0)
        {
            goldMeter.fillAmount = (float)playerScript.Gold / maxGoldPlayerCarry;
        }
    }

    public void YouLose()
    {
        StatePause(menuLose);
    }


    public IEnumerator MaxUpgrades()
    {
        maxUpgradesReached.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        maxUpgradesReached.SetActive(false);
    }

    public IEnumerator NotEnoughGold()
    {
        notEnoughGold.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        notEnoughGold.SetActive(false);
    }


    public GameObject MenuGeneral { get { return menuGeneral; } }
    public GameObject MenuAudio { get { return menuAudio; } }
    public GameObject MenuVideo { get { return menuVideo; } }
    public GameObject MenuWin { get { return menuWin; } }
    public GameObject MenuLose { get { return menuLose; } }
    public GameObject MenuPlayerUpgrade { get { return menuPlayerUpgrade; } }
    public GameObject MenuMiningUpgrade {  get { return menuMiningUpgrade; } }
    public GameObject MenuWeaponUpgrade { get { return menuWeaponUpgrade; } }


    public bool IsPaused { get { return isPaused; } }
}
