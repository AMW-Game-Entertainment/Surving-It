using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Levels")]
    [SerializeField]
    private int level;
    [SerializeField]
    private bool isLastLevel;
    private int nextLevel;
    [Header("Initial Game Funds")]
    [SerializeField]
    private int currCoins;
    [SerializeField]
    private int maxCoins;
    [SerializeField]
    private bool requiresAllCoins;
    [Header("Player")]
    [SerializeField]
    private Vector3 initialPoint;
    [Header("Configs")]
    public bool isPaused;

    [Header("Enemies")]
    [SerializeField]
    private int currKillEnemiesRequired;
    [SerializeField]
    private int maxKillEnemiesRequired;
    [SerializeField]
    private bool requiresAllEnemies;

    // Exposing singleton instance
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isAtMainMenu())
            return;

        nextLevel = (level + 1);

        if (!GetRequiresAllEnemies())
        {
            SetMaxKilledEnemies(maxKillEnemiesRequired);
        }
        if (!GetRequiresAllCoins())
        {
            SetMaxOwnedCoins(maxCoins);
        }
    }

    /**
     * Load assigned level
     * @param int level The level that we will load
     * @return void
     */
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(string.Format("Level-{0}", level));
    }

    /**
     * Load next level scene
     * @return void
     */
    public void LoadNextLevel()
    {
        if(isLastLevel)
        {
            OnMainMenu();

            return;
        }

        LoadLevel(nextLevel);
    }

    /**
     * Restart the current level
     * @return void
     */
    public void RestartCurrentLevel()
    {
        isPaused = false;

        LoadLevel(level);
    }

    /**
     * Close the app
     * @return void
     */
    public void CloseApp()
    {
        isPaused = false;

        Application.Quit();
    }

    /**
     * Main menu
     * @return void
     */
    public void OnMainMenu()
    {
        isPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    /**
     * Increase current user coins
     * @param int amount The amount to increase
     * @return void
     */
    public void IncreaseCoins(int amount)
    {
        currCoins += Mathf.Clamp(amount, 0, maxCoins);

        GameUI.instance.SetCoinText(currCoins, maxCoins);
    }

    /**
     * Decrease current user coins
     * @param int amount The amount to decrease
     * @return void
     */
    public void DecreaseCoins(int amount)
    {
        currCoins -= Mathf.Clamp(amount, 0, maxCoins);

        GameUI.instance.SetCoinText(currCoins, maxCoins);
    }

    /**
     * Check if it does requires all coins to win. If it does it auto generates the values
     * @return int
     */
    public bool GetRequiresAllCoins()
    {
        return requiresAllCoins;
    }

    /**
     * Get Total owned coins
     * @return int
     */
    public int GetOwnedCoins()
    {
        return currCoins;
    }

    /**
     * Get Total max coins obtainable
     * @return int
     */
    public int GetMaxOwnedCoins()
    {
        return maxCoins;
    }

    /**
     * Set Total max coins obtainable
     * @return int
     */
    public void SetMaxOwnedCoins(int amount)
    {
        maxCoins = amount;
        Debug.Log(maxCoins);
        GameUI.instance.SetCoinText(0, maxCoins);
    }


    /**
     * Increase current user killed enemies
     * @param int amount The amount to increase
     * @return void
     */
    public void IncreaseKilledEnemies(int amount)
    {
        if (maxKillEnemiesRequired > 0)
        {
            currKillEnemiesRequired += Mathf.Clamp(amount, 0, maxKillEnemiesRequired);
        } else
        {
            currKillEnemiesRequired += amount;
        }

        GameUI.instance.SetKilledEnemiesText(currKillEnemiesRequired, maxKillEnemiesRequired);
    }

    /**
     * Decrease current user killed enemies
     * @param int amount The amount to decrease
     * @return void
     */
    public void DecreaseKilledEnemies(int amount)
    {
        if (maxKillEnemiesRequired > 0)
        {
            currKillEnemiesRequired -= Mathf.Clamp(amount, 0, maxKillEnemiesRequired);
        } else
        {
            currKillEnemiesRequired -= amount;
        }

        GameUI.instance.SetKilledEnemiesText(currKillEnemiesRequired, maxKillEnemiesRequired);
    }

    /**
     * Check if it does requires all enemies to win. If it does it auto generates the values
     * @return int
     */
    public bool GetRequiresAllEnemies()
    {
        return requiresAllEnemies;
    }

    /**
     * Get Total killed enemies
     * @return int
     */
    public int GetCurrKilledEnemies()
    {
        return currKillEnemiesRequired;
    }

    /**
     * Get Total enemies needed to be killeed
     * @return int
     */
    public int GetMaxKilledEnemies()
    {
        return maxKillEnemiesRequired;
    }

    /**
     * Set Total enemies needed to be killeed
     * @return int
     */
    public void SetMaxKilledEnemies(int amount)
    {
        maxKillEnemiesRequired = amount;

        GameUI.instance.SetKilledEnemiesText(0, maxKillEnemiesRequired);
    }

    /**
     * Get intial point
     * @return void
     */
    public Vector3 GetInitialPoint()
    {
        return initialPoint;
    }

    /***
     * Check if is at main menu
     * @return bool
     */
    public bool isAtMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}
