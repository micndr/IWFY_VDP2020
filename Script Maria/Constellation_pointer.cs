﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation_pointer : MonoBehaviour
{

    public Animator Anime;

    // Start is called before the first frame update
    void Start()
    {
        Anime = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Anime.Play("Constellation_animation");
    }
}
