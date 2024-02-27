using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    Animator anim;
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    // Atýþ aralýðýný kontrol etmek için zamanlayýcý
    private float nextShootTime = 0f;
    public float shootInterval = 1f; // 3 saniye aralýk

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Atýþ aralýðýný kontrol et ve animasyonun oynatýlma durumunu kontrol et
        if (Time.time >= nextShootTime && Keyboard.current.tKey.wasPressedThisFrame && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Shoot(); // Atýþ yap
            nextShootTime = Time.time + shootInterval; // Bir sonraki atýþ zamanýný güncelle
        }
    }

    void Shoot()
    {
        anim.SetTrigger("shoot");
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
