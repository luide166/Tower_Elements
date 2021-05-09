using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletName
    {
        Rock,
        Lava,
    }

    public BulletName bulletName;

    public float speed = 10f;
    public int damage = 50;
    public int damageOtherTarget = 10;
    public float speedModifier = .2f;
    public float radius = 0f;
    public float timeOfEffect = 1;

    private Transform target;
    public GameManager impactEffect;

    private void Start()
    {
        
    }

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        //PlayEffect();


        switch (bulletName)
        {
            case BulletName.Rock:
                target.GetComponent<Enemy_test>().RockEffect(radius, damage, timeOfEffect, damageOtherTarget, speedModifier);
                break;
            case BulletName.Lava:
                target.GetComponent<Enemy_test>().RockEffect(radius, damage, timeOfEffect, damageOtherTarget, speedModifier);
                break;
        }


        Destroy(gameObject);
        return;
    }

    void PlayEffect()
    {
        //GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectInst, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
