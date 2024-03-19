using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itmeScript : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component is missing on the object named " + gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("itme"))
        {
            anim.SetBool("itme", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("itme"))
        {
            anim.SetBool("itme", false);
        }
        else
        {
            anim.SetBool("walk", true);
        }
    }
}

