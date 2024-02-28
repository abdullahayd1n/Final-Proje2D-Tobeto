using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OrtakDamageTexti : MonoBehaviour
{
    public float DestroyTime;
    private float timer;

     void Start()
    {
        timer = DestroyTime;
    }

     void Update()
    {
        timer -= Time.deltaTime;
        if(timer<= 0)
        {
            Destroy(gameObject);
        }
    }
}
