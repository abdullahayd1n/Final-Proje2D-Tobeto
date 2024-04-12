using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoPlayerKapat : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool hasStarted = false;

    void Start()
    {
        // Video oynatıcının loop özelliğini devre dışı bırak
        videoPlayer.isLooping = false;
    }

    void Update()
    {
        // Video oynatıcının başlatılmasını kontrol et
        if (!hasStarted && videoPlayer.isPlaying)
        {
            hasStarted = true;
            Invoke("CloseVideoPlayer", 5f); // 5 saniye sonra CloseVideoPlayer fonksiyonunu çağır
        }
    }

    void CloseVideoPlayer()
    {
        // Video oynatıcıyı kapat
        videoPlayer.Stop();
        gameObject.SetActive(false); // Bu scriptin bulunduğu GameObject'i devre dışı bırak
    }
}