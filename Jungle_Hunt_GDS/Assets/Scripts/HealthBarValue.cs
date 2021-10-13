using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarValue : MonoBehaviour
{
   public Slider SliderValue;
   public Gradient gradient;
   public Image fill;

   public void MaxHealthValue(float health)
   {
      SliderValue.maxValue = health;
      SliderValue.value = health;
      fill.color = gradient.Evaluate(1f);
   }

   public void HealthValue(float health)
   {
      SliderValue.value = health;
      fill.color = gradient.Evaluate(SliderValue.value/SliderValue.maxValue);
   }

}
