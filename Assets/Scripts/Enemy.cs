
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] int health = 500;
    [SerializeField] int scoreValue = 50;
    [SerializeField] int percentageOfLuck = 10;
    [SerializeField] GameObject healthObject;
    

    [Header("Projectile")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeToShot = .2f;
    [SerializeField] float maxTimeToShot = 3f;
    [SerializeField] float projectileSpeed = 7f;

    [Header("FX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = .7f;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] [Range(0, 1)] float projectileSoundVolume = .4f;
    [SerializeField] float durationOfExplosion = 1f;


    // Use this for initialization
    void Start ()
    {
        ResetShotCounter();
    }


    // Update is called once per frame
    void Update ()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        GameObject cloneLaser = Instantiate(
            enemyLaser, transform.position, Quaternion.identity);

        Vector2 projectileVelocity = new Vector2(
            0f, projectileSpeed);

        cloneLaser.GetComponent<Rigidbody2D>().velocity = projectileVelocity;

        //Shoot SFX
        AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, projectileSoundVolume);
        
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeToShot, maxTimeToShot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        //Health
        if(Random.Range(0,100) < percentageOfLuck)
        {
            GameObject health = Instantiate(healthObject, transform.position, Quaternion.identity);
            health.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -4f);
        }
        //VFX
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        //SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
