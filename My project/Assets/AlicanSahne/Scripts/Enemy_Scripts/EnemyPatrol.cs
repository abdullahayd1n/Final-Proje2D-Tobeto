using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge; // Sol sýnýr noktasý
    [SerializeField] private Transform rightEdge; // Sað sýnýr noktasý

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // Düþmanýn Transform bileþeni

    [Header("Movement Parameters")]
    [SerializeField] private float speed; // Düþmanýn hareket hýzý
    private Vector3 initScale; // Baþlangýçta düþmanýn ölçeði
    private bool movingLeft; // Düþmanýn þu an sola mý yoksa saða mý hareket ettiði

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // Düþmanýn hareketsiz durma süresi
    private float idleTimer; // Hareketsiz durma süresini takip eden zamanlayýcý

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; // Düþmanýn Animator bileþeni

    private void Awake()
    {
        initScale = enemy.localScale; // Düþmanýn baþlangýç ölçeðini kaydet
    }

    void OnDisable()
    {
        // Null kontrolü yaparak animator'ýn null olup olmadýðýný kontrol ediyoruz
        if (anim != null)
        {
            // Set the "moving" parameter of the animator to false when this script is disabled
            anim.SetBool("moving", false);
        }
    }


    private void Update()
    {

        // Eðer enemy ve sol ve sað sýnýr noktalarý null deðilse devam et
        if (enemy != null && leftEdge != null && rightEdge != null)
        {
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                    MoveInDirection(-1);
                else
                    DirectionChange();
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                    MoveInDirection(1);
                else
                    DirectionChange();
            }
        }
    }



    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0; // Hareketsizlik zamanlayýcýsýný sýfýrla
        anim.SetBool("moving", true); // Animator'daki "moving" parametresini true olarak ayarla

        // Düþmaný belirli bir yöne dönme
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        // Belirli bir yöne hareket etme
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
