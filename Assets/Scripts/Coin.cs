using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int amount;

    /**
     * Increase current user coins
     * @param int amount The amount to increase
     * @return void
     */
    public void IncreaseCoins()
    {
        GameManager.instance.IncreaseCoins(amount);
    }

    /**
     * Decrease current user coins
     * @param int amount The amount to decrease
     * @return void
     */
    public void DecreaseCoins()
    {
        GameManager.instance.DecreaseCoins(amount);
    }

    /**
     * On collision of game object call this
     * @param Colider colider Gets the references of what game object has colided with
     * @erturn void;
     */
    private void OnTriggerEnter(Collider colider)
    {
        Debug.Log(colider.tag);
        switch (colider.tag)
        {
            case "Player":
                GameManager.instance.IncreaseCoins(amount);
                // Destroy this
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    /**
     * Get the total coins that this class was assigned publically
     * @erturn int total amount was assigned outside;
     */
    public int GetAmount()
    {
        return amount;
    }
}
