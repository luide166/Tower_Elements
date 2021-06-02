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

    private void Start()
    {

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
