using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    private Inventory playerInventory;
    private ShopInventory shopInventory;

    public GameObject BuyButton;
    public GameObject SellButton;

    public GameObject TransferButton;

    public GameObject GoldText;

    public GameObject SalePopup;
    public GameObject SaleText;
    public GameObject SaleCancel;
    public GameObject SaleConfirm;

    public GameObject ItemSlot;
    public GameObject ItemArea;
    public GameObject Title;

    public GameObject InfoArea;

    public Sprite healthPotion;
    public Sprite manaPotion;

    public bool purchaseItems = true;
    public bool salePopup = false;

    public int selected = -1;

    public List<Item> shopItems;
    public List<Item> playerItems;
    public List<Item> items;
    public List<GameObject> itemSlots;

    private int maxItems = 25;

    void Start()
    {
        //Debug Purposes
        if (Data.i == null) Data.i = new Inventory();
        if (Data.s == null) Data.s = new ShopInventory();

        playerInventory = Data.i;
        shopInventory = Data.s;

        playerItems = playerInventory.items;
        shopItems = shopInventory.items.Count > 0 ? shopInventory.items : GetShopItems();

        //Debugging Purposes
        playerItems.Add(new HealthPotion(healthPotion));
        shopItems.Add(new HealthPotion(manaPotion));
        playerInventory.gold += 500;

        SalePopup.SetActive(false);

        SetItems();

        PopulateItemFrames();
        PopulateItems();
        SetInfo();

        SellButton.GetComponent<Button>().onClick.AddListener(SellListener);
        BuyButton.GetComponent<Button>().onClick.AddListener(BuyListener);

        TransferButton.GetComponent<Button>().onClick.AddListener(TransferListener);

        SaleCancel.GetComponent<Button>().onClick.AddListener(SaleCancelListener);
        SaleConfirm.GetComponent<Button>().onClick.AddListener(SaleConfirmListener);
    }

    private List<Item> GetShopItems()
    {
        return new List<Item>();
    }

    private void SetItems()
    {
        if (purchaseItems)
        {
            items = shopItems;
        }
        else
        {
            items = playerItems;
        }
        Debug.Log("Items:" + items.Count);
    }

    private void SellListener()
    {
        if (salePopup) return;

        if (!purchaseItems) return;

        purchaseItems = false;
        selected = -1;

        Title.GetComponent<TMP_Text>().text = "Sell Items";

        shopItems = items;

        SetItems();
        PopulateItems();
        SetInfo();
    }

    private void BuyListener()
    {
        if (salePopup) return;

        if (purchaseItems) return;

        purchaseItems = true;
        selected = -1;

        Title.GetComponent<TMP_Text>().text = "Purchase Items";

        playerItems = items;

        SetItems();
        PopulateItems();
        SetInfo();
    }

    private void TransferListener()
    {
        if (salePopup) return;

        if (selected >= 0)
        {
            SalePopup.SetActive(true);
            salePopup = true;

            string type = purchaseItems ? "purchase" : "sell";
            string itemName = items[selected].getName() ?? "item";
            string value = "100";

            SaleText.GetComponent<TMP_Text>().text = "Would you like to " + type + " " + itemName + " for " + value + " gold?";
        }
    }

    //void trashListener()
    //{
    //    if (selected >= 0)
    //    {
    //        items.RemoveAt(selected);
    //        selected = -1;
    //        PopulateItems();
    //        SetInfo();
    //    }
    //}

    //void SaleListener()
    //{
    //    salePopup = true;
    //    SalePopup.SetActive(true);
    //}

    void SaleCancelListener()
    {
        salePopup = false;
        SalePopup.SetActive(false);
    }

    void SaleConfirmListener()
    {
        int value = 100;
        if (purchaseItems)
        {
            if(playerInventory.gold >= value)
            {
                Item item = items[selected];
                items.RemoveAt(selected);
                playerInventory.gold = playerInventory.gold - value;
                playerItems.Add(item);
                selected = -1;
            }
            else
            {
                //not enough money
                
            }
            
        }
        else // selling item
        {
            playerInventory.gold = playerInventory.gold + value;
            Item item = items[selected];
            items.RemoveAt(selected);
            shopItems.Add(item);
            selected = -1;
        }
        SetInfo();
        PopulateItems();
        salePopup = false;
        SalePopup.SetActive(false);
    }

    private void UpdateItems()
    {
        //playerBank.items = shopItems;
        playerInventory.items = playerItems;

        Data.i = playerInventory;
        //Data.b = playerBank;
    }

    void PopulateItemFrames()
    {

        int x = 0;
        int y = 0;
        float cellSize = 150;
        int xOff = 120;
        int yOff = -120;
        for (int i = 0; i < maxItems; i++)
        {
            Vector3 pos = new Vector3(xOff + x * cellSize, yOff + -1 * y * cellSize, 0);
            GameObject itemslot = GameObject.Instantiate(ItemSlot, pos, Quaternion.identity);
            //itemslot.GetComponent<RectTransform>().localScale = new Vector2(200 / 120, 200 / 120);
            //itemslot.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector2(150 / 120, 150 / 120);
            itemslot.transform.SetParent(ItemArea.transform, false);
            itemSlots.Add(itemslot);

            if (i < items.Count)
            {
                GameObject icon = itemslot.transform.GetChild(0).gameObject;
                icon.GetComponent<Image>().sprite = items[i].getImage();
            }
            else
            {
                GameObject icon = itemslot.transform.GetChild(0).gameObject;
                icon.SetActive(false);
            }

            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }

    }

    private void PopulateItems()
    {
        for (int i = 0; i < maxItems; i++)
        {
            if (i < items.Count)
            {
                Debug.Log("Item:" + i);
                GameObject icon = itemSlots[i].transform.GetChild(0).gameObject;
                icon.GetComponent<Image>().sprite = items[i].getImage();
                icon.SetActive(true);
            }
            else
            {
                GameObject icon = itemSlots[i].transform.GetChild(0).gameObject;
                icon.SetActive(false);
            }
        }
    }

    void SetInfo()
    {
        //set gold info
        GoldText.GetComponent<TMP_Text>().text = "Gold: " + playerInventory.gold.ToString();

        TransferButton.transform.GetChild(0).GetComponent<TMP_Text>().text = purchaseItems ? "Purchase" : "Sell";

        GameObject Name = InfoArea.transform.GetChild(0).gameObject;
        GameObject Icon = InfoArea.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        GameObject Flavor = InfoArea.transform.GetChild(2).gameObject;
        GameObject Detail = InfoArea.transform.GetChild(3).gameObject;

        if (selected >= 0)
        {
            Icon.SetActive(true);
            Icon.GetComponent<Image>().sprite = items[selected].getImage();
            Name.GetComponent<TMP_Text>().text = items[selected].getName();
            Flavor.GetComponent<TMP_Text>().text = items[selected].getFlavor();
            Detail.GetComponent<TMP_Text>().text = items[selected].getDetails();
        }
        else
        {
            Icon.SetActive(false);
            Name.GetComponent<TMP_Text>().text = "";
            Flavor.GetComponent<TMP_Text>().text = "";
            Detail.GetComponent<TMP_Text>().text = "";
        }
    }

    void Update()
    {
        if (salePopup)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (itemSlots[i].GetComponent<Clickable>().getClicked())
                {
                    selected = i;
                    itemSlots[i].GetComponent<Clickable>().setClicked(false);
                    Debug.Log("Detect select");
                }
            }
            SetInfo();
        }
    }

    public void ToTown()
    {
        if (salePopup) return;

        UpdateItems();
        SceneManager.LoadScene("Town");
    }
}
