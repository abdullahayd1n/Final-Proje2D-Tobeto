using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsScript : MonoBehaviour
{
    [SerializeField] private float targetFPS = 60;
    private float timeBetweenFrames;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenFrames = 1f / targetFPS;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Mathf.RoundToInt(targetFPS);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > timeBetweenFrames)
        {
            float delayTime = timeBetweenFrames - Time.deltaTime;
            if (delayTime > 0) StartCoroutine(WaitForNextFrame(delayTime));
        }

    }
    IEnumerator WaitForNextFrame(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
