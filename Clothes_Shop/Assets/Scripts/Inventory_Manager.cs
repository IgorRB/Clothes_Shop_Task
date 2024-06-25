using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected Cosmetic_Item[] startingItens;

    protected string activeTab = "outfit";

    // Start is called before the first frame update
    void Start()
    {
        inventory.ClearInventory();

        foreach (Cosmetic_Item item in startingItens)
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

    public Cosmetic_Item[] GetItens()
    {
        return inventory.GetItensOfType(activeTab);
    }
    virtual public Cosmetic_Item SellItem(int index)
    {
        return inventory.RemoveItem(index, activeTab);
    }
}
