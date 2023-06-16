using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    //For now, this will store information of the Items that can be added to the inventory
    public List<ItemData> itemDatabase;

    //Store all the inventory slots in the scene here
    public List<InventorySlot> inventorySlots;

    //Store all the equipment slots in the scene here
    public List<EquipmentSlot> equipmentSlots;

    //Singleton implementation. Do not change anything within this region.
    #region SingletonImplementation
    private static InventoryManager instance = null;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Inventory";
                    instance = go.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void UseItem(ItemData data)
    {
        // TODO
        // If the item is a consumable, simply add the attributes of the item to the player.
        // If it is equippable, get the equipment slot that matches the item's slot.
        if(data.type==ItemType.Consumable)
            {
            player.AddAttributes(data.attributes);
            }
        else if(data.type==ItemType.Equipabble)
        {
                player.AddAttributes(data.attributes);
                equipmentSlots[GetEquipmentSlot(data.slotType)].SetItem(data);
        }

    }

   
    public void AddItem(string itemID)
    {
        //TODO
        //1. Cycle through every item in the database until you find the item with the same id.
        //2. Get the index of the InventorySlot that does not have any Item and set its Item to the Item found
        for (int i = 0; i <= itemDatabase.Count; i++)
        { 
            if(GetEmptyInventorySlot()!=-1)
            {
                if(itemDatabase[i].id==itemID)
                {
                    //Debug.Log("triggered   current    " + i);
                    inventorySlots[GetEmptyInventorySlot()].SetItem(itemDatabase[i]);
                    break;
                }
            }

        }
        //Debug.Log("new index " + GetEmptyInventorySlot());
    }

    public int GetEmptyInventorySlot()
    {
        //TODO
        //Check which inventory slot doesn't have an Item and return its index
        int temp=-1;
        for(int i = 0; i<=inventorySlots.Count-1;i++)
        {
            if (!inventorySlots[i].HasItem())
                {
                temp = i;
                break;
            }
        }
        return temp;
    }

    public int GetEquipmentSlot(EquipmentSlotType type)
    {
        //TODO
        //Check which equipment slot matches the slot type and return its index
        int temp = -1;
        for (int i = 0; i <= equipmentSlots.Count - 1; i++)
        {
            if (equipmentSlots[i].type == type)
            {
                if(!equipmentSlots[i].HasItem())
                {
                    temp = i;
                    break;
                }
            }
        }
        return temp;
    }

    public bool KeyCheck()
    {
        bool temp = false;
        for (int i = 0; i <= inventorySlots.Count - 1; i++)
        {
            if (inventorySlots[i].HasItem())
            {
                if (inventorySlots[i].HasKey())
                {
                    temp = true;
                    break;
                }
            }
        }
        return temp;
    }

    public void KeyPurge()
    {
        for (int i = 0; i <= inventorySlots.Count - 1; i++)
        {
            if (inventorySlots[i].HasItem())
            {
                if (inventorySlots[i].HasKey())
                {
                    inventorySlots[i].DestroyKey();
                }
            }
        }
    }




    //it is 5am, i have been awake for 24 hrs
}
