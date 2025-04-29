using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
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

    public void PlayerUpgrade()
    {

    }

    public void MiningUpgrade()
    {

    }

    public void WeaponUpgrade()
    {

    }
}
