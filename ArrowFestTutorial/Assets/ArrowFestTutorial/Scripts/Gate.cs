using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gate : MonoBehaviour
{
    [Dropdown("operatorOptions")] public string operatorChoice;
    [Range(1,100)] public int valueToCalculate = 1;

    [SerializeField] private TMP_Text gateText;
    [SerializeField] private ArrowContainer ArrowContainer;
    

    string[] operatorOptions = new string[]
    {
        "Add", "Subtract", "Multiply", "Divide" 
    };

    void Start()
    {
        SetText();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Arrow")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            ArrowContainer.CalculateArrow(operatorChoice, valueToCalculate);
        }
    }

    void SetText()
    {
        if(operatorChoice == "Add") gateText.text = "+" + valueToCalculate.ToString();
        else if(operatorChoice == "Subtract") gateText.text = "-" + valueToCalculate.ToString();
        else if(operatorChoice == "Multiply") gateText.text = "×" + valueToCalculate.ToString();
        else if(operatorChoice == "Divide") gateText.text = "÷" + valueToCalculate.ToString();
    }
}
