using UnityEngine;
using UnityEngine.UI;

public class GamerManager : MonoBehaviour
{
    
    public int coinCount = 0;
    public Text coinText;
    public Text waveCountdownText;
    public Slider enemyHealthBar;
    private AnthillHealth _anthillHealth;
    
    void Start()
    {
        _anthillHealth = GetComponent<AnthillHealth>();
        coinText.text = "Materiais: 0";
        enemyHealthBar.value = enemyHealthBar.maxValue;
    }

    public void AddPoints()
    {
        coinCount++;
        coinText.text = "Materiais: " + coinCount;

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
    public void UpdateEnemyHealth(int health)
    {
        enemyHealthBar.maxValue = _anthillHealth.maxHealth;
        enemyHealthBar.value = _anthillHealth.currentHealth;
    }
    public void GameOver()
    {
        // Display the game over screen or perform other game over logic
    }
    public void UpdateWaveCountdown(float countdown)
    {
        waveCountdownText.text = "Next Wave In: " + Mathf.Ceil(countdown).ToString();
    }
}
