using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ArrowContainer : MonoBehaviour
{
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private List<Transform> arrows = new List<Transform>();
    [SerializeField] private GameController gameController;
    [SerializeField] [Range(137.0f, 138.0f)] private float degree = 137.5231367f;   // Farklı Dağıtma Açıları(Divergence Angle): 137.3°, 137.3692546°, 137.4038819°, 137.4731367°, 137.5077641°, 137.5231367°, 137.553882°, 137.5692547°, 137.6° 
    [SerializeField] [Range(0.05f, 1.0f)] private float dotScale = 0.1f; // c

    private int arrowCount;

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
        for (int i = 0; i < arrows.Count; i++)
        {
            float angle = i * (degree * Mathf.Deg2Rad);     // angle(φ) = n * 137.5°
            float r = dotScale * Mathf.Sqrt(i);             // radius(r) = c * √n
            Vector3 point = new Vector3( Mathf.Cos(angle) * r , Mathf.Sin(angle) * r , 0 );
            arrows[i].localPosition = point;
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
