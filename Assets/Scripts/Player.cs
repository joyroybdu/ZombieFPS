using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodScreen;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            // Handle player death (e.g., restart game, show game over screen, etc.)
            Debug.Log("Player is dead!");
                PlayerDie(); 
        }
        else
        {
            // Handle player damage (e.g., play damage animation, update UI, etc.)
            Debug.Log("Player took damage! Remaining HP: " + HP);
            // StartCoroutine(ShowBloodScreen());

        }
    }

// private IEnumerator ShowBloodScreen()
//     {
//         if(bloodScreen.activeInHierarchy == false){
//             bloodScreen.SetActive(true);
            
//         }
//         yield return new WaitForSeconds(5f);
//         if(bloodScreen.activeInHierarchy == true){
//             bloodScreen.SetActive(false);
//         }
//     }
        
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            TakeDamage(other.gameObject.GetComponent<zombieHand>().damage); // Example damage amount
        }
    }
    private void PlayerDie()
    {
        // GetComponent<MouseMovement>().enabled = false;
 
        // GetComponent<PlayerMovement>().enabled = false;
        
        }
}
