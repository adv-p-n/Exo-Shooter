using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] float speed=15f;
    [SerializeField] float xRange=7f;
    [SerializeField] float yRange=5f;
    [Header("Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 3f;
    [Header("Player Control Based Tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float rotationFactor = 1f;

    [SerializeField][HideInInspector] bool isFiring = false;
    [Header("Laser Array")]
    [Tooltip("Add all laser GameObjects here")]
    [SerializeField] GameObject[] lasers;
    Vector2 moveSpeed;
    Vector3 palyerPosition;
    

    void Start()
    {
        
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessFiring()
    {
        if (isFiring) 
        { 
            SetActiveLasers(true); 
        }
        else 
        {
            SetActiveLasers(false);
        }
    }

    void ProcessRotation()
    {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float positionYaw = transform.localPosition.x * positionYawFactor;
        float controlPitch = moveSpeed.y * controlPitchFactor;
        float controlRoll = moveSpeed.x * controlRollFactor;
        float pitch = positionPitch + controlPitch;
        float yaw=positionYaw;
        float roll = controlRoll;
        Quaternion finalRotation= Quaternion.Euler(pitch,yaw,roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, finalRotation, rotationFactor);
    }

    void ProcessTranslation()
    {
        palyerPosition = transform.localPosition;
        float xPos = palyerPosition.x + ((moveSpeed.x * speed) * Time.deltaTime);
        float yPos = palyerPosition.y + ((moveSpeed.y * speed) * Time.deltaTime);
        float xClamped = Mathf.Clamp(xPos,-xRange,xRange);
        float yClamped = Mathf.Clamp(yPos,-yRange,yRange) ;
        transform.localPosition = new Vector3(xClamped, yClamped, 0);
    }

    void OnMove(InputValue value)
    {
        moveSpeed = value.Get<Vector2>();
    }
    void OnFire(InputValue value)
    {
        if (value.isPressed) { isFiring = true; }
        else { isFiring = false; }
       
    }
    void SetActiveLasers(bool val)
    {
        foreach(GameObject laser in lasers) 
        {
            var particleSystem = laser.GetComponent<ParticleSystem>().emission;
            particleSystem.enabled= val;
        }
    }
}
