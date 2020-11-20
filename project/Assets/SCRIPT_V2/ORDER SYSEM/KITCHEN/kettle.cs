using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kettle : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI kettleText;
    [SerializeField] private GameObject warmTempSprite;
    [SerializeField] private GameObject hotTempSprite;

    [SerializeField] public water water;
    [HideInInspector] public Water Water = new Water();
    [HideInInspector] public bool isBoilingWater = false;
    [HideInInspector] public bool isCooling = false;


    public void HeatWater()
    {
        Water.temp += 1 * Time.deltaTime;

    }

    public void HeatWater(float multiplier)
    {

        Water.temp += multiplier * Time.deltaTime;

    }

    public bool isBoiling()
    {

        if (Water.temp >= 100)
        {
            
            return true;
        }
        
        return false;
    }

    public bool isBoiling(float maxTemp)
    {

        if (Water.temp >= maxTemp)
        {
            hotTempSprite.SetActive(true);
            warmTempSprite.SetActive(false);
            return true;
        }
        hotTempSprite.SetActive(false);
        warmTempSprite.SetActive(true);
        return false;
       
    }





    public water emptyKettle()
    {
        return water;
    }

    public void Fill()
    {
        if (Water.amount <= 0)
        {
            Water.amount = 10;
        }
    }
     
}


public class Water
{

    public float temp;
    public float amount;
    

    public Water()
    {
        temp = 0;
        amount = 10;
        
        
    }

    public Water(float _temp, float _amount)
    {
        temp = _temp;
        amount = _amount;
    }


}
// 30 - 69
// 70 - 99

    // over 100 is too hot - jump to 130 for cooling