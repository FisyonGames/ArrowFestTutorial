using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool isCollisionRightWall = false;
    private bool isCollisionLeftWall = false;
    private bool isCollisionFinalPoint = false;

    public bool IsCollisionRightWall { get { return isCollisionRightWall; } }
    public bool IsCollisionLeftWall { get { return isCollisionLeftWall; } }
    public bool IsCollisionFinalPoint { get { return isCollisionFinalPoint; } }

    void Start()
    {
        isCollisionFinalPoint = false;
    }

    void OnTriggerStay(Collider obj)
    {
        if(obj.gameObject.tag == "FinalPoint")
        {
            isCollisionFinalPoint = true;
        }

        if(obj.gameObject.tag == "RightWall")
        {
            isCollisionRightWall = true;
        }
        else if(obj.gameObject.tag == "LeftWall")
        {
            isCollisionLeftWall = true;
        }
        else
        {
            isCollisionRightWall = false;
            isCollisionLeftWall = false;
        }
    }
    void OnTriggerExit(Collider obj)
    {
        if(obj.gameObject.tag == "RightWall")
        {
            isCollisionRightWall = false;
        }
        if(obj.gameObject.tag == "LeftWall")
        {
            isCollisionLeftWall = false;
        }
    }
}
