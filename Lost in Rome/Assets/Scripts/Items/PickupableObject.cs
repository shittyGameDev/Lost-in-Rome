using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    public Item item; // Referens till scriptet för det objekt som kan plockas upp

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Om spelaren krockar med objektet
            Inventory inventory = other.gameObject.GetComponent<Inventory>(); // Hämta spelarens inventory
            inventory.AddItem(item); // Lägg till objektet till spelarens inventory
            inventory.ShowItems(); // Visa alla objekt i spelarens inventory
            Destroy(gameObject); // Ta bort objektet från scenen
        }
    }
}
