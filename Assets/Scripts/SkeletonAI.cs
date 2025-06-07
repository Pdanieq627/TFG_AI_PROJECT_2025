using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonAI : MonoBehaviour
{
    [Header("Parámetros")]
    public float chaseSpeed = 3f;           // 3 u/s = ~96 px/s
    public float detectionRadius = 5f;      // tiles
    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            // Perseguir
            Vector2 dir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * chaseSpeed * Time.fixedDeltaTime);
        }
    }
}