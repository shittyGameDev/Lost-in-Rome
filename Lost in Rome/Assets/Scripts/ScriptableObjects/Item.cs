using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public string itemName; // Namnet på objektet
    public string itemDescription; // Beskrivningen av objektet
}
