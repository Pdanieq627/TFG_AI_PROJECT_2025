using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 input;
    private BoxCollider2D boxCol;
    private ContactFilter2D movementFilter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();

        // Configura filtro para colisionar solo con "Ground"
        movementFilter = new ContactFilter2D();
        movementFilter.SetLayerMask(LayerMask.GetMask("Ground"));
        movementFilter.useTriggers = false;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x != 0) input.y = 0;
    }

    void FixedUpdate()
    {
        Vector2 delta = input * moveSpeed * Time.fixedDeltaTime;
        if (CanMove(delta))
        {
            rb.MovePosition(rb.position + delta);
        }
    }

    // Comprueba si moverse en 'delta' chocaría con un muro
    bool CanMove(Vector2 delta)
    {
        // Hacemos un BoxCast de nuestro collider en la dirección delta
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int count = boxCol.Cast(
            delta.normalized,        // dirección
            movementFilter,          // filtro para Ground
            hits,                    // array donde vuelca resultados
            delta.magnitude + 0.01f  // distancia (añadimos un pequeño margen)
        );
        // Si count > 0, hay un muro al frente
        return count == 0;
    }
}