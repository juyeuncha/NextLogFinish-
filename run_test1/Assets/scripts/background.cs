using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    public float bgspeed = 0.34f;
    public Renderer rend;
   

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update ()
    {
        float bgoffset = Time.time * bgspeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(bgoffset, 0));
	}
}
