using UnityEngine;


public abstract class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public int buyPrice;
    public int sellPrice;
}
