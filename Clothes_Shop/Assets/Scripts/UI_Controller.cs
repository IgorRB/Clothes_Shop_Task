using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    // Singleton
    public static UI_Controller instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Variables and References
    [SerializeField] private GameObject shopPanel, soldOutText;
    [SerializeField] Item_Slot[] itemSlots;
    [SerializeField] Button[] shopButtons;
    [SerializeField] Shop shop;
    [SerializeField] Inventory_Manager playerInventory;

    int selectedItemIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
        shopButtons[0].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShopSetActive(bool value)
    {
        shopPanel.SetActive(value);
    }

    public void RefreshItens(Cosmetic_Item[] itens)
    {
        foreach(Item_Slot slot in itemSlots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < itens.Length; i++)
        {
            itemSlots[i].gameObject.SetActive(true);
            itemSlots[i].nameText.text = itens[i].nameItem;
            itemSlots[i].priceValueText.text = itens[i].price.ToString() + "$";

            itemSlots[i].icon.sprite = itens[i].icon;
        }

        if (!itemSlots[0].gameObject.activeInHierarchy)
            soldOutText.SetActive(true);
        else
            soldOutText.SetActive(false);
    }

    public void TabButtonClick(string type)
    {
        foreach (Button button in shopButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        shop.SetShopTab(type);

        switch (type)
        {
            case "outfit":
                shopButtons[0].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            case "hair":
                shopButtons[1].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            case "hat":
                shopButtons[2].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            default: break;
        }

        RefreshItens(shop.GetItens());
        ClearItemSelection();
    }

    public void ItemButtonClick(int index)
    {
        ClearItemSelection();

        selectedItemIndex = index;

        itemSlots[index].GetComponent<Image>().color = new Color(1, 0.796f, 0.3125f, 1);
    }

    public void BuyButton()
    {
        if (selectedItemIndex == -1) return;

        playerInventory.AddItem(shop.BuyItem(selectedItemIndex));

        RefreshItens(shop.GetItens());
        ClearItemSelection();
    }

    void ClearItemSelection()
    {
        foreach (Item_Slot slot in itemSlots)
        {
            slot.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        selectedItemIndex = -1;
    }
}
