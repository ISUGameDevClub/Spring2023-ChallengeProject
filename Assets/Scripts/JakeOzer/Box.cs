using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Box : MonoBehaviour
{
    private float conveyorTime;
    private int conveyorIndex = 0;

    public float boxFill { get; private set; } = 0;
    public float boxFillMax { get; private set; } = 100;
    public float speed;

    public float value;

    private bool isPacked = false;
    [SerializeField] private FillBar fillBar;
    [SerializeField] private Sprite packedSprite;

    public event Action boxPacked;
    private void Start()
    {
        conveyorTime = Time.time;
    }

    public float GetConveyorTime() => conveyorTime;
    public int GetConveyorIndex() => conveyorIndex;
    public float GetBoxFill() => boxFill;
    public float GetBoxFillMax() => boxFillMax;
    public bool IsPacked() => isPacked;
    public bool hasBeenPacked = false;

    public void NextConveyor()
    {
        conveyorIndex++;
        conveyorTime = Time.time;
    }

    public bool PackBox(float amount)
    {
        fillBar.transform.GetChild(0).gameObject.SetActive(true);
        fillBar.transform.GetChild(1).gameObject.SetActive(true);

        boxFill += amount;
        fillBar.FillBoxBar(boxFill);
        if (boxFill >= boxFillMax)
        {
            //Debug.Log("Box is filled.");
            GetComponent<SpriteRenderer>().sprite = packedSprite;
            fillBar.transform.GetChild(0).gameObject.SetActive(false);
            fillBar.transform.GetChild(1).gameObject.SetActive(false);
            isPacked = true;
        }
        return isPacked;
    }

    void update()
    {
        if (isPacked && !hasBeenPacked)
        {
            boxPacked?.Invoke();
            hasBeenPacked = true;
        }
    }

}
