using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter : MonoBehaviour
{
    public ItemSlot[] inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory[0].UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory[1].UseItem();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory[2].UseItem();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory[3].UseItem();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory[4].UseItem();
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].isEmpty)
            {
                inventory[i].SetItem(item);
                break;
            }

        }
    }
}
