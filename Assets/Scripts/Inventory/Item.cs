using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override void Interact()
    {
        //Debug.Log(temp);
        if(InventoryManager.Instance.GetEmptyInventorySlot()!=-1&&id!="Door")
        {
            InventoryManager.Instance.AddItem(id);

            Destroy(this.gameObject);
        }
        else if(InventoryManager.Instance.KeyCheck())
        {
            if(id=="Door")
            {
                this.gameObject.SetActive(false);
                InventoryManager.Instance.KeyPurge();
            }
        }
    }
}