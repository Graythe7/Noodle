using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddBlock(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
