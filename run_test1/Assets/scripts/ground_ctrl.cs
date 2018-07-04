using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_ctrl : MonoBehaviour {

    public float Speed = 9.5f; 
    public GameObject[] Ground; //계속 만들 그라운드
    public GameObject A_zone; //가운데에 있는 그라운드
    public GameObject B_zone; //화면의 오른쪽에 있는 그라운드 

    void Update()
    {
        Move();    
    }

    void Move()
    {
        A_zone.transform.Translate(Vector3.left * Speed * Time.deltaTime);
        B_zone.transform.Translate(Vector3.left * Speed * Time.deltaTime);

        if (B_zone.transform.position.x <= 0)
        {
            Destroy(A_zone);

            A_zone = B_zone;
            Make();

        }
        

    }

    void Make()
    {
        int GRtype = Random.Range(0, Ground.Length);
        B_zone = Instantiate(Ground[GRtype], new Vector3(110, -24, 2), transform.rotation)
                 as GameObject;
    }
}
