using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functionless component, meant to be able to create and edit comments directly on a game object within the editor.
/// </summary>
public class ObjectComment : MonoBehaviour
{
    //For Comments on Objects only in edit

    // TODO: Create a toggle for a Debug box, and a method to put in data. May be used to track values that are being passed to a specific
    // object/component, or to put debug on specific objects. Will need to be able to hide the debug text area if a boolean is not checked
    [TextArea(4,25)]
    public string comment = "Comment Goes Here";

}
