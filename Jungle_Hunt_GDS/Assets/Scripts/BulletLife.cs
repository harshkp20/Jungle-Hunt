using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float bulletLife,remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = bulletLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime<=0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            remainingTime-=Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);    
    }
}
