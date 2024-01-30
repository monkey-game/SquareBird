using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSetting : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    private RectTransform rectX;
    // Start is called before the first frame update
    void Start()
    {
        rectX = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectX.sizeDelta = rect.sizeDelta;
    }
}
