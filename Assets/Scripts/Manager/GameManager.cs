using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool onInventory = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!onInventory)
            {
                InventoryManager.Instance.Show();
                onInventory = true;
            }
            else
            {
                InventoryManager.Instance.Hide();
                onInventory = false;
            }
        }
        

    }
}
