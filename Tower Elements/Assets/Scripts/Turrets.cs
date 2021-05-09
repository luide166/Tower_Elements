using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretName
{
    Rock,
    Lava,
    Fire,
    Ice,
    IceRay,
    Lightining,
    Poison,
}

public class Turrets : MonoBehaviour
{
    [Header("Turret Model")]
    public TurretName turretName;
    private Transform target;
    private Enemy_test targetEnemy;


    [Header("Unity Setup")]
    private string enemytag = "Enemy";
    public Transform partToRotate;
    public float turnSpeedSmoothness;
    public GameObject firePoint;
    public float range = 5f;
    [Space(5)]
    public bool useLaser = false;

    //adicionar variaveis para controle dos efeitos de ataque nos inimigos

    [Header("With Laser")]
    [Space(15)]
    public LineRenderer lineRenderer;
    public float damageOverTime = 30;
    public float speedModifier; // max value is 1
    public float effectTimer;
    public float damageOtherTargets;
    public float singleTargerDamageMultiplier;
    public float rangeOfEffect;

    [Header("With Bullets")]
    [Space(15)]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        lineRenderer = GetComponent<LineRenderer>();
        targetEnemy = GetComponent<Enemy_test>();

    }


    void Update()
    {
        if(target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }

            return;
        }

        LookOnTarget();

        //Check the type of the Turret
        if (useLaser)
        {
            Laser();
        }
        else 
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
                print(fireCountdown);
            }

            fireCountdown -= Time.deltaTime;
        } 
    }

    //Rotate the correct Part of the turret
    public void LookOnTarget()
    {
        UpdateTarget();

        if (partToRotate == null)
        {
            return;
        }
        if (!target)
        {
            print("Sem target");
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeedSmoothness * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
  
    //Look for the Nearest Enemy
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy_test>();
        }
        else
        {
            target = null;
        }
    }

    //If the turret use bullets
    void Shoot()
    {
        if (!useLaser)
        {
            print("Atirei uma Bullet");
            GameObject _bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Bullet bullet = _bullet.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }
    }

    //If turret doesn't use bullets
    void Laser()
    {
        //Graphics
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        if (!target)
        {
            return;
        }

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, target.position);

        //Mechanics
        switch (turretName)
        {
            case TurretName.Fire:

                if (targetEnemy.imunityType != Enemy_test.EnemyImunity.Fire)
                {
                    targetEnemy.FireEffect(speedModifier, damageOverTime, effectTimer);
                }
                break;

            case TurretName.IceRay:

                if (targetEnemy.imunityType != Enemy_test.EnemyImunity.IceRay)
                {
                    targetEnemy.IceRayEffect(rangeOfEffect, damageOverTime, lineRenderer, speedModifier, effectTimer, damageOtherTargets,singleTargerDamageMultiplier);
                }

                break;

            case TurretName.Ice:

                if (targetEnemy.imunityType != Enemy_test.EnemyImunity.Ice)
                {
                    targetEnemy.IceEffect(speedModifier, effectTimer, damageOverTime);
                }

                break;

            case TurretName.Lightining:

                if (targetEnemy.imunityType != Enemy_test.EnemyImunity.Lightining)
                {
                    targetEnemy.ElectricEffect(rangeOfEffect, damageOverTime, lineRenderer);
                }

                break;

            case TurretName.Poison:

                if (targetEnemy.imunityType != Enemy_test.EnemyImunity.Poison)
                {
                    targetEnemy.PoisonEffect(damageOverTime, damageOtherTargets, effectTimer);
                }

                break;
        }
    }


    //draw Turret Range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
