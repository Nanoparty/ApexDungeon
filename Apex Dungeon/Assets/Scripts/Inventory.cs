using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory 
{
    public List<Item> items;
    public List<GameObject> itemSlots;

    int maxItems = 25;
    public GameObject slot;
    public GameObject panel;
    private GameObject panelObject;
    public Sprite sprite;
    int selected = -1;
    Button use;
    Button trash;
    Button close;
    int gold = 0;

    bool closed = false;

    public Inventory(GameObject panel, GameObject slot)
    {
        this.panel = panel;
        this.slot = slot;
        items = new List<Item>();
        itemSlots = new List<GameObject>();
    }

    /*
    private void Start()
    {
        items = new List<Item>();
        itemSlots = new List<GameObject>();
        //openInventory();
        //setInfo();

    }
    */
    public bool addItem(Item i)
    {
        if(items.Count < maxItems)
        {
            items.Add(i);
        }
        return false;
    }

    void setInfo()
    {
        GameObject infoPanel = panelObject.transform.GetChild(0).gameObject;
        GameObject icon = infoPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        GameObject Name = infoPanel.transform.GetChild(1).gameObject;
        GameObject Flavor = infoPanel.transform.GetChild(2).gameObject;
        GameObject Detail = infoPanel.transform.GetChild(3).gameObject;

        if(selected >= 0)
        {
            icon.SetActive(true);
            icon.GetComponent<Image>().sprite = items[selected].getImage();
            Name.GetComponent<Text>().text = items[selected].getName();
            Flavor.GetComponent<Text>().text = items[selected].getFlavor();
            Detail.GetComponent<Text>().text = items[selected].getDetails();
            //selected = -1;
        }
        else
        {
            icon.SetActive(false);
            Name.GetComponent<Text>().text = "";
            Flavor.GetComponent<Text>().text = "";
            Detail.GetComponent<Text>().text = "";
            //Name.GetComponent<Text>().text = "";
        }
    }

    void useListener()
    {
        if (selected >= 0)
        {
            items[selected].useItem();
            items.RemoveAt(selected);
            selected = -1;
            updateInventory();
            setInfo();
        }
    }

    void trashListener()
    {
        Debug.Log("TRASH LISTENER");
        if(selected >= 0)
        {
            items.RemoveAt(selected);
            selected = -1;
            updateInventory();
            setInfo();
        }
    }

    void closeListener()
    {
        closeInventory();
        closed = true;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown("b"))
        {
            //closeInventory();
            addItem(new HealthPotion(sprite));
            Debug.Log(items.Count);
            updateInventory();
        }
        if (Input.GetKeyDown("a"))
        {
            items.RemoveAt(0);
            updateInventory();
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (itemSlots[i].GetComponent<Clickable>().getClicked())
                {
                    selected = i;
                    itemSlots[i].GetComponent<Clickable>().setClicked(false);
                    Debug.Log("Detect select");
                }
            }
            setInfo();
        }

       
    }

    public bool getClosed()
    {
        return closed;
    }
    public void setClosed(bool b)
    {
        closed = b;
    }

    void updateInventory()
    {
        for(int i = 0; i < maxItems; i++)
        {
            GameObject itemslot = itemSlots[i];

            if (i < items.Count)
            {
                Debug.Log("Update");
                GameObject icon = itemslot.transform.GetChild(0).gameObject;
                icon.GetComponent<Image>().sprite = items[i].getImage();
                icon.SetActive(true);
            }
            else
            {
                GameObject icon = itemslot.transform.GetChild(0).gameObject;
                icon.SetActive(false);
            }
        }
    }

    public void openInventory()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Inventory");
        panelObject = GameObject.Instantiate(panel, Vector3.zero, Quaternion.identity);
        panelObject.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform, false);
        //panelObject.transform.parent = parent.transform;

        use = panelObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<Button>();
        trash = panelObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<Button>();
        close = panelObject.transform.GetChild(2).gameObject.GetComponent<Button>();

        GameObject goldText = panelObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
        goldText.GetComponent<Text>().text = "Gold: " + gold;

        use.onClick.AddListener(useListener);
        trash.onClick.AddListener(trashListener);
        close.onClick.AddListener(closeListener);

        int x = 0;
        int y = 0;
        float cellSize = 120;
        int xOff = 100;
        int yOff = -100;
        for(int i = 0; i < maxItems; i++)
        {
            Vector3 pos = new Vector3(xOff + x * cellSize,yOff + -1*y * cellSize, 0);
            GameObject itemslot = GameObject.Instantiate(slot, pos, Quaternion.identity);
            itemslot.transform.SetParent(panelObject.transform, false);
            Debug.Log("TEST:" + itemslot + ":" + itemSlots);
            itemSlots.Add(itemslot);
            if(i < items.Count)
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
        setInfo();
    }

    public void setGold(int i)
    {
        gold = i;
        //GameObject goldText = panelObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
        //goldText.GetComponent<Text>().text = "Gold: " + gold;
    }
    
    public void closeInventory()
    {
        GameObject.Destroy(panelObject);
        itemSlots.Clear();
    }
}
