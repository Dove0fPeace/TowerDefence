using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeAsset m_Asset;
    [SerializeField] private Image m_UpgradeIcon;
    [SerializeField] private TMP_Text m_Level, m_Cost;
    [SerializeField] private Button m_BuyButton;

    private int savedLevel;
    private int costNumber;

    public void Initialize()
    {
        m_UpgradeIcon.sprite = m_Asset.Sprite;
        savedLevel = Upgrades.GetUpgradeLevel(m_Asset);
        if (savedLevel >= m_Asset.MaxLevel)
        {
            m_Level.text = $"Lvl: {savedLevel + 1}(max)";
            m_BuyButton.interactable = false;
            m_Cost.text = "X";
            costNumber = int.MaxValue;

        }
        else
        {
            m_Level.text = "Lvl: " + (savedLevel+1);
            m_BuyButton.interactable = true;
            costNumber = m_Asset.CostByLevel[savedLevel];
            m_Cost.text = costNumber.ToString();
        }
    }
    
    public void Buy()
    {
        Upgrades.BuyUpgrade(m_Asset);
        Initialize();
    }

    public void CheckCost(int money)
    {
        m_BuyButton.interactable = money >= costNumber;
    }
}
