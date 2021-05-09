using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{ 
    private Node node;
    private GameObject turretTarget;
    public GameObject selectionArrowUI;

    BuildManager buildManager;

    public Image upgradeTurret;
    public Image sellTurret;
    public Image upgradeOne;
    public Image upgradeTwo;
    public Image upgradeThree;



    public void UpdateImages(Node _node)
    {
        node = _node;
        TurretBluePrint turretBlueprint = node.actualTurretBluePrint;

        upgradeTurret = turretBlueprint.turretToUpgradeImage;
        upgradeOne = turretBlueprint.upgradeOneImage;
        upgradeTwo = turretBlueprint.upgradeTwoImage;
        upgradeThree = turretBlueprint.upgradeThreeImage;
        sellTurret = turretBlueprint.sellTurretImage;

    }


    public void EvolveTurret()
    {
        node.EvolveTurret();
    }

    public void UpgradeOne()
    {
        node.UpgradeOne();
    }
    public void UpgradeTwo()
    {
        node.UpgradeTwo();
    }
    public void UpgradeThree()
    {
        node.UpgradeThree();
    }
    public void SellTurret()
    {
        node.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
