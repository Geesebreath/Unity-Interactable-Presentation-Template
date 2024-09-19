using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to Set an object's width and height at a point between a set minimum and maximum size, and Postion between a min and max position. Potential use for
/// setting size and/or position by slider - Slider passes value to this script (0,100), and values are set using the passed value
/// as the percentage.
/// </summary>
public class SetRectTransformPercentage : MonoBehaviour
{// Combination of SetRectSizePercentage and SetRectPositionPercentage

    // Bools to control either or/both elements of transform should be changeable
    [SerializeField]
    private bool allowPositionChange = true, allowSizeChange = true;

    private RectTransform rectTransform;

    //TODO: Force range to only be between 0 and 1

    //Position Variables
    public Vector3 minPosition, maxPosition;
    [Tooltip("Must be positive values.")]
    public float positionMinPercent = 0f, positionMaxPercent = 1f;
    private float xDistance, yDistance, zDistance;
    private Vector3 startingPosition;

    //Size Variables
    public float minWidth, maxWidth, minHeight, maxHeight;
    private float wDistance, hDistance;
    [Tooltip("Must be positive values.")]
    public float sizeMinPercent = 0f, sizeMaxPercent = 1f;
    // Calculation variables
    private float relativePositionPercentDistance, relativeSizePercentDistance;

    // Shared Methods
    private void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        //Position variables set
            relativePositionPercentDistance = positionMaxPercent - positionMinPercent;

            // Get Distance between min and max values for each property for use in Position change methods methods
            xDistance = maxPosition.x - minPosition.x;
            yDistance = maxPosition.y - minPosition.y;
            zDistance = maxPosition.z - minPosition.z;
        

        if (allowSizeChange)
        {
            relativeSizePercentDistance = sizeMaxPercent - sizeMinPercent;

            // Get Distance between min and max values for each property for use in other methods
            wDistance = maxWidth - minWidth;
            hDistance = maxHeight - minHeight;
        }

    }

    void Start()
    {
        //Position
        if (allowPositionChange)
        {
            SetAbsolutePosition(minPosition);
        }

        //Size
        if (allowSizeChange)
        {
            // Set to minimum size
            SetAbsoluteSize(minWidth, minHeight);
        }
    }

    //Position Methods
    public void SetAbsolutePosition(Vector3 position)
    {
        if(allowPositionChange)
            rectTransform.anchoredPosition = position;
    }

    public void SetPositionByPercent(float passedPercent)
    {
        if (allowPositionChange)
        {
            // Check for value between max and min values
            if (passedPercent < positionMinPercent)
            {
                SetAbsolutePosition(minPosition);
            }
            else if (passedPercent > positionMaxPercent)
            {
                SetAbsolutePosition(maxPosition);
            }
            else
            {
                //Get relative Percent value for given value (what percent is it between min and max percent
                float relativePercent = ((passedPercent - positionMinPercent) / relativePositionPercentDistance);
                Debug.Log(relativePercent);

                Vector3 newPosition = Vector3.zero;

                // Find percentage value of distance from passed value and set new position
                float percentDistance = xDistance * relativePercent;
                newPosition.x = minPosition.x + percentDistance;

                percentDistance = yDistance * relativePercent;
                newPosition.y = minPosition.y + percentDistance;

                percentDistance = zDistance * relativePercent;
                newPosition.z = minPosition.x + percentDistance;

                // Set position using found values
                rectTransform.anchoredPosition = newPosition;
            }
        }
        else
        {
            Debug.LogError($"Object {rectTransform.name} is not allowed to change position by percent.");
        }
    }

    //Size Methods
    public void SetAbsoluteSize(float width, float height)
    {
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void SetSizeByPercent(float passedPercent)
    {
        if (allowSizeChange)
        {
            // Check for value between max and min values
            float finalWidth, finalHeight;
            if (passedPercent < sizeMinPercent)
            {
                finalWidth = minWidth;
                finalHeight = minHeight;
            }
            else if (passedPercent > sizeMaxPercent)
            {
                finalWidth = maxWidth;
                finalHeight = maxHeight;
            }
            else
            {
                float newWidth, newHeight;
                //Get relative Percent value for given value (what percent is it between min and max percent
                float relativePercent = ((passedPercent - sizeMinPercent) / relativeSizePercentDistance);

                // Find percentage value of distance from passed value and set new position
                float percentDistance = wDistance * relativePercent;
                newWidth = minWidth + percentDistance;

                percentDistance = hDistance * relativePercent;
                newHeight = minHeight + percentDistance;

                finalWidth = newWidth;
                finalHeight = newHeight;
            }

            SetAbsoluteSize(finalWidth, finalHeight);
        }
        else
        {
            Debug.LogError($"Object {rectTransform.name} is not allowed to change size by percent.");
        }
    }
}

