using UnityEngine;

public class EnemyPatrol1 : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned in EnemyPatrol1 script!");
            enabled = false; // Bu bile�eni devre d��� b�rak�r
            return;
        }

        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        if (anim != null)
            anim.SetBool("move", false);
    }

    private void Update()
    {
        if (enemy != null && enemy.gameObject.activeSelf) // D��man var ve aktifse
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
        if (anim != null)
            anim.SetBool("move", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        if (anim != null)
            anim.SetBool("move", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + _direction * speed * Time.deltaTime,
            enemy.position.y, enemy.position.z);
    }

    private void OnDestroy()
    {
        // Abonelikleri temizle
        // �rne�in: enemy ile ilgili abonelikler temizlenebilir
    }
}
