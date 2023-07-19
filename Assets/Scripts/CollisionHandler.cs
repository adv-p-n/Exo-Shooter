using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay=1f;
    [SerializeField] ParticleSystem explosionVfx;
    PlayerMovement playerMovement;
    MeshRenderer myMeshRenderer;
    BoxCollider myBoxCollider;


    void Start()
    {
        playerMovement= GetComponent<PlayerMovement>();
        myMeshRenderer= GetComponent<MeshRenderer>();
        myBoxCollider= GetComponent<BoxCollider>();
        
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name +"--Collided with--"+other.gameObject.name);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} ** was Triggered by{other.gameObject.name}");
        if (this.tag == "Player")
        {
            StartCrashSequence();

        }
    }

   void StartCrashSequence()
    {
        explosionVfx.Play();
        myBoxCollider.enabled = false;
        myMeshRenderer.enabled = false;
        playerMovement.enabled = false;
        Invoke("ReloadLevel", reloadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
