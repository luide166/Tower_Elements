using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Unity Stats")]

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBluePrint turretBlueprint;
    public TurretBluePrint actualTurretBluePrint;
    public int turretLevel;
    public int sellPercentage;
    public Vector3 offset;

    BuildManager buildManager;

    [Header("VFX Stuff")]
    [Space(5)]
    public GameObject canBuildEffect;
    public GameObject cantBuildEffect;
    public GameObject[] fireEffect;
    public GameObject[] iceEffect;
    public GameObject[] iceRayEffect;
    public GameObject[] lightiningEffect;
    public GameObject[] poisonEffect;

    private void Start()
    {
        buildManager = BuildManager.instance;

    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }

        if (buildManager.hasMoney)
        {
            canBuildEffect.SetActive(true);

        }
        else
        {
            cantBuildEffect.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);

            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }

        actualTurretBluePrint = buildManager.GetTurretToBuild();
        BuildTurret(actualTurretBluePrint);

    }

    private void OnMouseExit()
    {
        canBuildEffect.SetActive(false);
        cantBuildEffect.SetActive(false);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }

    void BuildTurret(TurretBluePrint _blueprint)
    {
        if (PlayerStats.money < _blueprint.buildCost)
        {
            return;
        }

        PlayerStats.money -= _blueprint.buildCost;

        GameObject _turret = Instantiate(_blueprint.turretPrefab, GetBuildPosition(), _blueprint.turretPrefab.transform.rotation);
        turret = _turret;

        turretLevel = 0;
        VerifyTurretLevel();

        // faz mandar o efeito certo
        //GameObject effect = Instantiate(_blueprint.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 4f);
    }

    public void UpgradeOne()
    {
        TurretBluePrint bluePrint = actualTurretBluePrint;

        switch (bluePrint.turretName)
        {
            #region Fire Turret Simple Damage Upgrade
            case TurretName.Fire: // Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue1;
                        print("Upgrade Fire Damage to: " + turret.GetComponent<Turrets>().damageOverTime + " for $" + bluePrint.upgradeOneCost1);
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue2;
                        print("Upgrade Fire Damage to: " + turret.GetComponent<Turrets>().damageOverTime + " for $" + bluePrint.upgradeOneCost2);
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue3;
                        print("Upgrade Fire Damage to: " + turret.GetComponent<Turrets>().damageOverTime + " for $" + bluePrint.upgradeOneCost3);
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Fire");
                        break;
                }
                break;

            #endregion

            #region Lava Turret Simple Damage Upgrade
            case TurretName.Lava: //Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Lava");
                        break;
                }
                break;
            #endregion

            #region Rock Turret Simple Damage Upgrade
            case TurretName.Rock: //Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Rock");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Rock");
                            return;
                        }
                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Rock");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damage += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Rock");
                        break;
                }
                break;
            #endregion

            #region Ice Turret Simple Damage Upgrade
            case TurretName.Ice: //Simple Damage Upgrade (On first Enemy)
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Ice");
                            return;
                        }
                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Ice");
                            return;
                        }
                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Ice");
                            return;
                        }
                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Ice");
                        break;
                }
                break;
            #endregion

            #region Ice Ray Turret Simple Damage Upgrade
            case TurretName.IceRay: //Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Ice Ray");
                        break;
                }
                break;
            #endregion

            #region Lightining Turret Simple Damage Upgrade
            case TurretName.Lightining: //Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Lightning");
                        break;
                }
                break;
            #endregion

            #region Poison Turret Simple Damage Upgrade
            case TurretName.Poison: //Simple Damage Upgrade
                switch (bluePrint.upgradeOneLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeOneCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost1;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue1;
                        bluePrint.upgradeOneLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeOneCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost2;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue2;
                        bluePrint.upgradeOneLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeOneCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeOneCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeOneCost3;

                        turret.GetComponent<Turrets>().damageOverTime += bluePrint.upgradeOneValue3;
                        bluePrint.upgradeOneLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Poison");
                        break;
                }
                break;
                #endregion

        }

        turretLevel++;
        VerifyTurretLevel();
    }

    public void UpgradeTwo()
    {
        TurretBluePrint bluePrint = actualTurretBluePrint;

        switch (bluePrint.turretName)
        {
            #region Fire Turret Move Speed Modifier Upgrade
            case TurretName.Fire: // Reduce Move Speed Boost
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Effect Buff Upgrade 1 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().speedModifier -= bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Effect Buff Upgrade 2 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().speedModifier -= bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Effect Buff Upgrade 3 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().speedModifier -= bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Effect Buff Upgrade - Fire");
                        break;
                }
                break;

            #endregion

            #region Lava Turret Other Target Damage Upgrade
            case TurretName.Lava: // Upgrade Damage on other Target Enemies
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 1 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 2 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 1 - Lava");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Other Target Damage Upgrade - Lava");
                        break;
                }
                break;
            #endregion

            #region Rock Turret Other Target Damage Upgrade
            case TurretName.Rock: //Simple Damage Upgrade
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 1 - Rock");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 2 - Rock");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 3 - Rock");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().bulletPrefab.GetComponent<Bullet>().damageOtherTarget += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Other Target Damage Upgrade - Rock");
                        break;
                }
                break;
            #endregion

            #region Ice Turret Move Speed Modifier Upgrade
            case TurretName.Ice: //Move Speed on Effect Upgrade
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Damage Upgrade 1 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().speedModifier += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Damage Upgrade 2 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().speedModifier += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Damage Upgrade 3 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().speedModifier += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Ice Ray Turret Simple Damage Upgrade
            case TurretName.IceRay: //Other Target Damage Upgrade
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 1 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 2 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 3 - Ice Ray");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Ice Ray");
                        break;
                }
                break;
            #endregion

            #region Lightining Turret Simple Damage Upgrade
            case TurretName.Lightining: //Simple Damage Upgrade
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 1 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 2 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Other Target Damage Upgrade 3 - Lightning");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Damage Upgrade - Lightning");
                        break;
                }
                break;
            #endregion

            #region Poison Turret Simple Damage Upgrade
            case TurretName.Poison: //Simple Damage Upgrade
                switch (bluePrint.upgradeTwoLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost1)
                        {
                            print("Sem dinheiro para Poison Damage Upgrade 1 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost1;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue1;
                        bluePrint.upgradeTwoLevel = 1;
                        break;

                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost2)
                        {
                            print("Sem dinheiro para Poison Damage Upgrade 2 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost2;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue2;
                        bluePrint.upgradeTwoLevel = 2;
                        break;

                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeTwoCost3)
                        {
                            print("Sem dinheiro para Poison Damage Upgrade 3 - Poison");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeTwoCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeTwoCost3;

                        turret.GetComponent<Turrets>().damageOtherTargets += bluePrint.upgradeTwoValue3;
                        bluePrint.upgradeTwoLevel = 3;
                        break;

                    case 3:
                        print("Level Max Poison Damage Upgrade - Poison");
                        break;
                }
                break;
                #endregion

        }

        turretLevel++;
        VerifyTurretLevel();
    }

    public void UpgradeThree()
    {
        TurretBluePrint bluePrint = actualTurretBluePrint;

        switch (bluePrint.turretName)
        {
            #region Fire Range Upgrade
            case TurretName.Fire:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para Range Upgrade 1 - Fire");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;

                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;
                        bluePrint.upgradeThreeLevel = 1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para Range Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;

                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;
                        bluePrint.upgradeThreeLevel = 2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para Range Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;

                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;
                        bluePrint.upgradeThreeLevel = 3;

                        break;
                    case 3:
                        print("Level Max Range Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Lava Range Upgrade

            case TurretName.Lava:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para Range Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para Range Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para Range Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;


                        break;
                    case 3:
                        print("Level Max Range Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Rock Range Upgrade
            case TurretName.Rock:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para Range Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;

                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para Range Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para Range Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;

                        break;
                    case 3:
                        print("Level Max Range Upgrade - Rock");
                        break;
                }
                break;
            #endregion

            #region Ice Range Upgrade
            case TurretName.Ice:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para  Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para  Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para  Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;


                        break;
                    case 3:
                        print("Level Max  Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Ice Ray Range Upgrade
            case TurretName.IceRay:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para  Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para  Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para  Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;

                        break;
                    case 3:
                        print("Level Max  Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Lightning Range Upgrade
            case TurretName.Lightining:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para  Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;
                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para  Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para  Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;


                        break;
                    case 3:
                        print("Level Max  Upgrade - Fire");
                        break;
                }
                break;
            #endregion

            #region Poison Range Upgrade
            case TurretName.Poison:
                switch (bluePrint.upgradeThreeLevel)
                {
                    case 0:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost1)
                        {
                            print("Sem dinheiro para  Upgrade 1 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost1;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost1;
                        bluePrint.upgradeThreeLevel = 1;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue1;

                        break;
                    case 1:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost2)
                        {
                            print("Sem dinheiro para  Upgrade 2 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost2;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost2;
                        bluePrint.upgradeThreeLevel = 2;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue2;

                        break;
                    case 2:
                        if (PlayerStats.money < bluePrint.upgradeThreeCost3)
                        {
                            print("Sem dinheiro para  Upgrade 3 - ");
                            return;
                        }

                        PlayerStats.money -= bluePrint.upgradeThreeCost3;
                        bluePrint.spentMoneyOnThis += bluePrint.upgradeThreeCost3;
                        bluePrint.upgradeThreeLevel = 3;
                        turret.GetComponent<Turrets>().range += bluePrint.upgradeThreeValue3;

                        break;
                    case 3:
                        print("Level Max  Upgrade - Fire");
                        break;
                }
                break;

                #endregion
        }

        turretLevel++;
        VerifyTurretLevel();
    }

    public void SellTurret()
    {
        TurretBluePrint bluePrint = actualTurretBluePrint;

        int sellValue = actualTurretBluePrint.spentMoneyOnThis * sellPercentage / 100;
        print(sellValue);

        PlayerStats.money += sellPercentage;

        Destroy(turret);
        print(buildManager.canBuild);
        actualTurretBluePrint = null;
        turretLevel = 0;

    }

    public void EvolveTurret()
    {
        TurretBluePrint blueprint = actualTurretBluePrint;

        if (blueprint.turretLevel < 5)
        {
            if (PlayerStats.money < blueprint.evolveCost)
            {
                return;
            }
            if (blueprint.turretUpgradedPrefab == null)
            {
                return;
            }

            PlayerStats.money -= blueprint.evolveCost;
            Destroy(turret);

            GameObject _turret = Instantiate(blueprint.turretUpgradedPrefab, GetBuildPosition(), transform.rotation);
            turret = _turret;
            print("Turret Built");

            GameObject effect = Instantiate(blueprint.evolveEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 4f);

        }
        else // menos que lvl 5 - animação de erro
        {

        }


    }

    public Vector3 GetBuildPosition()
    {
        return this.transform.position + offset;
    }

    void ActualizeNodeVFX()
    {
        TurretBluePrint bluePrint = actualTurretBluePrint;

        DesactivateNodeVFX();

        switch (bluePrint.turretName)
        {
            case TurretName.Fire:
                switch (bluePrint.turretLevel)
                {
                    case 0:
                        fireEffect[0].SetActive(true);
                        break;
                    case 3:
                        fireEffect[1].SetActive(true);
                        break;
                    case 6:
                        fireEffect[2].SetActive(true);
                        break;
                    case 9:
                        fireEffect[3].SetActive(true);
                        break;
                }
                break;
            case TurretName.Ice:
                switch (bluePrint.turretLevel)
                {
                    case 0:
                        iceEffect[0].SetActive(true);
                        break;
                    case 3:
                        iceEffect[1].SetActive(true);
                        break;
                    case 6:
                        iceEffect[2].SetActive(true);
                        break;
                    case 9:
                        iceEffect[3].SetActive(true);
                        break;
                }
                break;
            case TurretName.IceRay:
                switch (bluePrint.turretLevel)
                {
                    case 0:
                        iceRayEffect[0].SetActive(true);
                        break;
                    case 3:
                        iceRayEffect[1].SetActive(true);
                        break;
                    case 6:
                        iceRayEffect[2].SetActive(true);
                        break;
                }
                break;
            case TurretName.Lightining:
                switch (bluePrint.turretLevel)
                {
                    case 0:
                        lightiningEffect[0].SetActive(true);
                        break;
                    case 3:
                        lightiningEffect[1].SetActive(true);
                        break;
                    case 6:
                        lightiningEffect[2].SetActive(true);
                        break;
                    case 9:
                        lightiningEffect[3].SetActive(true);
                        break;
                }
                break;
            case TurretName.Poison:
                switch (bluePrint.turretLevel)
                {
                    case 0:

                        break;
                    case 3:

                        break;
                    case 6:

                        break;
                }
                break;
            default:
                print("Node não tem efeito");
                break;
        }
    }

    void DesactivateNodeVFX()
    {
        fireEffect[0].SetActive(false);
        fireEffect[1].SetActive(false);
        fireEffect[2].SetActive(false);
        fireEffect[3].SetActive(false);
        iceEffect[0].SetActive(false);
        iceEffect[1].SetActive(false);
        iceEffect[2].SetActive(false);
        iceEffect[3].SetActive(false);
        iceRayEffect[0].SetActive(false);
        iceRayEffect[1].SetActive(false);
        iceRayEffect[2].SetActive(false);
        lightiningEffect[0].SetActive(false);
        lightiningEffect[1].SetActive(false);
        lightiningEffect[2].SetActive(false);
        lightiningEffect[3].SetActive(false);
        poisonEffect[0].SetActive(false);
        poisonEffect[1].SetActive(false);
        poisonEffect[2].SetActive(false);
    }

    void VerifyTurretLevel()
    {
        if(turretLevel == 0)
        {
            ActualizeNodeVFX();
        }
        else if (turretLevel == 3)
        {
            ActualizeNodeVFX();
        }
        else if (turretLevel == 6)
        {
            ActualizeNodeVFX();
        }
        else if (turretLevel == 9)
        {
            ActualizeNodeVFX();
        }
    }
}
