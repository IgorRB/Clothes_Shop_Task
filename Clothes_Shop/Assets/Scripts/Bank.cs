using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Interactive
{
    public override void Action(Player_Controller player)
    {
        base.Action(player);
        player.GetComponent<Player_Inventory_Manager>().AddMoney();
    }
}
