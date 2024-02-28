using UnityEngine;

public class SymbolSpawning : MonoBehaviour
{
    public Sprite[] symbols; // Array av symboler
    public static GameObject currentSymbolObject;

    void Start()
    {
        SpawnSymbol();
    }

    void SpawnSymbol()
    {
        int randomIndex = Random.Range(0, symbols.Length);
        currentSymbolObject = new GameObject("Symbol");
        SpriteRenderer renderer = currentSymbolObject.AddComponent<SpriteRenderer>();
        renderer.sprite = symbols[randomIndex];
        currentSymbolObject.transform.position = transform.position + Vector3.up * 2f;
        currentSymbolObject.transform.parent = transform;
    }
}
