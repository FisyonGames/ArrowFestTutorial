using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainerMovement : MonoBehaviour
{
    [SerializeField] [Range(5,25)] private float movementSpeed = 20f;
    [SerializeField] [Range(5,25)] private float speedForLeftRightMovement = 20f;
    [SerializeField] private float leftClampXPositionValue = -4.1f;
    [SerializeField] private float rightClampXPositionValue = 4.1f;
    private bool isMoving = false;
    private bool isRightMovement = false;
    private bool isLeftMovement = false;
    private bool isFinalMovement = false;

    void Start()
    {
        isFinalMovement = false;
        isMoving = false;
    }
    
    void Update()
    {
        // İlk hareket için ekrana veya klavye-mouse için herhangi bir tuşa basılacak.
        if (Input.touchCount > 0 || Input.anyKeyDown)
        {
            isMoving = true;
        }
        
        if(isMoving)
        {
            Movement();
        }
        if(isFinalMovement)
        {
            FinalMovement();
        }

        CheckArrowsFinalPointCollision();
    }

    void CheckArrowsFinalPointCollision()
    {
        foreach (var arrow in transform.GetComponent<ArrowContainer>().Arrows)
        {
            if(arrow.gameObject.GetComponent<Arrow>().IsCollisionFinalPoint)
            {
                isFinalMovement = true;
                isMoving = false;
                break;
            }
        }
    }

    private void Movement()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftArrow) || MobileInput.Instance.swipeLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speedForLeftRightMovement);
        }
        if(Input.GetKey(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speedForLeftRightMovement);
        }

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftClampXPositionValue, rightClampXPositionValue);
        transform.position = clampedPosition;
    }

    private void FinalMovement()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards (transform.position, new Vector3(0f, transform.position.y,transform.position.z), speedForLeftRightMovement * Time.deltaTime);
    }
}
