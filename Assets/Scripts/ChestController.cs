using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChestController : MonoBehaviour
{
    [Header("Loot UI")]
    public GameObject lootPanel;       // El panel con las 3 opciones
    public Button[] optionButtons;     // Array de 3 Buttons
    public Image[] optionIcons;        // Sus Image hijos
    public TMP_Text[] optionTexts;     // Sus TMP_Text hijos

    [Header("Loot Tables")]
    public List<Item> floor1And2Loot;
    public List<Item> floor3And4Loot;
    public List<Item> floor5Loot;

    private List<Item> currentLootOptions;
    private int chestFloor;

    void Start()
    {
        // Averigua en qué piso estamos buscando el DungeonGenerator
        var dg = FindObjectOfType<DungeonGenerator>();
        chestFloor = dg.currentFloor; // Necesitarás exponer currentFloor en DG
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OpenChest();
            Destroy(GetComponent<Collider2D>());
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OpenChest()
    {
        // Construye la lista adecuada
        if (chestFloor <= 2) currentLootOptions = floor1And2Loot;
        else if (chestFloor <= 4) currentLootOptions = floor3And4Loot;
        else currentLootOptions = floor5Loot;

        // Selecciona 3 ítems aleatorios sin repetir
        var copy = new List<Item>(currentLootOptions);
        var picks = new List<Item>();
        for (int i = 0; i < 3; i++)
        {
            if (copy.Count == 0) break;
            int idx = Random.Range(0, copy.Count);
            picks.Add(copy[idx]);
            copy.RemoveAt(idx);
        }

        // Muestra el panel
        lootPanel.SetActive(true);
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < picks.Count)
            {
                var item = picks[i];
                optionIcons[i].sprite = item.icon;
                optionTexts[i].text = item.name + "\n" + item.description;
                int capture = i; // para la lambda
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnItemSelected(picks[capture]));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnItemSelected(Item chosen)
    {
        // Aplica el ítem al jugador
        var pc = FindObjectOfType<PlayerCombat>();
        pc.EquipItem(chosen);
        lootPanel.SetActive(false);
        Destroy(gameObject); // Cofre consumido
    }
}