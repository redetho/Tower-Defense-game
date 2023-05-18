using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;
    int addCoins;

    void Start()
    {
        coinText.text = "Coins: 0";
        
    }

    void Update()
    {
        addCoins = Random.Range(1, 11);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Coin"))
                {
                    Destroy(hit.collider.gameObject);
                    coinCount += addCoins;
                    coinText.text = coinCount.ToString();
                }
            }
        }
    }
}
