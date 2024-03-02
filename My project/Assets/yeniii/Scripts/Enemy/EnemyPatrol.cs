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

    private void OnDisable()
    {
        anim.SetBool("moving", false); // D��man�n Animator'�ndaki "moving" parametresini false olarak ayarla
    }

    private void Update()
    {
        if (leftEdge != null && rightEdge != null) // Sol ve sa� s�n�r noktalar� null de�ilse devam et
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
        idleTimer = 0; // Hareketsizlik zamanlay�c�s�n� s�f�rla
        anim.SetBool("moving", true); // Animator'daki "moving" parametresini true olarak ayarla

        // D��man� belirli bir y�ne d�nme
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        // Belirli bir y�ne hareket etme
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
