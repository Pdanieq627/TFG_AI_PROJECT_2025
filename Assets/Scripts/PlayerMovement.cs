using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;           // Unidades por segundo (32 px = 1 u)
    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lectura de ejes
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Evitar diagonales rápidas
        if (input.x != 0) input.y = 0;

        // Guardar última dirección para animaciones/ataque
        if (input.sqrMagnitude > 0.01f)
            lastDirection = input.normalized;
    }

    void FixedUpdate()
    {
        // Movimiento por física
        Vector2 newPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}
