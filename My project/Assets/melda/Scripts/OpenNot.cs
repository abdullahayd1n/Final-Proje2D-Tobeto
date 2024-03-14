using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNot : MonoBehaviour
{
    public GameObject notObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& notObject != null)
        {
            notObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& notObject != null)
        {
            notObject.SetActive(false);
        }
    }
}
