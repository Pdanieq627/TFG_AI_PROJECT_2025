using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonAI : MonoBehaviour
{
    [Header("Parámetros IA")]
    public float chaseSpeed = 3f;
    public float detectionRadius = 5f;

    [Header("Salud Enemigo")]
    public int maxHP = 30;
    private int currentHP;

    private Rigidbody2D rb;
    private Transform player;
    private float lastHitTime = 0f;
    public float hitCooldown = 1f;
    public int contactDamage = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector2.Distance(rb.position, player.position);
        if (dist <= detectionRadius)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * chaseSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        AudioManager.Instance.PlaySkeletonDamage();  // <-- sonido de daño
        if (currentHP <= 0)
        {
            AudioManager.Instance.PlaySkeletonDeath(); // <-- sonido de muerte
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") && Time.time - lastHitTime >= hitCooldown)
        {
            var combat = col.collider.GetComponent<PlayerCombat>();
            if (combat != null)
                combat.ReceiveDamage(contactDamage);
            lastHitTime = Time.time;
        }
    }
}
