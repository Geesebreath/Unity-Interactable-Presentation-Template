using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Allows hitting Tab to move to the next UI element in the List. Placed on Section with elements that can be tabbed between. Disable when those elements are no longer on screen.
/// </summary>
public class TabToNextGUIElement : MonoBehaviour
{
    public List<Selectable> elements;   // add UI elements in inspector in desired tabbing order
    int index;

    void Start()
    {
        // always leave at -1 initially
        index = -1;   

        // uncomment to have focus on first element in the list
        //elements[0].Select(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].gameObject.Equals(EventSystem.current.currentSelectedGameObject))
                {
                    index = i;
                    break;
                }
            }

			//if (Input.GetKey(KeyCode.LeftShift))
			//{
			//	index = index > 0 ? --index : index = elements.Count - 1;
			//}
			//else
			//{
				index = index < elements.Count - 1 ? ++index : 0;
			//}
			Debug.Log(index);
            Debug.Log(elements[index].name);

            if(elements[index].GetComponent<InputField>() != null)
			{
                StartCoroutine(SelectInputField(elements[index].GetComponent<InputField>()));
            }
            else
			{
                elements[index].Select();
            }
            
        }
    }

    IEnumerator SelectInputField(InputField field)
    {
        yield return new WaitForEndOfFrame();
        field.ActivateInputField();
    }
}