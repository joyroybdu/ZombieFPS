using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 25;

    void OnCollisionEnter(Collision objectWeHit)
    {
        // Check if the bullet collides with an object tagged as "Enemy"
        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy: " + objectWeHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectWeHit);

            // Optionally destroy the enemy here if needed
            // Destroy(objectWeHit.gameObject);
CreateBloodSprayEffect(objectWeHit);
            // Destroy the bullet
            Destroy(gameObject);
        }
        else if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit Wall");
            CreateBulletImpactEffect(objectWeHit);

            // Destroy the bullet
            Destroy(gameObject);
        }
        else if (objectWeHit.gameObject.CompareTag("Zombie"))
        {
            Zombie zombie = objectWeHit.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(bulletDamage); // Deal damage safely
            }

            // Destroy the bullet after hitting the zombie
            Destroy(gameObject);
        }
    }
void CreateBloodSprayEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject bloodSprayPrefab = Instantiate(
            GlobalReferences.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
    }
    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
