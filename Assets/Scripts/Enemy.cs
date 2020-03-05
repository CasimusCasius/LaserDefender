using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health config")]
    [SerializeField] float health = 100f;

    [Header("Shooting config")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectalSpeed = 10f;

    [Header("VFX")]
    [SerializeField] float durationOfExplosion = 1f;  
    [SerializeField] GameObject deathVFX;

    [Header("SFX")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)]float deathSoundVolume;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        
        ProcessHit(damageDealer);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter<=0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser=Instantiate(enemyLaser, transform.position , Quaternion.identity)as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectalSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position,laserSoundVolume);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, durationOfExplosion);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position, deathSoundVolume);
    }

}
