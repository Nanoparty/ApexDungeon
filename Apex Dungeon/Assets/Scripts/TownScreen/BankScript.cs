using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BankScript : MonoBehaviour
{
    private Inventory playerInventory;
    private Bank playerBank;

    public GameObject DepositButton;
    public GameObject WithdrawButon;

    public GameObject TransferButton;
    public GameObject TrashButton;

    public GameObject GoldText;
    public GameObject GoldTransferButton;

    public GameObject GoldPopUp;
    public GameObject GoldInput;
    public GameObject GoldCancel;
    public GameObject GoldMax;
    public GameObject GoldConfirm;

    public GameObject ItemSlot;
    public GameObject ItemArea;
    public GameObject Title;

    public GameObject InfoArea;

    public Sprite healthPotion;
    public Sprite manaPotion;

    public bool depositItems = true;
    public bool goldPopup = false;

    public int selected = -1;

    public List<Item> bankItems;
    public List<Item> playerItems;
    public List<Item> items;
    public List<GameObject> itemSlots;

    private int maxItems = 25;

    void Start()
    {
        //Debug Purposes
        if (Data.i == null) Data.i = new Inventory();
        if (Data.b == null) Data.b = new Bank();

        playerInventory = Data.i;
        playerBank = Data.b;

        playerItems = playerInventory.items;
        bankItems = playerBank.items;

        //Debugging Purposes
        playerItems.Add(new HealthPotion(healthPotion));
        bankItems.Add(new HealthPotion(manaPotion));
        playerBank.gold += 100;
        playerInventory.gold += 500;

        GoldPopUp.SetActive(false);

        SetItems();

        PopulateItemFrames();
        PopulateItems();
        SetInfo();

        WithdrawButon.GetComponent<Button>().onClick.AddListener(WithdrawListener);
        DepositButton.GetComponent<Button>().onClick.AddListener(DepositListener);

        TransferButton.GetComponent<Button>().onClick.AddListener(TransferListener);
        TrashButton.GetComponent<Button>().onClick.AddListener(trashListener);

        GoldTransferButton.GetComponent<Button>().onClick.AddListener(goldListener);
        GoldCancel.GetComponent<Button>().onClick.AddListener(goldCancelListener);
        GoldMax.GetComponent<Button>().onClick.AddListener(goldMaxListener);
        GoldConfirm.GetComponent<Button>().onClick.AddListener(goldConfirmListener);
    }

    private void SetItems()
    {
        if (depositItems)
        {
            items = playerItems;         
        }
        else
        {
            items = bankItems;
        }
        Debug.Log("Items:" + items.Count);
    }

    private void WithdrawListener()
    {
        if (!depositItems) return;

        depositItems = false;
        selected = -1;

        Title.GetComponent<TMP_Text>().text = "Withdraw Items";

        playerItems = items;

        SetItems();
        PopulateItems();
        SetInfo();
    }

    private void DepositListener()
    {
        if (depositItems) return;

        depositItems = true;
        selected = -1;

        Title.GetComponent<TMP_Text>().text = "Deposit Items";

        bankItems = items;

        SetItems();
        PopulateItems();
        SetInfo();
    }

    private void TransferListener()
    {
        if (selected >= 0)
        {
            Item item = items[selected];
            items.RemoveAt(selected);
            selected = -1;
            PopulateItems();
            SetInfo();

            if (depositItems)
            {
                bankItems.Add(item);
            }
            else
            {
                playerItems.Add(item);
            }
        }
    }

    void trashListener()
    {
        if (selected >= 0)
        {
            items.RemoveAt(selected);
            selected = -1;
            PopulateItems();
            SetInfo();
        }
    }

    void goldListener()
    {
        goldPopup = true;
        GoldPopUp.SetActive(true);
    }

    void goldCancelListener()
    {
        goldPopup = false;
        GoldPopUp.SetActive(false);
    }

    void goldMaxListener()
    {
        if (depositItems)
        {
            GoldInput.GetComponent<TMP_InputField>().text = playerInventory.gold.ToString();
        }
        else
        {
            GoldInput.GetComponent<TMP_InputField>().text = playerBank.gold.ToString();
        }
        
    }

    void goldConfirmListener()
    {
        int goldDiff = int.Parse(GoldInput.GetComponent<TMP_InputField>().text);
        if (depositItems)
        {
            playerInventory.gold = playerInventory.gold - goldDiff;
            playerBank.gold = playerBank.gold + goldDiff;
        }
        else
        {
            playerInventory.gold = playerInventory.gold + goldDiff;
            playerBank.gold = playerBank.gold - goldDiff;
        }
        SetInfo();
        goldPopup = false;
        GoldPopUp.SetActive(false);
    }

    private void UpdateItems()
    {
        playerBank.items = bankItems;
        playerInventory.items = playerItems;

        Data.i = playerInventory;
        Data.b = playerBank;
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
            if(x > 4)
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
        if (depositItems) GoldText.GetComponent<TMP_Text>().text = "Gold: " + playerInventory.gold.ToString();
        else GoldText.GetComponent<TMP_Text>().text = "Gold: " + playerBank.gold.ToString();

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
        if (goldPopup)
        {
            int maxGold = depositItems ? playerInventory.gold : playerBank.gold;
            if(int.Parse(GoldInput.GetComponent<TMP_InputField>().text) > maxGold)
            {
                GoldInput.GetComponent<TMP_InputField>().text = maxGold.ToString();
            }
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
        UpdateItems();
        SceneManager.LoadScene("Town");
    }
}
