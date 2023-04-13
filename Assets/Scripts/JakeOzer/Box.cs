using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;
    public float boxFill { get; private set; } = 0;
    public float boxFillMax { get; private set; } = 100;
    public float speed;
    public AudioClip fillBeep;
    public float value;

    private bool isPacked = false;
    [SerializeField] private FillBar fillBar;


    private void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        //AudioClip audio = GetComponent<AudioClip>();
        conveyorTime = Time.time;
    }

    public float GetConveyorTime() => conveyorTime;
    public int GetConveyorIndex() => conveyorIndex;
    public float GetBoxFill() => boxFill;
    public float GetBoxFillMax() => boxFillMax;
    public bool IsPacked() => isPacked;

    public void NextConveyor()
    {
        conveyorIndex++;
        conveyorTime = Time.time;
    }

    public void PackBox(float amount)
    {
        fillBar.transform.GetChild(0).gameObject.SetActive(true);
        fillBar.transform.GetChild(1).gameObject.SetActive(true);

        boxFill += amount;
        fillBar.FillBoxBar(boxFill);
        if (boxFill >= boxFillMax)
        {
            //Debug.Log("Box is filled.");
            isPacked = true;
        }

        if (isPacked = true)
        {
            GetComponent<AudioSource>().clip = fillBeep;
            GetComponent<AudioSource>().Play();

        }

    }

}
