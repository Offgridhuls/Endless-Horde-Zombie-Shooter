using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealthComponent : HealthComponent
{
    public GameObject explosionParticles;
    public float explosionRadius;
    // Start is called before the first frame update
    public override void destroy()
    {
        //base.destroy();
       // explodeRoutine();
    }

    void Update()
    {
        if(CurrentHealth < 0)
        {
            Destroy(gameObject);
            explodeRoutine();
        }
    }
    void explodeRoutine()
    {
        Destroy(gameObject);
        var explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Collider[] zombiesInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyZombie in zombiesInRange)
        {
            Debug.Log(nearbyZombie.gameObject.name);
            IDamagable zombieHealth = nearbyZombie.gameObject.GetComponent<IDamagable>();
            zombieHealth?.TakeDamage(99);
           
        }
       
    }
}
