using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Estadísticas")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public int baseDamage = 10;

    [Header("Ataque Cuerpo a Cuerpo")]
    public Transform hitboxOrigin;
    public Vector2 hitboxSize = new Vector2(0.5f, 0.5f);
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    [Header("Experiencia")]
    public int currentXP = 0;
    public int level = 1;
    public int xpPerLevel = 50;


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
        // Colisionar con enemigos dentro del área
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
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        Debug.Log("Player le queda vida: " + currentHP);
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // Sólo para debug: visualiza el área de ataque
    void OnDrawGizmosSelected()
    {
        if (hitboxOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitboxOrigin.position, hitboxSize);
    }

//    public void GainXP(int amount)
//    {
//        currentXP += amount;
//        if (currentXP >= xpPerLevel * level)
//        {
//            currentXP -= xpPerLevel * level;
//            LevelUp();
//        }
//    }

//    private void LevelUp()
//    {
//        level++;
//        maxHP += 5;
//        currentHP = Mathf.Min(currentHP + Mathf.RoundToInt(maxHP * 0.3f), maxHP);
//        baseDamage += 1;
//        // Aquí un popup o SFX: subes de nivel
//    }

}