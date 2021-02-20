using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameConstants;

public class GameUI : MonoBehaviour
{
    [Header("Funds")]
    [SerializeField]
    private TextMeshProUGUI CoinText;
    [Header("Alert")]
    [SerializeField]
    private Image NotificationBg;
    [SerializeField]
    private TextMeshProUGUI NotificationText;
    [SerializeField]
    private float NotificationTimer;
    private float LastNotificationTimer;
    [Header("Strikes")]
    [SerializeField]
    private Image strikesFill;
    [SerializeField]
    private TextMeshProUGUI strikesTotal;
    [Header("Pause Menu")]
    private GameObject pauseMenu;
    [Header("Enemies")]
    [SerializeField]
    private TextMeshProUGUI killedEnemiesTotal;
    // Expose the game ui instance as static
    public static GameUI instance;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        LastNotificationTimer -= Time.deltaTime;
        if (LastNotificationTimer <= 0.0f)
        {
            HideNotification();
        }
    }

    /**
     * Set the current amount of coins
     * @return void
     */
    public void SetCoinText(int currAmount, int maxAmount)
    {
        Debug.LogWarning(maxAmount);
        if (maxAmount > 0)
        {
            CoinText.text = string.Format("Coins: {0} / {1}", currAmount, maxAmount);
        } 
        else
        {
            CoinText.text = string.Format("Coins: {0}", currAmount);
        }
    }

    /**
     * Hide the notification
     * @return void
     */
    private void HideNotification()
    {
        NotificationBg.gameObject.SetActive(false);
    }

    /**
     * Show the notification
     * @param ALERT_TYPES type Get the current type for message
     * @param string text Get the text to be show for message
     * @return void
     */
    public void ShowNotification(ALERT_TYPES type, string text)
    {
        NotificationText.color = Color.white;
        NotificationText.text = text;
        NotificationBg.gameObject.SetActive(true);

        LastNotificationTimer = NotificationTimer;

        switch (type)
        {
            case ALERT_TYPES.Info:
                NotificationBg.color = Color.blue;
                break;
            case ALERT_TYPES.Error:
                NotificationBg.color = Color.red;
                break;
            case ALERT_TYPES.Warning:
                NotificationBg.color = Color.yellow;
                break;
            case ALERT_TYPES.Success:
                NotificationBg.color = Color.green;
                break;
            default:
                break;
        }
    }

    /**
     * Update strikes
     * @param int currStrikes Get total strikes left
     * @param int maxStrikes Get maximum strikes possible
     * @return void
     */
    public void UpdateStrikes(int currStrikes, int maxStrikes)
    {
        strikesFill.fillAmount = (float)currStrikes / (float)maxStrikes;
        strikesTotal.text = string.Format("{0} / {1}", currStrikes, maxStrikes);
    }

    /**
     * Update total enemies killed
     * @param int currEnemiesKilled Get total enemies killed
     * @param int maxEnemiesKill Get maximum enemies left to kill
     * @return void
     */
    public void SetKilledEnemiesText(int currEnemiesKilled, int maxEnemiesKill)
    {
        if (maxEnemiesKill > 0)
        {
            killedEnemiesTotal.text = string.Format("Enemies: {0} / {1}", currEnemiesKilled, maxEnemiesKill);
        } 
        else
        {
            killedEnemiesTotal.text = string.Format("Enemies: {0}", currEnemiesKilled);
        }
    }
}
