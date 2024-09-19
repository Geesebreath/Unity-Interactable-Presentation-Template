using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Copies text from one Text component to another.
/// </summary>
public class TextDuplicator : MonoBehaviour
{
    public Text textToDuplicate, textTarget;

    void FixedUpdate()
    {
        textTarget.text = textToDuplicate.text;
    }
}
