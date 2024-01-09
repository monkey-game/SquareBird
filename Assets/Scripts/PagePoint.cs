using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PagePoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform RectTransform;
    [SerializeField] private RectTransform[] ListActivePoin;
    private Vector3 rectOld;
    private bool right = false;
    private bool left = false;
    private int index = 0;
    private void Start()
    {
        rectOld = RectTransform.anchoredPosition;
    }
    private void Update()
    {
        bool isMovingRight = RectTransform.anchoredPosition.x < rectOld.x; // Kiểm tra hướng di chuyển

        if (isMovingRight)
        {
            right = true;
            left = false;
            rectOld = RectTransform.anchoredPosition;
        }
        else
        {
            right = false;
            left = true;
            rectOld = RectTransform.anchoredPosition;
        }
        Debug.Log(left + " right:" + right);
        index = PosPoint(RectTransform.anchoredPosition.x);
        if (index > 0 && right && index < ListActivePoin.Length)
        {
            ListActivePoin[index - 1].gameObject.SetActive(false);
            ListActivePoin[index].gameObject.SetActive(true);
        }
        else if (index > 0 && left)
        {
            // Hãy chắc chắn rằng indexNow được định nghĩa và gán giá trị trước khi sử dụng nó
            int indexNow = index;
            if (indexNow + 1 < ListActivePoin.Length)
            {
                ListActivePoin[index + 1].gameObject.SetActive(false);
            }
            ListActivePoin[index].gameObject.SetActive(true);
        }
        else
        {
            ListActivePoin[index].gameObject.SetActive(true);

            // Kiểm tra index + 1 không vượt quá giới hạn của mảng trước khi thao tác
            if (index + 1 < ListActivePoin.Length)
            {
                ListActivePoin[index + 1].gameObject.SetActive(false);
            }
        }
    }
    public int PosPoint(float x)
    {
        if ((x < -3529.665 && left) || (x < -3093.39 && right))
        {
            return 8;
        }
        else if ((x < -2491.442 && left) || (x < -2111.17 && right))
        {
            return 7;
        }
        else if ((x < -1675.605 && left) || (x < -937.8705 && right))
        {
            return 6;
        }
        else if ((x < -579.3984 && left) || (x < -16.61371 && right))
        {
            return 5;
        }
        else if ((x < 389.7552 && left) || (x < 1012.357 && right))
        {
            return 4;
        }
        else if ((x < 1386.098 && left) || (x < 2015.852 && right))
        {
            return 3;
        }
        else if ((x < 2393.933 && left) || (x < 3021.007 && right))
        {
            return 2;
        }
        else if ((x < 3363.817 && left) || (x < 3968.379 && right))
        {
            return 1;
        }
        return 0;
    }

}
