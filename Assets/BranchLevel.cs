using UnityEngine;
using TMPro;

[RequireComponent(typeof(MapLevel))]
public class BranchLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text m_PointText;
    [SerializeField] private MapLevel m_RootLevel;
    
    [SerializeField] private int m_NeedPoints;

    public void TryActivate()
    {
        gameObject.SetActive(m_RootLevel.IsComplete);
        if (m_NeedPoints > MapCompletion.Instance.TotalScore)
        {
            m_PointText.text = m_NeedPoints.ToString();
        }
        else
        {
            m_PointText.transform.parent.gameObject.SetActive(false);
        }
    }
}
