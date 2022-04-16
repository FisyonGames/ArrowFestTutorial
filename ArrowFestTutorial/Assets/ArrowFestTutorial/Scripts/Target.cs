using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 5.0f)] private float multiplier;

    public float Multiplier { get { return multiplier; } }

    private void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.tag == "ArrowContainer")
        {
            FindObjectOfType<AudioManager>().Play("Target");
        }

    }
}
