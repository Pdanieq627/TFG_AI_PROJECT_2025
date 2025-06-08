using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Estad�sticas")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public int baseDamage = 10;

    [Header("Ataque Cuerpo a Cuerpo")]
    public Transform hitboxOrigin;
    public Vector2 hitboxSize = new Vector2(0.5f, 0.5f);
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    //[Header("Inventario")]
    //public int currentPotions = 0;
    //public int maxPotions = 3;
    //public int armorBonus = 0;  // usa esto al recibir da�o


    void Awake()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        // Colisionar con enemigos dentro del �rea
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            hitboxOrigin.position,
            hitboxSize,
            0f,
            LayerMask.GetMask("Enemy")
        );

        foreach (var hit in hits)
        {
            var ai = hit.GetComponent<SkeletonAI>();
            if (ai != null)
                ai.TakeDamage(baseDamage);
        }
    }

    public void ReceiveDamage(int amount)
    {
        //int effectiveDamage = Mathf.Max(amount - armorBonus, 0);
        //currentHP -= effectiveDamage;
        currentHP -= amount;
        AudioManager.Instance.PlayPlayerDamage(); // <-- sonido de da�o
        if (currentHP <= 0)
        {
            currentHP = 0;
            AudioManager.Instance.PlayPlayerDeath(); // <-- sonido de muerte
            Die();
        }
        Debug.Log("Player le queda vida: " + currentHP);
    }


    private void Die()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // S�lo para debug: visualiza el �rea de ataque
    void OnDrawGizmosSelected()
    {
        if (hitboxOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitboxOrigin.position, hitboxSize);
    }
}