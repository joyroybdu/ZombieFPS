using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance { get; set; }
    public GameObject bulletImpactEffectPrefab; // Prefab for bullet impact effect
    public GameObject bloodSprayEffect; // Prefab for blood spray effect

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
