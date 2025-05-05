using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            speed = 0.001f;
        }
        else
        {
            speed = 0.01f;
        }
        transform.position = new Vector3(transform.position.x + speed,transform.position.y);
        if (transform.position.x > 39)
        {
            transform.position = new Vector3(-38,transform.position.y);
        }
    }
}
