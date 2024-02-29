using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region Public Variables
    public Transform raycastOrigin; // Deðiþken ismi düzeltildi
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion

    void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Raycast sadece oyuncu aralýðýnda ise yapýlýr
        if (inRange)
        {
            hit = Physics2D.Raycast(raycastOrigin.position, Vector2.left, rayCastLength, raycastMask);
            raycastDebugger();
        }

        // Oyuncu algýlandýysa düþman mantýðý çalýþtýrýlýr
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            EnemyLogic();
        }
        else
        {
            inRange = false;
        }

        // Oyuncu aralýðýnda deðilse durdurma iþlemi yapýlýr
        if (!inRange)
        {
            anim.SetBool("Move", false);
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Player")) // Tag kontrolü optimize edildi
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && !cooling) // cooling kontrolü optimize edildi
        {
            Attack();
        }

        if (cooling)
        {
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Move", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skel_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("Move", false);
        anim.SetBool("Attack", true);
    }

    void raycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(raycastOrigin.position, Vector2.left * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycastOrigin.position, Vector2.left * rayCastLength, Color.green);
        }
    }
}
