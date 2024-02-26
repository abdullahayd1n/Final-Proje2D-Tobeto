using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float speed;
    Rigidbody rb;
   
  public GameObject bloodEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }

        {
            
        }
        transform.Translate(Vector2.left*speed*Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
       Instantiate(bloodEffect,transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Damage Taken!");
    }
}
