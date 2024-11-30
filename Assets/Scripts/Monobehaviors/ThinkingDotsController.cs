using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ThinkingDotsController : MonoBehaviour
{
    [SerializeField] RectTransform positionOne;
    [SerializeField] RectTransform positionTwo;
    bool atPositionOne;

    private void OnEnable()
    {
        GameManager.OnEndTurn += SwitchPosition;
        GameManager.OnWin += Disable;
        GameManager.OnLose += Disable;
    }

    private void OnDisable()
    {
        GameManager.OnEndTurn -= SwitchPosition;
        GameManager.OnWin -= Disable;
        GameManager.OnLose -= Disable;
    }

    private void Start()
    {
        atPositionOne = true;
        transform.SetParent(positionOne);
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }


    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void SwitchPosition()
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
