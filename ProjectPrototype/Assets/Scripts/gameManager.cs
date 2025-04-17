using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("---- Menus ----")]

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuSettings;
    [SerializeField] TMP_Text gameGoalCountText;

    public Image playerHPBar;
    public GameObject playerDamageScreen;

    public bool isPaused;

    [Header("---- Player ----")]

    public GameObject player;
    public playerController playerScript;

    [Header("---- Meters ----")]
    [SerializeField] Image healthBar;
    [SerializeField] Image oreMeter;

    float timeScaleOrig;

    int gameGoalCount;
    int goalCountOrig;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<playerController>();

        //GameObject.FindWithTag("PostProcess").GetComponent<PostProcessVolume>().profile.GetSetting<Grain>().intensity.value = Camera.main.GetComponent<AudioSource>().volume = 0.01f;
        

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

    public void UpdateGameGoal(int amount)
    {
        gameGoalCount += amount;
        gameGoalCountText.text = gameGoalCount.ToString("F0");

        if (amount > 0)
        {
            goalCountOrig += amount;
        }


        if (gameGoalCount <= 0)
        {
            // You Won !
            StatePause(menuWin);
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

    public void UpdateOreMeter()
    {
        oreMeter.fillAmount = 1 - ((float)gameGoalCount / goalCountOrig);
    }
}
