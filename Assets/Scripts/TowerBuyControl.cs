using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class TowerBuyControl : MonoBehaviour
{
    [SerializeField] private TowerAsset m_TowerAsset;
    [SerializeField] private TMP_Text m_CostText;
    [SerializeField] private Button m_Button;
    [SerializeField] private Transform m_BuildSite;
    public void SetBuildSite (Transform value)
    {
         m_BuildSite = value;
    }

    private void Start()
    {
        TD_Player.GoldUpdateSubscribe(GoldStatusCheck);
        m_CostText.text = m_TowerAsset.GoldCost.ToString();
        m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
    }

    private void GoldStatusCheck(int gold)
    {
        if (gold >= m_TowerAsset.GoldCost != m_Button.interactable)
        {
            m_Button.interactable = !m_Button.interactable;
            m_CostText.color = m_Button.interactable ? Color.white : Color.red;
        }
    }

    public void Buy()
    {
        TD_Player.Instance.TryBuild(m_TowerAsset, m_BuildSite);
        BuildSite.HideControls();
    }
}
