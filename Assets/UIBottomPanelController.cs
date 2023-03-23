using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottomPanelController : MonoBehaviour
{
    [SerializeField] 
    private Animator animator;

    private bool up = false;

    public void Toggle()
    {
        if (up)
        {
            animator.Play("uidown");
            up = false;
        }
        else
        {
            animator.Play("uiup");
            transform.SetAsLastSibling();
            up = true;
        }
    }
}
