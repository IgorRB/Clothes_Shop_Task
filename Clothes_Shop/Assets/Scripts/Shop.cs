using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactive
{

    private Player_Controller interactingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
