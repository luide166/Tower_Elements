using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint fireTurret;
    public TurretBluePrint rockTurret;
    public TurretBluePrint lavaTurret;
    public TurretBluePrint lightningTurret;
    public TurretBluePrint iceTurret;
    public TurretBluePrint iceRayTurret;
    public TurretBluePrint poisonTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    #region select Turrets
    public void SelectFireTurret()
    {
        buildManager.SelectTurretToBuild(fireTurret);
    }
    public void SelectRockTurret()
    {
        buildManager.SelectTurretToBuild(rockTurret);
    }
    public void SelectElectricTurret()
    {
        buildManager.SelectTurretToBuild(lightningTurret);
    }
    public void SelectIceTurret()
    {
        buildManager.SelectTurretToBuild(iceTurret);
    }
    public void SelectPoisonTurret()
    {
        buildManager.SelectTurretToBuild(poisonTurret);
    }
    #endregion
}
