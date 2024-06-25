using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player_Inventory_Manager : Inventory_Manager
{
    [HideInInspector] public int money;

    [SerializeField] GameObject outfit, hair, hat;
    private Cosmetic_Item equippedOutfit, equippedHair, equippedHat;

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

        InitialCosmeticItens();
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
        UnequipItem(item);
        UI_Controller.instance.RefreshMoneyText(money);
        return item;
    }

    void InitialCosmeticItens()
    {
        outfit.SetActive(false);
        hair.SetActive(false);
        hat.SetActive(false);

        Cosmetic_Item[] items;

        items = inventory.GetItensOfType("outfit");
        if (items.Length > 0)
        {
            EquipItem(items[0]);
        }

        items = inventory.GetItensOfType("hair");
        if (items.Length > 0)
        {
            EquipItem(items[0]);
        }

        items = inventory.GetItensOfType("hat");
        if (items.Length > 0)
        {
            EquipItem(items[0]);
        }

    }

    public Cosmetic_Item[] GetEquipped()
    {
        List<Cosmetic_Item> items = new List<Cosmetic_Item>();
        items.Add(equippedOutfit);
        items.Add(equippedHair);
        items.Add(equippedHat);
        return items.ToArray();
    }

    public void UnequipItem(Cosmetic_Item item)
    {
        switch (item.type)
        {
            case "outfit":
                if (equippedOutfit == item)
                {
                    equippedOutfit = null;
                    outfit.SetActive(false);
                }
                break;
            case "hair":
                if(equippedHair == item)
                {
                    equippedHair = null;
                    hair.SetActive(false);
                }
                break;
            case "hat":
                if(equippedHat == item)
                {
                    equippedHat = null;
                    hat.SetActive(false);
                }
                break;
            default: break;
        }
    }

    public void EquipItem(Cosmetic_Item item)
    {
        switch (item.type)
        {
            case "outfit":
                equippedOutfit = item;
                outfit.GetComponent<Animator>().runtimeAnimatorController = equippedOutfit.animator;
                outfit.SetActive(true);
                break;
            case "hair":
                equippedHair = item;
                hair.GetComponent<Animator>().runtimeAnimatorController = equippedHair.animator;
                hair.SetActive(true);
                break;
            case "hat":
                equippedHat = item;
                hat.GetComponent<Animator>().runtimeAnimatorController = equippedHat.animator;
                hat.SetActive(true);
                if (equippedHat.nameItem == "Witch Hat")
                    hair.SetActive(false);
                break;
            default: break;
        }
    }
}
