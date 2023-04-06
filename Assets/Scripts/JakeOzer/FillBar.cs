using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    private Slider fillBar;
    private Box curBox;

    private void Start()
    {
        curBox = this.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Box>();
        fillBar = GetComponent<Slider>();
        fillBar.maxValue = curBox.GetBoxFillMax();
        fillBar.value = curBox.GetBoxFill();
    }

    public void FillBoxBar(float fill)
    {
        fillBar.value = fill;
    }

}
