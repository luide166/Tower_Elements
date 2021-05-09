using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Enemy_test : MonoBehaviour
{
    public enum EnemyImunity
    {
        None,
        Rock,
        Lava,
        Fire,
        Ice,
        IceRay,
        Lightining,
        Poison,
    }

    private bool fireImunity;
    private bool lavaImunity;
    private bool rockImunity;
    private bool iceImunity;
    private bool iceRayImunity;
    private bool LightningImunity;
    private bool poisonImunity;


    /*------------------------------------------Player Stats------------------------------------------*/
    [HideInInspector] public float speed;
    [HideInInspector] public float health;

    [Header("Imunity")]

    [InspectorName("Type")] public EnemyImunity imunityType;
    [Space(5)]
    [Header("Stats")]
    [Space(5)]
    public float normalHealth = 100;
    public float normalSpeed = 10f;
    public int value = 50;
    public bool isDead = false;

    [Space(10)]
    [HideInInspector] public GameObject target1 = null;
    [HideInInspector] public GameObject target2 = null;

    [Header("1) - Effected By:")]
    [Space(5)]

    [Header("---------Unity Stuff-----------")]
    [Space(10)]
    public bool iceEffected = false;
    public bool iceRayEffected = false;
    public bool lightningEffected = false;
    public bool fireEfected = false;
    public bool lavaEffected = false;
    public bool rockEffected = false;
    public bool poisonEffected = false;

    [Header("2) - UI ")]
    [Space(10)]
    public Image characterIcon;
    [Space(5)]
    public Image healthBar;
    [Space(5)]
    public Image fireEffectIcon;
    public Image LavaEffectIcon;
    public Image rockEffectIcon;
    public Image iceEffectIcon;
    public Image iceRayEffectIcon;
    public Image lightiningEffectIcon;
    public Image poisonEffectIcon;



    private void Start()
    {
        speed = normalSpeed;
        health = normalHealth;
        VerifyImunity();

        ResetEffectIcons();
    }

    void TakeDamage(float _amount)
    {
        if (isDead == false)
        {
            health -= _amount;

            healthBar.fillAmount = health / normalHealth;

            if (health <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        isDead = true;
        speed = 0;
        PlayerStats.money += value;
        Spawner.enemiesAlive--;

        Destroy(gameObject);
    }
    void VerifyImunity()
    {
        switch (imunityType)
        {
            case EnemyImunity.Fire:
                DeselectImunity();
                fireImunity = true;

                break;
            case EnemyImunity.Lava:
                DeselectImunity();
                lavaImunity = true;

                break;
            case EnemyImunity.Rock:
                DeselectImunity();
                rockImunity = true;

                break;
            case EnemyImunity.Ice:
                DeselectImunity();
                iceImunity = true;

                break;
            case EnemyImunity.IceRay:
                DeselectImunity();
                iceRayImunity = true;

                break;
            case EnemyImunity.Lightining:
                DeselectImunity();
                LightningImunity = true;

                break;
            case EnemyImunity.Poison:
                DeselectImunity();
                poisonImunity = true;

                break;
        }
    }
    void DeselectImunity()
    {
        fireImunity = false;
        lavaImunity = false;
        rockImunity = false;
        iceImunity = false;
        iceRayImunity = false;
        LightningImunity = false;
        poisonImunity = false;
    }

    public void FireEffect(float _speed, float _damageOverTime, float _timeOfEffect)
    {
        //Graphics
        fireEffectIcon.enabled = true;

        //Mechanics
        if (fireEfected == false)
        {
            fireEfected = true;
            speed *= (1f + _speed);
        }

        TakeDamage(_damageOverTime * Time.deltaTime);
        Invoke("ResetFireEffect", _timeOfEffect);

    }
    public void LavaEffect(float _range, float _damageOnTarget, float _timeOfEffect, float _damageRangedTargets, float _slowPower)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.OrderBy(enemy => Vector3.Distance(this.transform.position, enemy.transform.position)).ToArray();

        foreach (GameObject Enemy in enemies)
        {
            if (Vector3.Distance(this.transform.position, Enemy.transform.position) <= _range)
            {
                float distance = Vector3.Distance(this.transform.position, Enemy.transform.position);
                if (distance != 0)
                {
                    Enemy.GetComponent<Enemy_test>().TakeDamage(_damageRangedTargets);
                    print("Tomei de revesgueio " + _damageRangedTargets);
                }
                else
                {
                    // so um inimigo
                }
            }
        }

        this.rockEffected = true;
        LavaEffectIcon.enabled = true;
        this.TakeDamage(_damageOnTarget);
        this.speed *= (1f + _slowPower);
        Invoke("ResetLavaEffect", _timeOfEffect);

    }
    public void RockEffect(float _range, float _damageOnTarget, float _timeOfEffect, float _damageRangedTargets, float _slowPower)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.OrderBy(enemy => Vector3.Distance(this.transform.position, enemy.transform.position)).ToArray();

        foreach (GameObject Enemy in enemies)
        {
            if (Vector3.Distance(this.transform.position, Enemy.transform.position) <= _range)
            {
                float distance = Vector3.Distance(this.transform.position, Enemy.transform.position);
                if (distance != 0)
                {
                    Enemy.GetComponent<Enemy_test>().TakeDamage(_damageRangedTargets);
                    print("Tomei de revesgueio " + _damageRangedTargets);
                }
                else
                {
                    // so um inimigo
                }
            }
        }

        this.rockEffected = true;
        rockEffectIcon.enabled = true;
        this.TakeDamage(_damageOnTarget);
        this.speed *= (1f - _slowPower);
        Invoke("ResetRockEffect", _timeOfEffect);

    }
    public void IceEffect(float _slowAmount, float _slowTime, float damageOverTime)
    {
        //Graphics

        //Mechanics
        iceEffected = true;
        iceEffectIcon.enabled = true;
        speed *= (1f - _slowAmount);
        Invoke("ResetIceEffect", _slowTime);
    }
    public void IceRayEffect(float _range, float _enemyDamageOverTime, LineRenderer _renderer, float _slowAmount, float _slowTime, float _otherEnemyDamage, float _singleTargetDamageMultiplier)
    {
        //Graphics

        //Mechanics
        this.iceRayEffected = true;
        iceRayEffectIcon.enabled = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.OrderBy(enemy => Vector3.Distance(this.transform.position, enemy.transform.position)).ToArray();
        float shortestDistance = Mathf.Infinity;

        //seleciona os dois targets
        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distanceToEnemy < _range)
            {
                if (!target1)
                {
                    if (enemies[i] != this.gameObject)
                    {
                        target1 = enemies[i];
                        shortestDistance = Mathf.Infinity;
                        print("nearest enemy OK");
                    }
                }
                else if (!target2)
                {
                    if (enemies[i] != this.gameObject)
                    {
                        if (enemies[i] != target1)
                        {
                            target2 = enemies[i];
                            shortestDistance = distanceToEnemy;
                            print("next enemy OK");
                        }
                    }
                }
            }
        }


        if (target1 && !target2)
        {
            //Graphics
            _renderer.positionCount = 3;
            _renderer.SetPosition(2, target1.transform.position);

            //Mechanics
            this.TakeDamage(_enemyDamageOverTime * Time.deltaTime);
            this.speed *= (1f - _slowAmount);
            

            target1.GetComponent<Enemy_test>().TakeDamage(_otherEnemyDamage * Time.deltaTime);
            target1.GetComponent<Enemy_test>().speed *= (1f - _slowAmount);
            target1.GetComponent<Enemy_test>().iceRayEffected = true;
            target1.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = true;

        }
        else if (!target1 && target2)
        {
            //Graphics
            _renderer.positionCount = 3;
            _renderer.SetPosition(2, target2.transform.position);

            //Mechanics
            this.TakeDamage(_enemyDamageOverTime * Time.deltaTime);
            this.speed *= (1f - _slowAmount);

            target2.GetComponent<Enemy_test>().TakeDamage(_otherEnemyDamage * Time.deltaTime);
            target2.GetComponent<Enemy_test>().speed *= (1f - _slowAmount);
            target2.GetComponent<Enemy_test>().iceRayEffected = true;
            target2.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = true;
        }
        else if (target1 && target2)
        {
            //Graphics
            _renderer.positionCount = 6;
            _renderer.SetPosition(2, target1.transform.position);
            _renderer.SetPosition(3, this.transform.position);

            _renderer.SetPosition(4, target2.transform.position);
            _renderer.SetPosition(5, this.transform.position);

            //Mechanics
            this.TakeDamage(_enemyDamageOverTime * Time.deltaTime);
            this.speed *= (1f - _slowAmount);

            target1.GetComponent<Enemy_test>().TakeDamage(_otherEnemyDamage * Time.deltaTime);
            target1.GetComponent<Enemy_test>().speed *= (1f - _slowAmount);
            target1.GetComponent<Enemy_test>().iceRayEffected = true;
            target1.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = true;

            target2.GetComponent<Enemy_test>().TakeDamage(_otherEnemyDamage * Time.deltaTime);
            target2.GetComponent<Enemy_test>().speed *= (1f - _slowAmount);
            target2.GetComponent<Enemy_test>().iceRayEffected = true;
            target2.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = true;
        }
        else
        {
            //Graphics
            _renderer.positionCount = 2;

            //Mechanics
            this.TakeDamage((_enemyDamageOverTime)* _singleTargetDamageMultiplier * Time.deltaTime);
            this.speed *= (1f - _slowAmount);

        }

        Invoke("ResetIceRayEffect", _slowTime);

    }
    public void ElectricEffect(float _range, float _enemyDamageOverTime, LineRenderer _renderer)
    {
        //Graphics

        //Mechanics
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.OrderBy(enemy => Vector3.Distance(this.transform.position, enemy.transform.position)).ToArray();
        float shortestDistance = Mathf.Infinity;

        //seleciona os dois targets
        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distanceToEnemy < _range)
            {
                if (!target1)
                {
                    if (enemies[i] != this.gameObject)
                    {
                        target1 = enemies[i];
                        shortestDistance = Mathf.Infinity;
                    }
                }
                else if (!target2)
                {
                    if (enemies[i] != this.gameObject)
                    {
                        if (enemies[i] != target1)
                        {
                            target2 = enemies[i];
                            shortestDistance = distanceToEnemy;
                        }
                    }
                }
            }
        }


        if (target1 && !target2)
        {
            //Graphics
            _renderer.positionCount = 3;
            _renderer.SetPosition(2, target1.transform.position);

            //Mechanics
            target1.GetComponent<Enemy_test>().TakeDamage(_enemyDamageOverTime * Time.deltaTime);

        }
        else if (!target1 && target2)
        {
            //Graphics
            _renderer.positionCount = 3;
            _renderer.SetPosition(2, target2.transform.position);

            //Mechanics
            target2.GetComponent<Enemy_test>().TakeDamage(_enemyDamageOverTime * Time.deltaTime);
        }
        else if (target1 && target2)
        {
            //Graphics
            _renderer.positionCount = 6;
            _renderer.SetPosition(2, target1.transform.position);
            _renderer.SetPosition(3, this.transform.position);
            _renderer.SetPosition(4, target2.transform.position);
            _renderer.SetPosition(5, this.transform.position);

            //Mechanics
            target1.GetComponent<Enemy_test>().TakeDamage(_enemyDamageOverTime * Time.deltaTime);
            target2.GetComponent<Enemy_test>().TakeDamage(_enemyDamageOverTime * Time.deltaTime);
        }
        else
        {
            //Graphics
            _renderer.positionCount = 2;

            //Mechanics
            this.TakeDamage((_enemyDamageOverTime * .25f) * Time.deltaTime);
        }

    }
    public void PoisonEffect(float _enemyDamageOverTime, float _poisonDamage, float _timeToNextPoisonDamage)
    {
        //Graphics

        //Mechanics
        if(poisonEffected == false)
        {
            StartCoroutine(TickPoison(_poisonDamage, _timeToNextPoisonDamage));
            poisonEffected = true;
            return;
        }

        TakeDamage(_enemyDamageOverTime * Time.deltaTime);

    }


    IEnumerator TickPoison(float _poisonDamage, float _timeToNextPoisonDamage)
    {
        print(_poisonDamage + " damage, next tick in: " + _timeToNextPoisonDamage);

        yield return new WaitForSeconds(_timeToNextPoisonDamage);
        StartCoroutine(TickPoison(_poisonDamage, _timeToNextPoisonDamage));
        
        
    }
    void ResetFireEffect()
    {
        speed = normalSpeed;
        fireEfected = false;
        fireEffectIcon.enabled = false;

        print("Fire Effect Ends");
    }
    void ResetLavaEffect()
    {
        speed = normalSpeed;
        lavaEffected = false;
        LavaEffectIcon.enabled = false;
    }
    void ResetRockEffect()
    {
        speed = normalSpeed;
        rockEffected = false;
        rockEffectIcon.enabled = false;
        print("End Rock Effect");
    }
    void ResetIceEffect()
    {
        speed = normalSpeed;
        iceEffected = false;
        iceEffectIcon.enabled = false;

        print("Slow Ends");
    }
    void ResetIceRayEffect()
    {
        this.speed = normalSpeed;
        this.iceRayEffected = false;
        iceRayEffectIcon.enabled = false;
    
        target1.GetComponent<Enemy_test>().speed = target1.GetComponent<Enemy_test>().normalSpeed;
        target1.GetComponent<Enemy_test>().iceRayEffected = false;
        target1.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = false;

        target2.GetComponent<Enemy_test>().speed = target1.GetComponent<Enemy_test>().normalSpeed;
        target2.GetComponent<Enemy_test>().iceRayEffected = false;
        target2.GetComponent<Enemy_test>().iceRayEffectIcon.enabled = false;

        print("Ice Lighting Ends");
    }
    void ResetEffectIcons()
    {
        fireEffectIcon.enabled = false;
        LavaEffectIcon.enabled = false;
        rockEffectIcon.enabled = false;
        iceEffectIcon.enabled = false;
        iceRayEffectIcon.enabled = false;
        lightiningEffectIcon.enabled = false;
        poisonEffectIcon.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}
