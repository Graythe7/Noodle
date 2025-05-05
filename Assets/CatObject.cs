using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatObject : MonoBehaviour
{
    [SerializeField] public int Weight;
    Animator animator;

    void Start()
    {
        GameManager.instance.SetCat(this);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.GetBed().transform.position.Equals(transform.position))
        {   
            Invoke(nameof(PlayWinAnimation),0.4f);
            enabled = false;
        }
    }

    void PlayWinAnimation()
    {
        animator.Play("Sleep");
        GameManager.instance.GetBed().GetComponent<Renderer>().enabled = false;
    }
    
    
}