using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactive
{
    [SerializeField] Inventory inventory;
    [SerializeField] Cosmetic_Item[] startingItens;

    private Player_Controller interactingPlayer;
    private string activeShopTab = "outfit";

    // Start is called before the first frame update
    void Start()
    {
        inventory.ClearInventory();

        foreach(Cosmetic_Item item in startingItens)
        {
            switch (item.type)
            {
                case "outfit":
                    inventory.AddItem(item); break;
                case "hair":
                    inventory.AddItem(item); break;
                case "hat":
                    inventory.AddItem(item); break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(interactingPlayer != null)
        {
            if(interactingPlayer.interactingWith != this)
            {
                UI_Controller.instance.ShopSetActive(false);
                interactingPlayer = null;
            }
        }
    }

    public override void Action(Player_Controller player)
    {
        base.Action(player);
        interactingPlayer = player;
        UI_Controller.instance.ShopSetActive(true);
        UI_Controller.instance.RefreshItens(GetItens());
    }

    public void SetShopTab(string tab)
    {
        activeShopTab = tab;
    }

    public Cosmetic_Item[] GetItens()
    {
        return inventory.GetItensOfType(activeShopTab);
    }

    public Cosmetic_Item BuyItem(int index)
    {
        return inventory.RemoveItem(index, activeShopTab);
    }
}
