using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAnimationHandler : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inventory inventory = other.gameObject.GetComponent<Inventory>();
            Item wineItem = inventory.FindItemByName("Wine");

            if(wineItem != null)
            {
                anim.SetTrigger("Blend");
                inventory.RemoveItem(wineItem);
                inventory.ShowItems();
            }
        }
    }
}
