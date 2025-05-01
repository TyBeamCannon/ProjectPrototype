using TMPro;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] TMP_Text costText;

    public int Cost { get { return cost; } set { cost = value; } }
    public TMP_Text CostText { get { return costText; } set { costText = value; } }
}
