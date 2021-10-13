using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float max_health = 100f;
    public float current_health;    
    public HealthBarValue bar_value;

    void Start()
    {
        current_health = max_health;
        bar_value.MaxHealthValue(current_health);
    }

    void New_Value(float damage)
    {
        current_health -= damage;
    }
    
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     {
        //         if(current_health > 0){
        //             New_Value(0.5f);
        //         }
        //         bar_value.HealthValue(current_health);
        //     }
        // }
    }
}
