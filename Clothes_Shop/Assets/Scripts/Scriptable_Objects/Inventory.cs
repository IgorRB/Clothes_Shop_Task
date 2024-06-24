using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
