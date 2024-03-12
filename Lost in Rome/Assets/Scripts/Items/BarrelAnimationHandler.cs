using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BarrelAnimationHandler : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI text;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inventory inventory = other.gameObject.GetComponent<Inventory>();
            Item waterItem = inventory.FindItemByName("Water");
            Item ashItem = inventory.FindItemByName("Ash");
            Item limeItem = inventory.FindItemByName("Lime");

            if(waterItem != null && ashItem != null && limeItem != null)
            {
                anim.SetTrigger("Blend");
                inventory.RemoveItem(waterItem);
                inventory.RemoveItem(ashItem);
                inventory.RemoveItem(limeItem);
                inventory.ShowItems();
                other.GetComponent<QuestManager>().questCompleted = true;

                text.text = "You made a barrel of concrete!";
            }
        }
    }
}
