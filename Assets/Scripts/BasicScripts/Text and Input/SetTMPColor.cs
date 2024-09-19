using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Sets the text color of a text mesh pro component.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class SetTMPColor : MonoBehaviour, CommonInterface
{
    private TextMeshProUGUI tmp;

    private Color defaultColor;

    [SerializeField]
    private Color highlightColor;

	private void Awake()
	{
		if (tmp == null)
		{
			tmp = GetComponent<TextMeshProUGUI>();
		}
        
		defaultColor = tmp.color;
	}

	public void SetColor(Color color)
	{
		if (tmp == null)
		{
			tmp = GetComponent<TextMeshProUGUI>();
		}
		tmp.color = color;
	}

	public void Highlight()
	{
		if (tmp == null)
		{
			tmp = GetComponent<TextMeshProUGUI>();
		}
		tmp.color = highlightColor;
	}

	public void Unhighlight()
	{
		if(tmp == null)
		{
			GetComponent<TextMeshProUGUI>();
		}
		tmp.color = defaultColor;
	}

	public void Reset()
	{
		if(defaultColor == null)
		{
			if (tmp == null)
			{
				tmp = GetComponent<TextMeshProUGUI>();
				defaultColor = tmp.color;
			}
		}
		Unhighlight();
	}
}
