using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class Portal : MonoBehaviour
{
    [Header("Current Player")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private bool isTeleport;

    // Start is called before the first frame update
    void Start()
    {
        if (isTeleport)
        {
            InitTotalRequiredCoins();
            InitTotalRequiredEnemies();
        }
    }

    /**
     * Init the total required coins 
     * @erturn void;
     */
    private void InitTotalRequiredCoins()
    {
        if (GameManager.instance.GetRequiresAllCoins())
        {
            int requiredMaxCoins = 0;
            // Get entire list of objects that has "Coin" tag and store it in game objects array
            GameObject[] totalCoins = GameObject.FindGameObjectsWithTag("Coin");
            // Then we go for each coin that we found
            foreach (GameObject coin in totalCoins)
            {
                // We try to get the specific component as Coin, and get for us the amount that it gives
                requiredMaxCoins += coin.GetComponent<Coin>().GetAmount();
            }
            // We assign to game manager this info
            GameManager.instance.SetMaxOwnedCoins(requiredMaxCoins);
        } 
    }

    /**
     * Init the total required coins 
     * @erturn void;
     */
    private void InitTotalRequiredEnemies()
    {
        if (GameManager.instance.GetRequiresAllEnemies())
        {
            // Get entire list of objects that has "enemy" tag and get the total length
            // In this case we gonna have 1 enemy not multi enemies in same enemy, unlike coons. Getting the total number is always right
            GameManager.instance.SetMaxKilledEnemies(GameObject.FindGameObjectsWithTag("Enemy").Length);
        }
    }

    /**
     * Check if user has completed the level
     * @erturn void;
     */
    private void CheckCompletedLevel()
    {
        int totalOwnedCoins = GameManager.instance.GetOwnedCoins();
        int maxRequiredCoins = GameManager.instance.GetMaxOwnedCoins();
        int currKilledEnemies = GameManager.instance.GetCurrKilledEnemies();
        int maxRequiredEnemies = GameManager.instance.GetMaxKilledEnemies();
        // We must add 2 conditions
        // 1. if has enough coins to surpass the level
        // 2. If killed total enemies required
        if (totalOwnedCoins >= maxRequiredCoins &&
            currKilledEnemies >= maxRequiredEnemies)
        {
            GameManager.instance.LoadNextLevel();
        }
        else if (totalOwnedCoins < maxRequiredCoins)
        {
            //Push back to the init of the map
            PushOnOutOfBounce();
            // Show error message
            GameUI.instance.ShowNotification(ALERT_TYPES.Error, string.Format("Not enough coins! You need to get at least {0}", maxRequiredCoins));
        }
        else if (currKilledEnemies < maxRequiredEnemies)
        {
            //Push back to the init of the map
            PushOnOutOfBounce();
            // Show error message
            GameUI.instance.ShowNotification(ALERT_TYPES.Error, string.Format("You must kill enemies! You need to get at least {0}", maxRequiredEnemies));
        }
    }

    /**
     * On collision of game object call this
     * @param Colider colider Gets the references of what game object has colided with
     * @erturn void;
     */
    private void OnTriggerEnter(Collider colider)
    {
        switch (colider.tag)
        {
            case "Player":
                if (isTeleport)
                {
                    CheckCompletedLevel();
                } 
                else
                {
                    //Push back to the init of the map
                    PushOnOutOfBounce();
                }
                break;
            default:
                break;
        }
    }

    /**
     * Push on out of bounce
     * @erturn void;
     */
    void PushOnOutOfBounce()
    {
        Vector3 position = GameManager.instance.GetInitialPoint();
        // Setup player position back to intial point
        player.transform.position = new Vector3(position.x, position.y, position.z);
    }
}
