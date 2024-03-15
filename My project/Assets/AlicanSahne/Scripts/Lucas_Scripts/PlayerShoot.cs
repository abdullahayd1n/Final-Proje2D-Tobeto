using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    Animator anim;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    AudioSource audioSource;

    // Atýþ aralýðýný kontrol etmek için zamanlayýcý
    private float nextShootTime = 0f;
    public float shootInterval = 1f; // 3 saniye aralýk

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Atýþ aralýðýný kontrol et ve animasyonun oynatýlma durumunu kontrol et
        if (Time.time >= nextShootTime && Keyboard.current.tKey.wasPressedThisFrame && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Shoot(); // Atýþ yap
            audioSource.Play();
            nextShootTime = Time.time + shootInterval; // Bir sonraki atýþ zamanýný güncelle
        }
    }

    public void Shoot()
    {

        anim.SetTrigger("shoot");
        CameraShake.instance.shakeCamera(25f, 3f);




        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        StartCoroutine(ResetTimeScaleAfterAnimation());

    }

    IEnumerator ResetTimeScaleAfterAnimation()
    {
        // Bu animasyonun bitiþini bekler
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Animasyon bittiðinde zamaný normale çevir
        Time.timeScale = 1f;
    }

}