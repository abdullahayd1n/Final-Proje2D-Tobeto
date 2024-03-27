using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{
    public float destrucTime;
    private float timer;
    void Start()
    {
        timer = destrucTime;
    }

 
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            Destroy(gameObject);
        }
    }
}
