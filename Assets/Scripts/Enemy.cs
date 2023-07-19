using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int score=100;
    [SerializeField] int maxHealth = 1;

    ScoreKeeper scoreKeeper;
    GameObject parentGameObject;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessScore();
        ProcessHealth();
    }

    void ProcessHealth()
    {
        maxHealth -= 1;
        GameObject hvfx = Instantiate(hitVFX,transform.position,Quaternion.identity);
        hvfx.transform.parent= parentGameObject.transform;
        if (maxHealth <= 0) 
        { 
            ProcessDeath();
        }
    }

    void ProcessScore()
    {
        scoreKeeper.IncreaseScore(score);
    }
    void ProcessDeath()
    {
        GameObject dfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        dfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }


}
