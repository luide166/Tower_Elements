using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    //spublic GameObject buildEffect;

    private TurretBluePrint turretToBuild;
    private EvolveBluePrint evolveInfos;
    private Node selectedNode;

    public GameObject uiManager;


    private void Awake()
    {
        instance = this;


        if (instance != null)
        {
            print("Build Manager OK");
        }
    }

    public void SelectNode(Node _node)
    {
        //Deseleciona o Node
        if (selectedNode == _node)
        {
            DeselectNode();
            return;
        }

        //seleciona o Node
        selectedNode = _node;
        turretToBuild = null;

        //abrir a tela do node
        uiManager.GetComponent<UI_Manager>().ActivateUpgradeUI(_node);

    }

    public void DeselectNode()
    {
        selectedNode = null;

        //fechar a tela do node
        uiManager.GetComponent<UI_Manager>().ActivateTurretsUI();
    }

    public void SelectTurretToBuild(TurretBluePrint _turret)
    {
        turretToBuild = _turret;
        selectedNode = null;
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.buildCost; } }

}
