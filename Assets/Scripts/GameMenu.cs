using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    // Our panel menu
    private GameObject menu;

    // Use this for initialization
    private void Start()
    {
        // Get the panel menu
        menu = transform.GetChild(0).gameObject;

        ToggleGameMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if ESC was called
        if(Input.GetButtonDown("Cancel"))
        {
            bool isActive = !menu.activeSelf;

            ToggleGameMenu(isActive);
        }
    }

    /**
     * Continue the app
     * @return void
     */
    public void OnContinue()
    {
        ToggleGameMenu(false);
    }

    /**
     * Handle game menu visiblity
     * @return void
     */
    private void ToggleGameMenu(bool isActive)
    {
        Time.timeScale = isActive ? 0.0f : 1.0f;

        GameManager.instance.isPaused = isActive;

        menu.SetActive(isActive);
    }

    /**
     * @performance
     * When user focuses on the app means is no longer paused
     * Saving the app resources and doesn't use much cpu and ram % on their phone
     * @docslink https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html
     * @link https://answers.unity.com/questions/496290/can-somebody-explain-the-onapplicationpausefocus-s.html
     * @return void
     */
    void OnApplicationFocus(bool hasFocus)
    {
        if (!menu)
            return;

        GameManager.instance.isPaused = !hasFocus;

        ToggleGameMenu(!hasFocus);
    }

    /**
     * @performance
     * When user pauses the app
     * Saving the app resources and doesn't use much cpu and ram % on their phone
     * @docslink https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html
     * @link https://answers.unity.com/questions/496290/can-somebody-explain-the-onapplicationpausefocus-s.html
     * @return void
     */
    void OnApplicationPause(bool pauseStatus)
    {
        if (!menu)
            return;

        GameManager.instance.isPaused = pauseStatus;

        ToggleGameMenu(pauseStatus);
    }
}
