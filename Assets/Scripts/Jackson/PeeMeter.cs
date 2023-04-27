using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeMeter : MonoBehaviour
{
    [SerializeField]
    private float peeTime;
    [SerializeField]
    private int peeAmount;
    private bool chargeCooldown;
    private bool spriteEnabled;
    private GameObject peeMeter;
    private SpriteRenderer spriteRenderer;

    private Vector2 startPos;

    [SerializeField]
    private float shakeSpeed;
    [SerializeField]
    private float shakeAmount;

    private Vector3 startingPos;

    [SerializeField]
    private Sprite pee0;
    [SerializeField]
    private Sprite pee1;
    [SerializeField]
    private Sprite pee2;
    [SerializeField]
    private Sprite pee3;
    [SerializeField]
    private Sprite pee4;
    [SerializeField]
    private Sprite pee5;
    [SerializeField]
    private Sprite pee6;

    bool peeBreak;
    bool forcedPee;




    // Start is called before the first frame update
    void Start()
    {
        peeMeter = gameObject.transform.GetChild(0).gameObject;
        spriteRenderer = peeMeter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pee0;
        startingPos = peeMeter.transform.position;
        chargeCooldown = true;
        peeBreak = false;
        forcedPee = false;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.enabled = spriteEnabled;
        displayMeter();
        if (chargeCooldown)
        {
            StartCoroutine(peeCharge());
        }
        
    }

    private void LateUpdate()
    {
        spriteEnabled = false;
    }

    private void OnMouseOver()
    {
        if (!peeBreak)
        {
            spriteEnabled = true;
        }

        if (Input.GetMouseButtonDown(0) && !peeBreak)
        {
            peeBreak = true;

            startPeeBreak();
        }
    }

    private void startPeeBreak()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        startPos = gameObject.transform.position;
        transform.position = new Vector3(1000, 1000, 1000);

        StartCoroutine(peeBreakTime());
    }

    private IEnumerator peeCharge()
    {
        chargeCooldown = false;
        yield return new WaitForSeconds(peeTime);
        int random = Random.Range(1, 6);
        Debug.Log(random);
        if (random == 5 && !peeBreak)
        {
            peeAmount++;
        }
        chargeCooldown = true;
    }

    private IEnumerator peeBreakTime()
    {        
        peeAmount = 0;

        if (forcedPee)
        {
            yield return new WaitForSeconds(20);
            forcedPee = false;
        }
        else
        {
            yield return new WaitForSeconds(10);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        peeBreak = false;
        gameObject.transform.position = startPos;

    }


    private void displayMeter()
    {
        switch (peeAmount)
        {
            case 0:
                spriteRenderer.sprite = pee0;
                peeMeter.transform.position = startingPos;
                break;

            case 1:
                spriteRenderer.sprite = pee1;
                peeMeter.transform.position = startingPos;
                break;

            case 2:
                spriteRenderer.sprite = pee2;
                peeMeter.transform.position = startingPos;
                break;

            case 3:
                spriteRenderer.sprite = pee3;
                peeMeter.transform.position = startingPos;
                break;

            case 4:
                spriteRenderer.sprite = pee4;
                peeMeter.transform.position = startingPos;
                break;

            case 5:
                spriteRenderer.sprite = pee5;
                peeMeter.transform.position = startingPos;
                break;

            case 6:
                spriteRenderer.sprite = pee6;
                peeMeter.transform.position = new Vector2((startingPos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount), (startingPos.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount));
                break;

            case 7:
                forcedPee = true;
                peeBreak = true;
                startPeeBreak();
                break;
        }
    }
}
