using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item_Slot_Equip : Item_Slot
{
    [SerializeField] TMP_Text equippedText;

    public void RefreshSlot(Cosmetic_Item[] equippedItems)
    {
        if (item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        nameText.text = item.nameItem;
        icon.sprite = item.icon;
        equippedText.text = "";

        foreach (Cosmetic_Item equipped in equippedItems)
        {
            if (item == equipped)
                equippedText.text = "equipped";
        }
    }
}
