using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SAALLAN : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float angle = 20.0f;

    private float currentAngle = 0;
    private float time;


    private void Update()
    {
        time += Time.deltaTime * speed;
        float angle =math.sin(time)* this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + currentAngle));
    }
}
