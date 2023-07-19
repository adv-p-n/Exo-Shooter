using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delayToDestroy=3f;
    void Start()
    {
        Destroy(gameObject, delayToDestroy);
    }


}
