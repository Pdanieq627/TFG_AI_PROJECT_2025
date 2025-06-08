//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ChestController : MonoBehaviour
//{
//    [Header("Loot UI")]
//    public Canvas lootCanvas;             // LootCanvas de la escena
//    public Button[] optionButtons;        // Option1, Option2, Option3
//    public Image[] optionIcons;
//    public Text[] optionLabels;           // o TMP_Text si usas TextMeshPro

//    [Header("Loot Tables")]
//    public List<ItemData> floor1Items;    // Arrastra SwordIron, ArmorLeather, HealthPotion

//    private List<ItemData> currentLoot;
//    private bool isOpen = false;

//    void OnTriggerEnter2D(Collider2D col)
//    {
//        if (isOpen) return;
//        if (col.CompareTag("Player"))
//        {
//            OpenChest();
//        }
//    }

//    void OpenChest()
//    {
//        isOpen = true;
//        // Generar 3 ítems aleatorios (sin repetir)
//        currentLoot = new List<ItemData>();
//        var pool = new List<ItemData>(floor1Items);

//        for (int i = 0; i < optionButtons.Length; i++)
//        {
//            if (pool.Count == 0) break;
//            int idx = Random.Range(0, pool.Count);
//            currentLoot.Add(pool[idx]);
//            pool.RemoveAt(idx);
//        }

//        // Mostrar UI
//        lootCanvas.gameObject.SetActive(true);
//        Time.timeScale = 0f;  // pausa el juego

//        for (int i = 0; i < optionButtons.Length; i++)
//        {
//            if (i < currentLoot.Count)
//            {
//                var item = currentLoot[i];
//                optionIcons[i].sprite = item.icon;
//                optionLabels[i].text = item.itemName + "\n" + item.description;
//                int copy = i;  // closure
//                optionButtons[i].onClick.RemoveAllListeners();
//                optionButtons[i].onClick.AddListener(() => OnSelectItem(copy));
//                optionButtons[i].gameObject.SetActive(true);
//            }
//            else
//            {
//                optionButtons[i].gameObject.SetActive(false);
//            }
//        }
//    }

//    void OnSelectItem(int index)
//    {
//        var item = currentLoot[index];
//        var player = FindObjectOfType<PlayerCombat>();
//        if (player != null)
//        {
//            // Aplicar efecto
//            switch (item.itemType)
//            {
//                case ItemType.Weapon:
//                    player.baseDamage = item.damageBonus;
//                    break;
//                case ItemType.Armor:
//                    player.armorBonus = item.armorBonus;
//                    break;
//                case ItemType.Potion:
//                    player.currentPotions = Mathf.Min(player.currentPotions + 1, player.maxPotions);
//                    break;
//            }
//        }
//        CloseChest();
//    }

//    void CloseChest()
//    {
//        lootCanvas.gameObject.SetActive(false);
//        Time.timeScale = 1f;
//        Destroy(gameObject);  // destruye el cofre
//    }
//}
