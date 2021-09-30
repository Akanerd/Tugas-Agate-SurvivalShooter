using UnityEngine;

public class EnemyMovement : MonoBehaviour{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    
    private void Awake ()
    {
        //cari game
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        
        //Mendapatkan Reference component
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }
    
    void Update ()
    {
        //Memindahkan posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else// Hentikan moving
        {
            nav.enabled = false;
        }
    }
}
