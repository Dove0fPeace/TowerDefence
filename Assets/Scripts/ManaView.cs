using UnityEngine;
using UnityEngine.UI;

public class ManaView : MonoBehaviour
{
    [SerializeField] private Image m_Mask;
    private float m_OriginalSize;

    private void Start()
    {
        m_OriginalSize = m_Mask.rectTransform.rect.width;
        TD_Player.ManaUpdateSubscribe(SetValue);
    }

    private void SetValue(int currentMana)
    {
        float value = (float)currentMana / (float)TD_Player.Instance.MaxMana;
        m_Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_OriginalSize * value);
    }

    private void OnDestroy()
    {
        TD_Player.ManaUpdateUnSubscribe(SetValue);
    }
}

