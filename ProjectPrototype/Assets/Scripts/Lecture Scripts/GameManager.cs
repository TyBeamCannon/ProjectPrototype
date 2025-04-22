using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("---- Menus ----")]
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuSettings;
    [SerializeField] TMP_Text gameGoalCountText;

    public GameObject playerDamageScreen;

    public bool isPaused;

    [Header("---- Player ----")]
    [SerializeField] public GameObject player;
    [SerializeField] public ZeroG playerScript;

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
            if (menuActive == null)
            {
                StatePause(menuPause);
            }
            else if (menuActive == menuPause)
            {
                StateUnpause();
            }
            else if(menuActive == menuSettings)
            {
                SwapSettingsScreen();
            }

        }

        

    }

    public void StatePause(GameObject menuToActivate)
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuActive = menuToActivate;
        menuActive.SetActive(true);
    }

    public void StateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(false);
        menuActive = null;
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

    public void SwapSettingsScreen()
    {
        if (menuActive == menuPause)
        {
            menuActive.SetActive(false);
            menuActive = null;
            menuActive = menuSettings;
            menuActive.SetActive(true);
        }
        else if (menuActive == menuSettings)
        {
            menuActive.SetActive(false);
            menuActive = null;
            menuActive = menuPause;
            menuActive.SetActive(true);
        }
    }

    

    

    public bool IsPaused { get { return isPaused; } }
}
