using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class CompletionBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    void Start()
    {
        slider.value = 0;
    }

    public void updateBar(float currentValue, float maxValue){
        slider.value = (float)(currentValue / maxValue);
    }
    // Update is called once per frame
    void Update()
    {
        //slider.value = slider.value + 1 * Time.deltaTime;
    }
}
