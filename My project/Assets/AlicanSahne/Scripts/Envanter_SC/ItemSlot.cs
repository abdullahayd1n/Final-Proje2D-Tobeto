using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public bool isEmpty;
    public Image itemImage;
    private float buffDuration = 5f;

    void Start()
    {
        isEmpty = true;
        itemImage.enabled = false;
    }
    public void SetItem(Item i)
    {
        item = i;
        isEmpty = false;
        itemImage.enabled = true;
        itemImage.sprite = item.itemSprite;
    }
    public void UseItem()
    {
        if (item == null)
            return;

        StartCoroutine(item.itemName); // Start the coroutine using StartCoroutine

        item = null;
        isEmpty = true;
        itemImage.enabled = false;
    }

    public void HealthPoitions()
    {
        GameObject.Find("Lucas").GetComponent<LucasHealth>().Heal(15);  // hiyerarsideki lucas gameobjectini bulup lucashealth scriptini çekiyor
    }

    public IEnumerator DamageBuff()
    {
        MeleeAttack meleeAttack = GameObject.Find("Lucas").GetComponent<MeleeAttack>();
        if (meleeAttack != null)
        {
            meleeAttack.minDamage += 40;
            meleeAttack.maxDamage += 40;
            yield return new WaitForSeconds(buffDuration);
            meleeAttack.minDamage -= 40;
            meleeAttack.maxDamage -= 40;
        }
    }

    public IEnumerator GunBuff()
    {
        GameObject lucas = GameObject.Find("Lucas");
        if (lucas != null)
        {
            PlayerShoot playerShoot = lucas.GetComponentInChildren<PlayerShoot>();
            if (playerShoot != null)
            {
                Mermi bulletPrefab = playerShoot.bulletPrefab.GetComponent<Mermi>();
                if (bulletPrefab != null)
                {
                    bulletPrefab.minDamage += 40; // Minimum hasarý arttýr
                    bulletPrefab.maxDamage += 40; // Maksimum hasarý arttýr
                    yield return new WaitForSeconds(buffDuration); // Belirtilen süre boyunca bekle
                    bulletPrefab.minDamage -= 40; // Minimum hasarý eski haline getir
                    bulletPrefab.maxDamage -= 40; // Maksimum hasarý eski haline getir
                }
            }
        }
    }

}