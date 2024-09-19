using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class SetTextColor : MonoBehaviour
{
    [SerializeField]
    private Text txt;

    [SerializeField]
    private Color color1, color2;
    // Start is called before the first frame update
    void Awake()
    {
        if(txt == null)
		{
            txt = GetComponent<Text>();
        }
    }

    public void SetTextColor1()
	{
        txt.color = color1;
	}

    public void SetTextColor2()
	{
        txt.color = color2;
	}
}
