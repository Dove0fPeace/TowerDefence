using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    [SerializeField] private int m_Scene;

    public void LoadScene()
    {
        SceneManager.LoadScene(m_Scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
