using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) Shoot();
        if (timer >= timeBetweenBullets * effectsDisplayTime) DisableEffects();
    }
    
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    
    public void Shoot()
    {
        timer = 0f;
        
        gunAudio.Play();
        gunLight.enabled = true;
        
        gunParticles.Stop();
        gunParticles.Play();
        
        gunLine.enabled = true;
        gunLine.positionCount = 2;
        gunLine.SetPosition(0, Vector3.zero); //transform.position;


        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null) enemyHealth.TakeDamage(damagePerShot, shootHit.point);

            gunLine.SetPosition(1, Vector3.forward * Vector3.Distance(shootHit.point, transform.position)); //gunLine.SetPosition(1, shootHit.point);
        }
        else
            gunLine.SetPosition(1, Vector3.forward * range);
    }
}