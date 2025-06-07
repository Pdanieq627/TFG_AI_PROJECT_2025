using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image hpBar;
    public TMP_Text hpText;

    private PlayerCombat player;

    void Start()
    {
        // Intento inicial
        player = FindObjectOfType<PlayerCombat>();
    }

    void Update()
    {
        // Si aún no hemos localizado al jugador, lo buscamos de nuevo
        if (player == null)
            player = FindObjectOfType<PlayerCombat>();

        if (player == null)
            return;  // Salimos si sigue sin existir

        float ratio = (float)player.currentHP / player.maxHP;
        hpBar.rectTransform.sizeDelta = new Vector2(100 * ratio, hpBar.rectTransform.sizeDelta.y);
        hpText.text = $"HP: {player.currentHP}/{player.maxHP}";
    }
}