using BarthaSzabolcs.Tutorial_SpriteFlash;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Player Layers")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    

    private Animator anim;
    
    public int maxhealth;
    public int health;

    Rigidbody rb;

    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    private EnemyHealthBar healthBar;

   

   [SerializeField] private SimpleFlash flashEffect;
   public GameObject bloodEffect;


    
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxhealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        rb = GetComponent<Rigidbody>();
        
        
    }
    
    
    void Update()
    {

        
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }


   

    

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        healthBar.UpdateHealthBar(maxhealth,health);
        popUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        flashEffect.Flash();
    }

   




    private void OnCollisionEnter2D(Collision2D collision)//lucas�n can� gidiyor.
   {
       if (collision.gameObject.CompareTag("Player")) // "CompareTag" metodu kullan�larak etiket kar��la�t�rmas� yap�l�yor
        {
            LucasHealth healthComponent = collision.gameObject.GetComponent<LucasHealth>();
           if (healthComponent != null)
            {
                
                healthComponent.TakeDamage(10); // Oyuncuya 10 hasar veriliyor
            }
        }
    }




}
