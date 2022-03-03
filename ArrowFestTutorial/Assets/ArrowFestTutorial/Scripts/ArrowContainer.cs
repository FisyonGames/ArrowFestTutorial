using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainer : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private List<Transform> arrows = new List<Transform>();
    [Range(1,300)] [SerializeField]private int arrowCount;

    public int ArrowCount { get { return arrowCount; } set { arrowCount = value; }}

    void Start()
    {
        arrowCount = 1;
        Transform firstArrow = Instantiate(arrowPrefab, transform);
        arrows.Add(firstArrow);
        firstArrow.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        transform.position = transform.position + Vector3.forward * movementSpeed * Time.deltaTime;

        /* if(arrowCount > arrows.Count)
        {
            CreateArrow();
        }
        else if(arrowCount < arrows.Count)
        {
            DestroyArrow();
        }
        else
        {
            CollateArrow();
        } */
    }

    void CreateArrow()
    {
        for(int i = arrows.Count; i < arrowCount; i++)
        {
            Transform newArrow = Instantiate(arrowPrefab, transform);
            arrows.Add(newArrow);
            newArrow.transform.localPosition = Vector3.zero;
        }

        CollateArrow();
    }

    void DestroyArrow()
    {
        for(int i = arrows.Count - 1; i >= arrowCount; i--)
        {
            Transform arrowToDestroy = arrows[i];
            arrows.RemoveAt(i);
            Destroy(arrowToDestroy.gameObject);
        }

        CollateArrow();
    }

    void CollateArrow()
    {
        float angle = 360.0f / arrows.Count;

        for (int i = 0; i < arrows.Count; i++)
        {
            MoveArrows(arrows[i], i * angle);
        }
    }

    void MoveArrows(Transform objTransform, float degree)
    {
        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Cos(degree * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(degree * Mathf.Deg2Rad);

        objTransform.localPosition = pos;
    }
}
