using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactive
{

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactingPlayer != null)
        {
            if(interactingPlayer.interactingWith != this)
            {
                UI_Controller.instance.PanelSetActive("Shop_Panel" ,false);
                UI_Controller.instance.PanelSetActive("Inventory_Shop_Panel", false);
                interactingPlayer = null;
            }
        }
    }

    public override void Action(Player_Controller player)
    {
        base.Action(player);
        interactingPlayer = player;
        UI_Controller.instance.PanelSetActive("Shop_Panel", true);
        UI_Controller.instance.RefreshItems(inventory.GetItems());
    }
}
