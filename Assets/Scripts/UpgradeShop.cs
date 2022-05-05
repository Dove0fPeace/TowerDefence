using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private int m_Money;
    [SerializeField] private TMP_Text m_MoneyText;

    [SerializeField] private BuyUpgrade[] m_Sales;
    
    private int CheatedMoney = 0;
    private bool cheat = false;

    private void Start()
    {
        foreach (var slot in m_Sales)
        {
            slot.Initialize();
            slot.transform.Find(nameof(Button)).GetComponent<Button>().onClick.AddListener(UpdateMoney);
        }
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        m_Money = MapCompletion.Instance.TotalScore;
        if (cheat)
            m_Money += CheatedMoney;
        m_Money -= Upgrades.GetTotalCost();
        
        m_MoneyText.text = m_Money.ToString();

        foreach (var slot in m_Sales)
        {
            slot.CheckCost(m_Money);
        }
    }

    public void AddCheatMoney()
    {
        cheat = true;
        CheatedMoney += 10;
        UpdateMoney();
    }
}
