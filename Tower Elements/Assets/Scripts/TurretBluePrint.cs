using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBluePrint 
{

    [Header("Turret Settings")]
    public TurretName turretName;
    public GameObject turretPrefab;
    public GameObject turretUpgradedPrefab;

    [Header("Update Stats")]

    public int turretLevel;
    public int spentMoneyOnThis;
    [Space(15)]

    [HideInInspector] public int upgradeOneLevel;

    [Header("Upgrade One Stats")]
    [Space(15)]
    public int upgradeOneCost1;
    public int upgradeOneValue1;
    [Space(8)]
    public int upgradeOneCost2;
    public int upgradeOneValue2;
    [Space(8)]
    public int upgradeOneCost3;
    public int upgradeOneValue3;

    [HideInInspector] public int upgradeTwoLevel;

    [Header("Upgrade Two Stats")]
    [Space(15)]
    public int upgradeTwoCost1;
    public int upgradeTwoValue1;
    [Space(8)]
    public int upgradeTwoCost2;
    public int upgradeTwoValue2;
    [Space(8)]
    public int upgradeTwoCost3;
    public int upgradeTwoValue3;

    [HideInInspector] public int upgradeThreeLevel;

    [Header("Upgrade Three Stats")]
    [Space(15)]
    public int upgradeThreeCost1;
    public int upgradeThreeValue1;
    [Space(8)]
    public int upgradeThreeCost2;
    public int upgradeThreeValue2;
    [Space(8)]
    public int upgradeThreeCost3;
    public int upgradeThreeValue3;


    [Header("UI Resources")]
    [Space(15)]
    public Image turretToUpgradeImage;
    public Image upgradeOneImage;
    public Image upgradeTwoImage;
    public Image upgradeThreeImage;
    public Image sellTurretImage;


    [Header("Costs")]
    public int buildCost;
    public int evolveCost;
    public int sellCost;

    [Header("Sounds")]
    public AudioClip buildSound;
    public AudioClip upgradeSound;
    public AudioClip atackSound;
    public AudioClip sellSound;

    [Header("Effects")]
    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject evolveEffect;
    public GameObject sellEffect;

}
