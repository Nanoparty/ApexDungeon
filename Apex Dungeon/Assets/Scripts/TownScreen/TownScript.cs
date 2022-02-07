using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownScript : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventorySlot;

    private bool openTavern = false;
    private bool openMerchant = false;
    private bool openBlacksmith = false;
    private bool openBank = false;

    private bool townOpen = true;

    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        if(Data.i == null)
        {
            Data.i = new Inventory(inventoryPanel, inventorySlot);
        }

        inventory = Data.i;
    }

    // Update is called once per frame
    void Update()
    {
        if (openBank)
        {
            inventory.Update();
            if (inventory.getClosed())
            {
                inventory.setClosed(false);
                openBank = false;
            }
            return;
        }
    }

    public void clickBank()
    {
        SceneManager.LoadScene("Bank");
    }

    public void toDungeon()
    {
        SceneManager.LoadScene("test");
    }


}
