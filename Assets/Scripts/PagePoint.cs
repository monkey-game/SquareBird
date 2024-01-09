using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PagePoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Scrollbar Scrollbar;
    [SerializeField] private GameObject[] ListPoin;
    private bool ToLeft;
    private bool ToRight;
    private float ValueTemp = 0;
    private int IndexP = 0;
    public void CheckMoveShop()
    {
        if (ValueTemp < Scrollbar.value)
        {
            ToLeft = false;
            ToRight = true;
            ValueTemp = Scrollbar.value;
        }
        else if (ValueTemp > Scrollbar.value)
        {
            ToRight = false;
            ToLeft = true;
            ValueTemp = Scrollbar.value;
        }
        Debug.Log("ToLeft: " + ToLeft + " ToRight: " + ToRight);
    }

}
