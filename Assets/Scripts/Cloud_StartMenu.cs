using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_StartMenu : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = Random.Range(0.12f, 0.2f);
    }

}
