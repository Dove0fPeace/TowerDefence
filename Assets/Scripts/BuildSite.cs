using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static event Action<BuildSite> OnClickEvent;

    [SerializeField] private TowerAsset[] m_BuildableTowers;
    public TowerAsset[] BuildableTowers => m_BuildableTowers;

    public void SetBuildableTowers(TowerAsset[] towers)
    {
        if (towers == null || towers.Length == 0)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            m_BuildableTowers = towers;
        }
    }

    public static void HideControls()
    {
        OnClickEvent?.Invoke(null);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
}
