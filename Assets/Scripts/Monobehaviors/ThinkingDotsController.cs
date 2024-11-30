using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ThinkingDotsController : MonoBehaviour
{
    [SerializeField] RectTransform positionOne;
    [SerializeField] RectTransform positionTwo;
    bool atPositionOne;

    public void Initialize()
    {
        gameObject.SetActive(true);
        atPositionOne = true;
        transform.SetParent(positionOne);
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void SwitchPosition()
    {
        if(atPositionOne)
        {
            atPositionOne = false;
            transform.SetParent(positionTwo);
            GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        else
        {
            atPositionOne = true;
            transform.SetParent(positionOne);
            GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }
}
