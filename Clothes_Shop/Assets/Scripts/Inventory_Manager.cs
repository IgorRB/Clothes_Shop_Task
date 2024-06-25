using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected Cosmetic_Item[] startingItems;

    protected string activeTab = "outfit";

    // Start is called before the first frame update
    void Start()
    {
        inventory.ClearInventory();

        foreach (Cosmetic_Item item in startingItems)
        {
            inventory.AddItem(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Cosmetic_Item item)
    {
        inventory.AddItem(item);
    }

    public void SetTab(string tab)
    {
        activeTab = tab;
    }
    public string GetTab()
    {
        return activeTab;
    }

    public Cosmetic_Item[] GetItems()
    {
        return inventory.GetItemsOfType(activeTab);
    }
    public Cosmetic_Item GetItemAt(int index)
    {
        Cosmetic_Item item = inventory.GetItem(index, activeTab);
        return item;
    }
    virtual public Cosmetic_Item SellItem(int index)
    {
        return inventory.RemoveItem(index, activeTab);
    }
}
