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

    // At�� aral���n� kontrol etmek i�in zamanlay�c�
    private float nextShootTime = 0f;
    public float shootInterval = 1f; // 3 saniye aral�k

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // At�� aral���n� kontrol et ve animasyonun oynat�lma durumunu kontrol et
        if (Time.time >= nextShootTime && Keyboard.current.tKey.wasPressedThisFrame && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Shoot(); // At�� yap
            audioSource.Play();
            nextShootTime = Time.time + shootInterval; // Bir sonraki at�� zaman�n� g�ncelle
        }
    }

    void Shoot()
    {
        anim.SetTrigger("shoot");
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
