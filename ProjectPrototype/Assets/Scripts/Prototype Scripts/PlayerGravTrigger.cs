using UnityEngine;

public class PlayerGravTrigger : MonoBehaviour
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
            Debug.Log("Grav Entered");

        controller.OnPlayerGravTrigger(true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;
        else
            Debug.Log("Grav Exited");

        controller.OnPlayerGravTrigger(false);
    }
}
