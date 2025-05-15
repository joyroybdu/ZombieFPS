using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; set; }
    public AudioSource shootingSound; // Prefab for bullet impact effect
    public AudioClip zombieWalkingSound; // Prefab for blood spray effect
    public AudioClip  zombieAttackSound; // Prefab for blood spray effect
    public AudioClip  zombieHitSound; // Prefab for blood spray effect
    public AudioClip  zombieDeathSound; // Prefab for blood spray effect
    public AudioClip  zombieChaseSound; // Prefab for blood spray effect
    public AudioClip  zombieRoarSound; // Prefab for blood spray effect
    public AudioSource zombieChannel;
  
 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If an instance already exists and it's not this one, destroy this game object
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}