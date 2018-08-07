﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    
    public GlobVar globvar;
    public Renderer rend;
   

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void MoveBg()
    {
        float bgoffset = Time.time * globvar.bgspeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(bgoffset, 0));
	}
}
