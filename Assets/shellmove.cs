using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellmove : MonoBehaviour
{
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0,Time.deltaTime * speed/2,Time.deltaTime * speed);
    }
}
