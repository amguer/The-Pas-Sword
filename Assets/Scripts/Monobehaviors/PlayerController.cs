using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator),(typeof(SpriteRenderer)), (typeof(Rigidbody2D)))]
public class PlayerController : MonoBehaviour
{
    private Animator animController;
    private Rigidbody2D rb2D;
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        animController.SetBool("N", true);
        animController.SetBool("E", true);
    }

    private void OnEnable()
    {
        InputManager.OnGetW += MoveNorth;
        InputManager.OnGetA += MoveWest;
        InputManager.OnGetS += MoveSouth;
        InputManager.OnGetD += MoveEast;
    }

    // Update is called once per frame
    void Update()
    {
        if(!animController.GetBool("N") && !animController.GetBool("S") && !animController.GetBool("E") && !animController.GetBool("W"))
        {
            animController.SetBool("isMoving", false);
        }
        else
        {
            animController.SetBool("isMoving", true);
        }
    }

    private void MoveNorth(bool isPressed)
    {
        animController.SetBool("N", isPressed);
        if (rb2D != null && isPressed)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        
    }

    private void MoveSouth(bool isPressed)
    {
        animController.SetBool("S", isPressed);
        if (rb2D != null && isPressed)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }

    private void MoveEast(bool isPressed)
    {
        animController.SetBool("E", isPressed);
        if (rb2D != null && isPressed)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    private void MoveWest(bool isPressed)
    {
        animController.SetBool("W", isPressed);
        if (rb2D != null && isPressed)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }


}
