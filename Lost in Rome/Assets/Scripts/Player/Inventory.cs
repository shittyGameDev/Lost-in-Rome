using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int inventorySize; // Storleken på din inventering
    public List<Item> items; // En lista för att lagra alla objekt i inventeringen

    private void Start()
    {
        items = new List<Item>(); // Initiera listan
    }

    public void AddItem(Item item)
    {
        // Kontrollera om inventeringen är full
        if (items.Count < inventorySize)
        {
            items.Add(item); // Lägg till objektet till listan
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item); // Ta bort objektet från listan
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item); // Kontrollera om objektet finns i listan
    }

    //Visa alla objekt i listan
    public void ShowItems()
    {
        foreach (Item item in items)
        {
            Debug.Log(item.itemName);
        }
    }

    public Item FindItemByName(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                return items[i];
            }
        }
        return null;
    }

}
