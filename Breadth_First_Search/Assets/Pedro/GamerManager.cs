using UnityEngine;
using UnityEngine.UI;

public class GamerManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;

    

    void Start()
    {
        coinText.text = "Coins: 0";

    }

    public void AddPoints()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;

    }
    
    public bool CanBuildTower(int towerCost)
    {
        return coinCount >= towerCost;
    }

    public void SpendPoints(int pointsToSpend)
    {
        coinCount -= pointsToSpend;
        coinText.text = "Coins: " + coinCount;
    }
    
}
