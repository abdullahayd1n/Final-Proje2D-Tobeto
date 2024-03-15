using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    Animator anim;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    AudioSource audioSource;

    private bool canShoot = true; // At�� yap�labilir durumu kontrol eder

    public float shootInterval = 3f; // 3 saniye aral�k

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (canShoot && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            anim.SetTrigger("shoot");
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            audioSource.Play();
            StartCoroutine(ResetCanShoot());
        }
    }

    IEnumerator ResetCanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
    }
}
