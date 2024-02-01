using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFire : MonoBehaviour
{

    public Transform muiSung;
    public GameObject fireBall;
    public float fireRate = 1f;
    public float nextFire = 0;

    public static CharMove CharMove;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            BanDan();
        }
        
    }

    void BanDan()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
           if(CharMove.facingRight)
            {
                Instantiate(fireBall, muiSung.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            } else if (!CharMove.facingRight)
            {
                Instantiate(fireBall, muiSung.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
    }
}
