using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory_Manager : Inventory_Manager
{
    [HideInInspector] public int money;

    // Start is called before the first frame update
    void Start()
    {
        inventory.ClearInventory();

        foreach (Cosmetic_Item item in startingItens)
        {
            inventory.AddItem(item);
        }

        money = 100;
        UI_Controller.instance.RefreshMoneyText(money);

    }

    // Update is called once per frame
    void Update()
    {
        // Testing Zone
        if (Input.GetKeyDown(KeyCode.P))
            inventory.ClearInventory();
    }
    public void BuyItem(Cosmetic_Item item)
    {
        money -= item.price;
        UI_Controller.instance.RefreshMoneyText(money);
        inventory.AddItem(item);
    }

    override public Cosmetic_Item SellItem(int index)
    {
        Cosmetic_Item item = inventory.RemoveItem(index, activeTab);
        money += item.price;
        UI_Controller.instance.RefreshMoneyText(money);
        return item;
    }
}
