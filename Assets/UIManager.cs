using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Text catWeight;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        catWeight.text = SceneManager.GetActiveScene().name + "\nCat Weight: " + GameManager.instance.GetCatWeight() +
                         "\nYour Weight: " + GameManager.instance.GetPlayerWeight();
    }
}