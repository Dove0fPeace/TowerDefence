using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform _rect;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        BuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);
    }

    private void MoveToBuildSite(Transform buildSite)
    {
        if (buildSite)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.position);
            _rect.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
        {
            tbc.SetBuildSite(buildSite);
        }
    }

    private void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
