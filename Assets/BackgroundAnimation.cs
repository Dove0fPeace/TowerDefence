using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform m_StartPosition;
    [SerializeField] private RectTransform m_FinishPosition;

    [SerializeField] private float m_Speed;

    private RectTransform _myTransform;

    private void Start()
    {
        _myTransform = GetComponent<RectTransform>();
        print(m_FinishPosition.anchoredPosition);
    }

    private void Update()
    {
        _myTransform.Translate(m_FinishPosition.localPosition * (Time.deltaTime * m_Speed), Space.World);
        if (_myTransform.position == m_FinishPosition.position)
        {
            _myTransform.position = m_StartPosition.position;
        }
    }
}
