using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy_test))]
public class EnemyMovement : MonoBehaviour
{
    private int wavepointInvex = 0;
    private Transform target;

    private Enemy_test enemy;

    [Header("Presets")]
    public GameObject model;
    [SerializeField]
    Animator anim;
    public float turnSpeedSmoothness;

    private void Start()
    {
        target = Waypoints.points[0];
        enemy = GetComponent<Enemy_test>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        #region Movimentação
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        LookAtWayPoint();

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWayPoint();
        }


        if (!enemy.isDead)
        {
            anim.SetBool("isDead", false);
        }
        else
        {
            anim.SetBool("isDead", true);
        }

        #endregion

        

    }
    void GetNextWayPoint()
    {
        if (wavepointInvex >= Waypoints.points.Length - 1)
        {
            //chegou no final   
            EndPath();
            return;
        }

        wavepointInvex++;
        target = Waypoints.points[wavepointInvex];
    }

    void EndPath()
    {
        PlayerStats.lives--;
        Spawner.enemiesAlive--;
        Destroy(gameObject);
    }

    void LookAtWayPoint()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(model.transform.rotation, lookRotation, turnSpeedSmoothness * Time.deltaTime).eulerAngles;
        model.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
