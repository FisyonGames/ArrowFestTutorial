using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowContainer : MonoBehaviour
{

    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private List<Transform> arrows = new List<Transform>();
    [SerializeField] private GameController gameController;
    private int arrowCount;
    private float c = 5.25f;
    private float radiusScale = 0.0001f;
    

    public int ArrowCount { get { return arrowCount; } set { arrowCount = value; }}
    public List<Transform> Arrows { get { return arrows; }}

    void Start()
    {
        arrowCount = 1;
        Transform firstArrow = Instantiate(arrowPrefab, transform);
        arrows.Add(firstArrow);
        firstArrow.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        if(arrowCount > arrows.Count)
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
        }
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
        for(int i = 1; i <= arrows.Count; i++)
        {
            float phi = c * Mathf.Sqrt(i);
            float r = i * 137.3f * radiusScale;
            Vector3 point = new Vector3( Mathf.Cos(phi) * r , Mathf.Sin(phi) * r , 0 );
            arrows[i-1].localPosition = point;
        }
    }

    public void CalculateArrow(string opr, int value)
    {
        if(opr == "Add") arrowCount += value; 
        else if(opr == "Subtract") arrowCount -= value; 
        else if(opr == "Multiply") arrowCount *= value; 
        else if(opr == "Divide") arrowCount /= value;

        arrowCount = Mathf.Clamp(arrowCount, 0, 360);

        CheckArrowCount();
    }

    void CheckArrowCount()
    {
        // Oyun içinde arrow count 0 olduğunda gameController içindeki Fail paneli aktif edilir.
        if(arrowCount <= 0)
        {
            gameObject.SetActive(false);
            gameController.IsFail = true;
        }
    }
}
