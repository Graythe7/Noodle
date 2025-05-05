using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private int buttonNum;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OnMouseEnter()
    {
        animator.Play("MouseJoin");
    }

    private void OnMouseExit()
    {
        animator.Play("Idle");
    }
}