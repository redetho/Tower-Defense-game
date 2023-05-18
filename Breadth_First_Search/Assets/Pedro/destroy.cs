using UnityEngine;

public class destroy : MonoBehaviour
{
    public int coins = 0; // Contador de moedas

    private void OnMouseDown()
    {
        Destroy(gameObject); // Destrói o objeto que foi clicado
        coins++; // Adiciona +1 ao contador de moedas
        Debug.Log($"Object {name} destroyed! Coins: {coins}");
    }
}
