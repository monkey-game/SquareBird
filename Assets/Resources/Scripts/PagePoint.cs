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
        }
        else if (ValueTemp > Scrollbar.value)
        {
            ToRight = false;
            ToLeft = true;
        }
        ValueTemp = Scrollbar.value;
        IndexP = IndexPoint(Scrollbar.value);
        if(ToLeft)
        {
            if (IndexP > 0)
                ListPoin[IndexP - 1].SetActive(false);
            if (IndexP < 8)
                ListPoin[IndexP + 1].SetActive(false);
            ListPoin[IndexP].SetActive(true);
        }
        else if(ToRight)
        {
            if(IndexP <8)
            ListPoin[IndexP + 1].SetActive(false);
            if (IndexP > 0)
                ListPoin[IndexP - 1].SetActive(false);
            ListPoin[IndexP].SetActive(true);
        }
    }
    private int IndexPoint(float Value)
    {
        if((ToRight&& Value < 0.1029074)||(ToLeft && Value < 0.03281487))
        {
            return 0;
        }else if ((ToRight && Value > 0.1029074&& Value < 0.2192367) || (ToLeft && Value < 0.145914))
        {
            return 1;
        }
        else if ((ToRight && Value > 0.2190967 && Value < 0.3360844) || (ToLeft && Value < 0.2626529))
        {
            return 2;
        }
        else if ((ToRight && Value > 0.3360844 && Value < 0.453916) || (ToLeft && Value < 0.3796346))
        {
            return 3;
        }
        else if ((ToRight && Value > 0.453916 && Value < 0.5654969) || (ToLeft && Value < 0.5022736))
        {
            return 4;
        }
        else if ((ToRight && Value > 0.5654969 && Value < 0.6976954) || (ToLeft && Value < 0.6119781))
        {
            return 5;
        }
        else if ((ToRight && Value > 0.6976954 && Value < 0.7938409) || (ToLeft && Value < 0.7473455))
        {
            return 6;
        }
        else if ((ToRight && Value > 0.7938409 && Value < 0.9170689) || (ToLeft && Value < 0.8634204))
        {
            return 7;
        }
        else if ((ToRight && Value > 0.9170689) || (ToLeft && Value < 2))
        {
            return 8;
        }
        return 0;
    }

}
