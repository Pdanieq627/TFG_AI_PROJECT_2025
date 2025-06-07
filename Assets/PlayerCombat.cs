using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Estadísticas")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public int baseDamage = 10;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void ReceiveDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        // Aquí podrías reproducir animación/SFX antes de cambiar de escena
        SceneManager.LoadScene("GameOverScene");
    }
}