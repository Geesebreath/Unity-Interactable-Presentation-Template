using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Puts a value directly into a text field.
/// </summary>
public class ValueToText : MonoBehaviour
{
    [SerializeField]
    Text textObject;
    
    [Tooltip("C# Number format specifier (ex N2 for 2 digit decimals).")]
    [SerializeField]
    string digitFormat = "0.##";
    
    public void SetText(float value)
	{
        GetText();

        textObject.text = value.ToString(digitFormat);
	}

    public void SetText(int value)
	{
        textObject.text = value.ToString();
	}

    public void SetText(bool value)
	{
        GetText();
        textObject.text = value.ToString();
	}

    private void GetText()
	{
        if (textObject == null)
        {
            textObject = GetComponent<Text>();
        }
    }


}
