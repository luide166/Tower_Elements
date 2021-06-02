using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("Standard UI")]
    [Space(10)]
    public GameObject pauseUI;
    public GameObject gameOverUI;

    [Header("Gameplay UI")]
    [Space(10)]
    public GameObject turretsUI;
    public GameObject upgradeUI;
    public GameObject speelsUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }



    public void ActivateTurretsUI()
    {
        DisableAllUI();
        turretsUI.SetActive(true);
    }
    public void ActivateUpgradeUI(Node _node)
    {
        DisableAllUI();
        upgradeUI.SetActive(true);
        upgradeUI.GetComponent<Upgrade>().UpdateImages(_node);
    }
    public void ActivateSpellsUI()
    {
        DisableAllUI();
        speelsUI.SetActive(true);
    }

    private void DisableAllUI()
    {
        turretsUI.SetActive(false);
        upgradeUI.SetActive(false);
        speelsUI.SetActive(false);
    }




}
