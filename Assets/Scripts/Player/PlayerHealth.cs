using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    
    Animator anim;
    AudioSource playerAudio;
    [SerializeField] private PlayerMovement playerMovement; 
    PlayerShooting playerShooting;

    bool isDead;                                                
    bool damaged;                                               

    void Awake()
    {
        //Mendapatkan refernce komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }


    void Update()
    {
        //Jika terkena damaage
        if (damaged) 
        { 
            damageImage.color = flashColour; 
        }
        else 
        { 
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); 
        }
        //Set damage to false
        damaged = false;
    }

    // fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;
        //mengurangi health
        currentHealth -= amount;
        //mengurangi health
        healthSlider.value = currentHealth;
        //Memainkan suara ketika terkena damage
        playerAudio.Play();
        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        //playerShooting.DisableEffects ();
        playerShooting.DisableEffects();
        //mentrigger animasi Die
        anim.SetTrigger("Die");
        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            PowerUp power = collision.gameObject.GetComponent<PowerUp>();
            SetEffect(power.powerType, power.amount);
            Destroy(collision.gameObject);
        }
    }

    void SetEffect(PowerUpType type, float amount)
    {
        if(type == PowerUpType.healthUp)
        {
            int health = Mathf.RoundToInt(amount);

            currentHealth += health;
            healthSlider.value = currentHealth;
        }
        else if(type == PowerUpType.speedUp)
        {
            playerMovement.speed += amount;
        }
    }
}
