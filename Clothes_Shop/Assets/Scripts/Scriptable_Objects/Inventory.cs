using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "My_Inventory", menuName = "ScriptableObjects/InventoryScriptableObject", order = 1)]
public class Inventory : ScriptableObject
{
    public List<Cosmetic_Item> outfits = new List<Cosmetic_Item>();
    public List<Cosmetic_Item> hairs = new List<Cosmetic_Item>();
    public List<Cosmetic_Item> hats = new List<Cosmetic_Item>();

    public void ClearInventory()
    {
        outfits.Clear();
        hairs.Clear();
        hats.Clear();
    }

    public void AddItem(Cosmetic_Item item)
    {
        switch (item.type)
        {
            case "outfit":
                outfits.Add(item); break;
            case "hair":
                hairs.Add(item); break;
            case "hat":
                hats.Add(item); break;
        }
    }

    public Cosmetic_Item GetItem(int index, string type)
    {
        Cosmetic_Item item = null;

        switch (type)
        {
            case "outfit":
                item = outfits[index];
                break;
            case "hair":
                item = hairs[index];
                break;
            case "hat":
                item = hats[index];
                break;
            default: break;
        }

        return item;
    }

    public Cosmetic_Item RemoveItem(int index, string type)
    {
        Cosmetic_Item item = null;

        switch (type)
        {
            case "outfit":
                item = outfits[index];
                outfits.RemoveAt(index);
                break;
            case "hair":
                item = hairs[index];
                hairs.RemoveAt(index);
                break;
            case "hat":
                item = hats[index];
                hats.RemoveAt(index);
                break;
            default: break;
        }

        return item;
    }

    public Cosmetic_Item[] GetItemsOfType(string type)
    {
        Cosmetic_Item[] items = new Cosmetic_Item[0];

        switch (type)
        {
            case "outfit":
                items = outfits.ToArray(); break;
            case "hair":
                items = hairs.ToArray(); break;
            case "hat":
                items = hats.ToArray(); break;
            default: break;
        }
        
        return items;
    }
}
