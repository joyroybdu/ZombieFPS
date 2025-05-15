using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom : MonoBehaviour
{
    public zombieHand zombieHand;
    public int zombieDamage;

    private void Start()
    {
        zombieHand.damage = zombieDamage;
    }
}
