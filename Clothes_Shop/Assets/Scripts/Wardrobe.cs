using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactive
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactingPlayer != null)
        {
            if (interactingPlayer.interactingWith != this)
            {
                UI_Controller.instance.PanelSetActive("Inventory_Equip_Panel", false);
                interactingPlayer = null;
            }
        }
    }

    public override void Action(Player_Controller player)
    {
        base.Action(player);
        interactingPlayer = player;
        inventory = player.GetComponent<Inventory_Manager>();
        UI_Controller.instance.PanelSetActive("Inventory_Equip_Panel", true);
        UI_Controller.instance.RefreshItens(inventory.GetItens());
    }
}
