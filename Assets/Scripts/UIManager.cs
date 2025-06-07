using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image hpBar;        // Imagen roja
    public Text hpText;        // Texto de HP (o TMP_Text)
    private PlayerCombat player;

    void Start()
    {
        player = FindObjectOfType<PlayerCombat>();
    }

    void Update()
    {
        if (player == null) return;
        float ratio = (float)player.currentHP / player.maxHP;
        hpBar.rectTransform.sizeDelta = new Vector2(100 * ratio, hpBar.rectTransform.sizeDelta.y);
        hpText.text = $"HP: {player.currentHP}/{player.maxHP}";
    }
}
