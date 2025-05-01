using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [Header("-- Animator --")]
    [SerializeField] Animator doorController;

    [Header("-- Doors --")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;


    

    public void OnDoorTrigger(bool isTriggered)
    {
        doorController.SetBool("In Range", isTriggered);
    }

    public void OnPlayerGravTrigger(bool isTriggered)
    {
        GameManager.instance.playerScript.PlayerGrav(isTriggered);
        GameManager.instance.GravModel = isTriggered;
        GameManager.instance.SpaceModel = !isTriggered;
    }
}
