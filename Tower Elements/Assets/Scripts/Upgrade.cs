using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private Node node;
    private GameObject turretTarget;

    BuildManager buildManager;

    Image upgradeTurret;
    Image sellTurret;
    Image upgradeOne;
    Image upgradeTwo;
    Image upgradeThree;

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


    public void UpgradeTurret()
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
