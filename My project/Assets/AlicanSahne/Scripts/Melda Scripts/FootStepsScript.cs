using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsScript : MonoBehaviour
{
    public GameObject footsteps;

    void Start()
    {
        footsteps.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            ActivateFootsteps();
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            DeactivateFootsteps();
        }
    }

    void ActivateFootsteps()
    {
        footsteps.SetActive(true);
    }

    void DeactivateFootsteps()
    {
        footsteps.SetActive(false);
    }
}