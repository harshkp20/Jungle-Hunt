using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject Bullet;
    public float Speed;
    public Vector2 MousePos;
    public ParticleSystem BulletBurst;
    public Vector2 Direction;
   
    // Start is called before the first frame update
    void Start()
    {
        ShootPoint = this.transform.GetChild(1).gameObject.transform;
        BulletBurst = ShootPoint.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(Bullet,ShootPoint.position,ShootPoint.rotation);
            if(this.GetComponent<CharacterController2D>().m_FacingRight)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = transform.right*Speed;
            }
            else bullet.GetComponent<Rigidbody2D>().velocity = transform.right*(-Speed);
            BulletBurst.Play();
        }        
    }
}
