﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botParticleCl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Tornado new");
    }
    private void OnParticleTrigger()
    {
        Debug.Log("Tornado new");
    }
}
