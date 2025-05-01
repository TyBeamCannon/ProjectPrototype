using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] MineNWeapon tool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IPickup pickupable = other.GetComponent<IPickup>();
        
        if (pickupable != null )
        {
            pickupable.GetToolStats(tool);
            Destroy(gameObject);
        }
    }
}
