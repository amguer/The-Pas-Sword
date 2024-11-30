using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private UnityEvent OnTriggerEnter;
    [SerializeField] private UnityEvent OnTriggerExit;
    

    private void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            OnTriggerEnter?.Invoke();
            InputManager.OnGetSpace += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            OnTriggerExit?.Invoke();
            InputManager.OnGetSpace -= Interact;
        }
    }

    private void Interact()
    {
        OnInteract?.Invoke();
    }
}
