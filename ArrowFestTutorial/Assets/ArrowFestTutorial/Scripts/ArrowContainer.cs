using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ArrowContainer : MonoBehaviour
{
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private List<Transform> arrows = new List<Transform>();
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform arrowContainerBackground;
    [SerializeField] private TMP_Text arrowCountText;
    [SerializeField] private ArrowContainerMovement arrowContainerMovement;
    
    private float degree = 137.5231367f;   // Farklı Dağıtma Açıları(Divergence Angle): 137.3°, 137.3692546°, 137.4038819°, 137.4731367°, 137.5077641°, 137.5231367°, 137.553882°, 137.5692547°, 137.6° 
    private float dotScale = 0.1f; // 0.05f <= c <=1.0f
    private int arrowCount;
    private List<Vector2> traverseSpiralPos = new List<Vector2>();
    private int bowlingPinDecrement = 10;
    
    public int ArrowCount { get { return arrowCount; } set { arrowCount = value; }}
    public List<Transform> Arrows { get { return arrows; }}

    void Start()
    {
        arrowCount = 1;
        arrowCountText.text = arrowCount.ToString();

        Transform firstArrow = Instantiate(arrowPrefab, transform);
        arrows.Add(firstArrow);
        firstArrow.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "FinalArea")
        {
            arrowContainerMovement.IsFinalMovement = true;
            arrowContainerBackground.gameObject.SetActive(false);
        }
        if(col.gameObject.tag == "Gate")
        {
            CalculateNewArrowCount( col.gameObject.GetComponent<Gate>().operatorChoice, 
                                    col.gameObject.GetComponent<Gate>().valueToCalculate);
            
            if(arrowCount > arrows.Count)
            {
                CreateArrow();
            }
            else if(arrowCount < arrows.Count)
            {
                DestroyArrow();
            }
        }
        if(col.gameObject.tag == "Target")
        {
            gameController.Score += Mathf.RoundToInt(arrowCount * col.GetComponent<Target>().Multiplier);
        }
        if(col.gameObject.tag == "BowlingPin")
        {
            arrowCount -= bowlingPinDecrement;
            if(arrowCount <= 0)
            {
                gameObject.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameController.ActivateLevelCompletedPanel();
            }
        }
        if(col.gameObject.tag == "EndPoint")
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameController.ActivateLevelCompletedPanel();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "FinalArea")
        {
            CollateArrowForFinal();
            GetComponent<BoxCollider>().size = new Vector3(3.0f, 1.0f, 6.0f);
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

    public void CalculateNewArrowCount(string opr, int value)
    {
        if(opr == "Add") arrowCount += value; 
        else if(opr == "Subtract") arrowCount -= value; 
        else if(opr == "Multiply") arrowCount *= value; 
        else if(opr == "Divide") arrowCount /= value;

        arrowCount = Mathf.Clamp(arrowCount, 0, 720);
        arrowCountText.text = arrowCount.ToString();

        CheckArrowCount();
    }

    // if arrow count is zero, FAIL.
    void CheckArrowCount()
    {
        if(arrowCount <= 0)
        {
            gameObject.SetActive(false);
            gameController.ActivateFailPanel();
        }
    }
    
    void CollateArrowForFinal()
    {
        int column = ((float)arrows.Count / 50) < 10 ? Mathf.CeilToInt((float)arrows.Count / 50) : 10;
        int row = Mathf.CeilToInt((float)arrows.Count / column);
        float distance = arrowPrefab.localScale.x * 0.1f;

        TraverseSpiral(column, row, arrows.Count);

        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].localPosition = new Vector3(  traverseSpiralPos[i].x * distance,
                                                    traverseSpiralPos[i].y * distance,
                                                    0f);
        }
    }

    

    // https://www.baeldung.com/cs/looping-spiral
    private List<Vector2> TraverseSpiral(int column, int row, int count) 
    {
        traverseSpiralPos.Clear();

        float x = 0;
        float y = 0;
        float dx = 0;
        float dy = -1;

        float t;

        for (int i = 0; i < Mathf.Pow(Mathf.Max(row, column), 2); i++)
        { 
            if((x > -row / 2.0f  && x <= row / 2.0f ) && (y > -column / 2.0f && y <= column / 2.0f))
            {        
                traverseSpiralPos.Add(new Vector2(x, y));
            }
            if(x == y || (x == -y && x < 0) || (x == 1 - y && x > 0))
            {
                t = dx;
                dx = -dy;
                dy = t;
            }

            x += dx;
            y += dy;

            if(traverseSpiralPos.Count >= count)
            {
                break;
            }
        }
        return traverseSpiralPos;
    }

}
