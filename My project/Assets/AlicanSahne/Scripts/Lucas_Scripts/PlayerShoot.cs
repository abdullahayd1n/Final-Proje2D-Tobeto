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

    public Image cooldownImage; // UI Image

    private Coroutine cooldownCoroutine; // Coroutine referans�

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

            // E�er bir so�uma i�lemi varsa, iptal et
            if (cooldownCoroutine != null)
                StopCoroutine(cooldownCoroutine);

            // FillAmount de�erini s�f�rla ve 3 saniyede 1'e kadar art�r
            cooldownImage.fillAmount = 0f;
            cooldownCoroutine = StartCoroutine(IncreaseFillAmountOverTime(shootInterval));
        }
    }

    IEnumerator ResetCanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
    }

    IEnumerator IncreaseFillAmountOverTime(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cooldownImage.fillAmount = elapsedTime / duration;
            yield return null;
        }
        cooldownImage.fillAmount = 1f;
    }
}
