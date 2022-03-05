using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainerMovement : MonoBehaviour
{
    [SerializeField] [Range(5,25)] private float movementSpeed = 20f;
    [SerializeField] [Range(5,25)] private float speedForLeftRightMovement = 20f;
    private bool isRightMovement = false;
    private bool isLeftMovement = false;
    private bool isFinalMovement = false;

    void Start()
    {
        isFinalMovement = false;
    }
    
    void Update()
    {
        CheckArrowsLeftRightCollision();
        CheckArrowsFinalPointCollision();

        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) && isRightMovement && !isFinalMovement)  
        {  
            transform.Translate(Vector3.right * Time.deltaTime * speedForLeftRightMovement); 
        }  
        if (Input.GetKey(KeyCode.LeftArrow)  && isLeftMovement && !isFinalMovement)  
        {  
            transform.Translate(Vector3.left * Time.deltaTime * speedForLeftRightMovement);
        }
        if(isFinalMovement)
        {
            transform.position = Vector3.MoveTowards (transform.position, new Vector3(0f, transform.position.y,transform.position.z), speedForLeftRightMovement * Time.deltaTime);
        }
    }
    
    void CheckArrowsLeftRightCollision()
    {
        isRightMovement = true;
        isLeftMovement = true;

        foreach (var arrow in transform.GetComponent<ArrowContainer>().Arrows)
        {
            if(arrow.gameObject.GetComponent<Arrow>().IsCollisionRightWall)
            {
                isRightMovement = false;
            }
            else if(arrow.gameObject.GetComponent<Arrow>().IsCollisionLeftWall)
            {
                isLeftMovement = false;
            } 
        }
    }

    void CheckArrowsFinalPointCollision()
    {
        foreach (var arrow in transform.GetComponent<ArrowContainer>().Arrows)
        {
            if(arrow.gameObject.GetComponent<Arrow>().IsCollisionFinalPoint)
            {
                isFinalMovement = true;
                break;
            }
        }
    }

}
