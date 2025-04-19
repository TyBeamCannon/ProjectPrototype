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
    [SerializeField] int maxGoldCarry;

    [Header("---- Meters ----")]
    [SerializeField] public Image healthMeter;
    [SerializeField] public Image crystalMeter;
    [SerializeField] public Image goldMeter;
    [SerializeField] int crystalCount;
    [SerializeField] int crystalCountOrig;
    [SerializeField] int goldCount;
    [SerializeField] int maxCrystalGoal;
    [SerializeField] float crystalDisplaySpeed = 2f;
    [SerializeField] float crystalDisplayAmount = 0f;
    [SerializeField] float crystalDisplayFillAmount = 0f;

    float timeScaleOrig;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<ZeroG>();

        GameObject.FindWithTag("PostProcess").GetComponent<PostProcessVolume>().profile.GetSetting<Grain>().intensity.value = Camera.main.GetComponent<AudioSource>().volume = 0.01f;
        

        timeScaleOrig = Time.timeScale;

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

        crystalDisplayFillAmount = Mathf.Lerp(crystalDisplayFillAmount, (float)crystalCount / crystalCountOrig, Time.deltaTime * crystalDisplaySpeed);
        crystalMeter.fillAmount = crystalDisplaySpeed;

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
        crystalCount += amount;

        if (amount > 0)
        {
            crystalCountOrig += amount;
        }

        crystalCount = Mathf.Clamp(crystalCount, 0 , maxCrystalGoal);

        if (crystalCount >= maxCrystalGoal)
        {
            StatePause(menuWin);
        }


    }

    public void UpdateGoldCount(int amount)
    {
        goldCount += amount;
        goldCount = Mathf.Max(0, goldCount);

        if (goldMeter != null && maxGoldCarry > 0)
        {
            goldMeter.fillAmount = (float)goldCount / maxGoldCarry;
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
