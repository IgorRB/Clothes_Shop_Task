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
    [SerializeField] Inventory_Manager shop;
    [SerializeField] Player_Inventory_Manager player;

    [SerializeField] private GameObject helpPanel;

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
                helpPanel.SetActive(false);
                if (IsPlayerInventory())
                    TabButtonClick(player.GetTab());
                else
                    TabButtonClick(shop.GetTab());
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

    public void RefreshItems(Cosmetic_Item[] items)
    {
        Inventory_Menu active = GetActivePanel();

        for (int i = 0; i < 4; i++)
        {
            active.itemSlots[i].item = null;

            if (i < items.Length)
                active.itemSlots[i].item = items[i];

            if (active.name.Contains("Equip"))
                active.itemSlots[i].GetComponent<Item_Slot_Equip>().RefreshSlot(player.GetEquipped());
            else
                active.itemSlots[i].RefreshSlot(player.money);
        }

        if (!active.itemSlots[0].gameObject.activeInHierarchy)
            active.emptyText.SetActive(true);
        else
            active.emptyText.SetActive(false);
    }

    public void TabButtonClick(string type)
    {
        Inventory_Menu active = GetActivePanel();
        bool playerInventory = IsPlayerInventory();

        if (active == null) return;

        foreach (Button button in active.tabButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        if (playerInventory)
            player.SetTab(type);
        else
            shop.SetTab(type);

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

        if (playerInventory)
            RefreshItems(player.GetItems());
        else
            RefreshItems(shop.GetItems());

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

        if (active.itemSlots[selectedItemIndex].item.price <= player.money)
            player.BuyItem(shop.SellItem(selectedItemIndex));

        RefreshItems(shop.GetItems());
        ClearItemSelection();
    }

    public void SellButton()
    {
        Inventory_Menu active = GetActivePanel();

        if (selectedItemIndex == -1) return;

        shop.AddItem(player.SellItem(selectedItemIndex));
        RefreshItems(player.GetItems());
        ClearItemSelection();
    }

    public void EquipButton()
    {
        Inventory_Menu active = GetActivePanel();

        if (selectedItemIndex == -1) return;

        player.EquipItem(player.GetItemAt(selectedItemIndex));
        RefreshItems(player.GetItems());
        ClearItemSelection();
    }

    public void UnequipButton()
    {
        Inventory_Menu active = GetActivePanel();

        if (selectedItemIndex == -1) return;

        player.UnequipItem(player.GetItemAt(selectedItemIndex));
        RefreshItems(player.GetItems());
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

    public bool IsPlayerInventory()
    {
        Inventory_Menu active = GetActivePanel();
        if(active == null) return false;
        return active.name.StartsWith("Inventory");
    }

    // HELP MENU

    public void QuitGameButton()
    {
        Application.Quit();
    }
    public void CloseHelpButton()
    {
        helpPanel.SetActive(false);
    }
    public void HelpButton()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
    }
}
