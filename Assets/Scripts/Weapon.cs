using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class Weapon : MonoBehaviour
{
    [Header("About")]
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private float attckRate;
    private float lastAttck;
    [Header("Strikes")]
    [SerializeField]
    private int maxStrikes;
    [SerializeField]
    private int currStrikes;
    [SerializeField]
    private bool hasInfiniteStrikes;

    private void Start()
    {
        gameObject.SetActive(false);

        if (isPlayer)
        {
            GameUI.instance.UpdateStrikes(currStrikes, maxStrikes);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastAttck -= Time.deltaTime;

        if (GameManager.instance.isPaused)
            return;

        if (CanAttack())
        {
            gameObject.SetActive(false);
        }
    }

    /**
     * Can weapon be used to attack?
     * @return bool
     */
    private bool CanAttack()
    {
        return lastAttck <= 0;
    }

    /**
     * Use weapon to attck
     * @return bool
     */
    public void Attck()
    {
        if (CanAttack())
        {
            lastAttck = attckRate;

            if (currStrikes > 0 || hasInfiniteStrikes)
            {

                ReduceStrikesAmount();

                gameObject.SetActive(true);
            } else
            {
                GameUI.instance.ShowNotification(ALERT_TYPES.Error, string.Format("You have no more strikes left...Try to survive!"));
            }
        }
    }

    /**
     * On collision of game object call this
     * @param Colider colider Gets the references of what game object has colided with
     * @erturn void;
     */
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.CompareTag("Enemy"))
        {
            GameManager.instance.IncreaseKilledEnemies(1);

            Destroy(collider.gameObject);
        }
    }

    /**
     * Reduce the amount of ammo
     * @return bool
     */
    private void ReduceStrikesAmount()
    {
        currStrikes = Mathf.Clamp((currStrikes - 1), 0, maxStrikes);

        if(isPlayer)
        {
            GameUI.instance.UpdateStrikes(currStrikes, maxStrikes);

            if (currStrikes == 0)
            {
                GameUI.instance.ShowNotification(ALERT_TYPES.Error, string.Format("You have no more strikes left...Try to survive!"));
            }
        }
    }
}
