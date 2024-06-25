using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Slot : MonoBehaviour
{
    [HideInInspector] public Cosmetic_Item item;

    [SerializeField] protected TMP_Text nameText, priceValueText;
    [SerializeField] protected Image icon;
    
    public void RefreshSlot(int playerMoney)
    {
        if (item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        nameText.text = item.nameItem;
        priceValueText.text = item.price.ToString() + "$";
        icon.sprite = item.icon;

        if (playerMoney < item.price && !UI_Controller.instance.IsPlayerInventory())
            priceValueText.color = Color.red;
        else
            priceValueText.color = Color.black;
    }
}
