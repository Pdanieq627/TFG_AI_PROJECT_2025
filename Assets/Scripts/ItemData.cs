using UnityEngine;

public enum ItemType { Weapon, Armor, Potion }

[CreateAssetMenu(fileName = "NewItem", menuName = "Loot/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    [Header("Stats")]
    public int damageBonus;       // Si es arma
    public int armorBonus;        // Si es armadura
    public int healAmount;        // Si es poci�n

    [TextArea] public string description;
}