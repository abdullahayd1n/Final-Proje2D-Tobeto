using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge; // Sol s�n�r noktas�
    [SerializeField] private Transform rightEdge; // Sa� s�n�r noktas�

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // D��man�n Transform bile�eni

    [Header("Movement Parameters")]
    [SerializeField] private float speed; // D��man�n hareket h�z�
    private Vector3 initScale; // Ba�lang��ta d��man�n �l�e�i
    private bool movingLeft; // D��man�n �u an sola m� yoksa sa�a m� hareket etti�i

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // D��man�n hareketsiz durma s�resi
    private float idleTimer; // Hareketsiz durma s�resini takip eden zamanlay�c�

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; // D��man�n Animator bile�eni

    private void Awake()
    {
        initScale = enemy.localScale; // D��man�n ba�lang�� �l�e�ini kaydet
    }

    void OnDisable()
    {
        // Null kontrol� yaparak animator'�n null olup olmad���n� kontrol ediyoruz
        if (anim != null)
        {
            // Set the "moving" parameter of the animator to false when this script is disabled
            anim.SetBool("moving", false);
        }
    }


    private void Update()
    {
        // E�er enemy ve sol ve sa� s�n�r noktalar� null de�ilse devam et
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

            // Belirli bir y�ne do�ru hareket ederken boyutu ayarla
            float direction = movingLeft ? -1 : 1;
            enemy.localScale = new Vector3(0.8f * direction, initScale.y, initScale.z);
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
        idleTimer = 0; // Hareketsizlik zamanlay�c�s�n� s�f�rla
        anim.SetBool("moving", true); // Animator'daki "moving" parametresini true olarak ayarla

        // D��man�n boyutunu belirli bir boyuta (0.8) ayarla, y�n�ne g�re
        enemy.localScale = new Vector3(0.8f * direction, initScale.y, initScale.z);

        // Belirli bir y�ne hareket etme
        float moveSpeed = speed * direction; // Hareket h�z�, y�n ile �arp�l�r
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * moveSpeed, enemy.position.y, enemy.position.z);
    }

}
