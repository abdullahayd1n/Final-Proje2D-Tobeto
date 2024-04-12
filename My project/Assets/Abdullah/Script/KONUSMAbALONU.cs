using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KONUSMAbALONU : MonoBehaviour
{
    public Canvas konusmaCanvas;
    public GameObject konusmabox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(konusmaCanvas != null)
            konusmaCanvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (konusmaCanvas != null)
            konusmaCanvas.gameObject.SetActive(false);

        Destroy(konusmabox);
        
    }
}
