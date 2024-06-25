using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Inventory_Menu[] menuPanels;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] Shop shop;
    [SerializeField] Inventory_Manager playerInventory;

    int selectedItemIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Inventory_Menu panel in menuPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PanelSetActive(string name, bool value, bool closeOthers = false)
    {
        foreach(Inventory_Menu panel in menuPanels)
        {
            if(panel.name == name)
            {
                panel.gameObject.SetActive(value);
                panel.tabButtons[0].gameObject.GetComponent<Image>().color = new Color(.75f, .75f, .75f);
            }                
            else if(closeOthers)
                panel.gameObject.SetActive(false);
        }
    }

    Inventory_Menu GetActivePanel()
    {
        foreach( Inventory_Menu panel in menuPanels)
        {
            if(panel.gameObject.activeInHierarchy)
                return panel;
        }

        return null;
    }

    public void RefreshItens(Cosmetic_Item[] items)
    {
        Inventory_Menu active = GetActivePanel();

        for (int i = 0; i < 4; i++)
        {
            active.itemSlots[i].item = null;

            if (i < items.Length)
                active.itemSlots[i].item = items[i];

            active.itemSlots[i].RefreshSlot(playerInventory.money);
        }

        if (!active.itemSlots[0].gameObject.activeInHierarchy)
            active.emptyText.SetActive(true);
        else
            active.emptyText.SetActive(false);
    }

    public void TabButtonClick(string type)
    {
        Inventory_Menu active = GetActivePanel();

        foreach (Button button in active.tabButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        shop.SetShopTab(type);

        switch (type)
        {
            case "outfit":
                active.tabButtons[0].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            case "hair":
                active.tabButtons[1].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            case "hat":
                active.tabButtons[2].GetComponent<Image>().color = new Color(.75f, .75f, .75f);
                break;
            default: break;
        }

        RefreshItens(shop.GetItens());
        ClearItemSelection();
    }

    public void ItemButtonClick(int index)
    {
        Inventory_Menu active = GetActivePanel();
        ClearItemSelection();

        selectedItemIndex = index;

        active.itemSlots[index].GetComponent<Image>().color = new Color(1, 0.796f, 0.3125f, 1);
    }

    public void BuyButton()
    {
        Inventory_Menu active = GetActivePanel();

        if (selectedItemIndex == -1) return;

        if (active.itemSlots[selectedItemIndex].item.price <= playerInventory.money)
            playerInventory.BuyItem(shop.SellItem(selectedItemIndex));

        RefreshItens(shop.GetItens());
        ClearItemSelection();
    }

    public void GoToButton(string name)
    {
        PanelSetActive(name, true, true);
    }

    void ClearItemSelection()
    {
        Inventory_Menu active = GetActivePanel();

        foreach (Item_Slot slot in active.itemSlots)
        {
            slot.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        selectedItemIndex = -1;
    }

    public void RefreshMoneyText(int value)
    {
        moneyText.text = value.ToString() + "$";
    }

    public void CloseButton(string panel)
    {
        PanelSetActive(panel, false);
    }
}
