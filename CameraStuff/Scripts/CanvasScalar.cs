using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScalar : MonoBehaviour
{

    public bool bottom;
    Canvas canvas;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        //rectTransform.rect.height = canvas.GetComponent<RectTransform>().rect.height / 2;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, canvas.GetComponent<RectTransform>().rect.height / 2);
        if(bottom)
            rectTransform.position = new Vector3(rectTransform.position.x, canvas.GetComponent<RectTransform>().rect.height / 2, rectTransform.position.z);
    }
}
