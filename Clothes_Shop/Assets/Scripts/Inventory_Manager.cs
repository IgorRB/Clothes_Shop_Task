using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Cosmetic_Item[] testItens;

    [HideInInspector] public int money;
    private string activeIventoryTab = "outfit";

    // Start is called before the first frame update
    void Start()
    {
        money = 100;
        UI_Controller.instance.RefreshMoneyText(money);
    }

    // Update is called once per frame
    void Update()
    {
        
        // Testing zone
        if (Input.GetKeyDown(KeyCode.J))
            inventory.AddItem(testItens[0]);
        if (Input.GetKeyDown(KeyCode.K))
            inventory.AddItem(testItens[1]);
        if (Input.GetKeyDown(KeyCode.L))
            inventory.AddItem(testItens[2]);
        if (Input.GetKeyDown(KeyCode.P))
            inventory.ClearInventory();

    }

    public void AddItem(Cosmetic_Item item)
    {
        inventory.AddItem(item);
    }

    public void BuyItem(Cosmetic_Item item)
    {
        money -= item.price;
        UI_Controller.instance.RefreshMoneyText(money);
        inventory.AddItem(item);
    }

    public void SetInventoryTab(string tab)
    {
        activeIventoryTab = tab;
    }
}
