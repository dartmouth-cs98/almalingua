using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public GameObject nameText;

    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.GetComponent<TMPro.TextMeshProUGUI>().text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp; 
    }
}