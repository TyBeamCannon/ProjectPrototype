using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    SpaceShipController controller;


    void Start()
    {
        controller = GetComponentInParent<SpaceShipController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        else
            Debug.Log("Doors Entered");

        controller.OnDoorTrigger(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;
        else
            Debug.Log("Doors Exited");

        controller.OnDoorTrigger(false);
    }
}
