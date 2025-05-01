using TMPro;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] int maxClicks;
    int clicks;
    [SerializeField] TMP_Text costText;

    void Start()
    {
        costText.text = "Cost: " + cost.ToString() + " Gold";
        clicks = 0;
    }

    public void UpdateCostText()
    {
        if ((clicks < maxClicks) && (GameManager.instance.playerScript.Gold >= cost))
        {
            cost *= 2;
            costText.text = "Cost: " + cost.ToString() + " Gold";
            clicks++;
        }
        else if (clicks >= maxClicks)
        {
            costText.text = "";
        }
    }
}
