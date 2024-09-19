using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Can Move and Rotate rect transforms, if allowed.
/// </summary>
public class MoveRect : MonoBehaviour, CommonInterface
{// combines the MoveRectTransformSmoothly and RotateRectToValue scripts. Changes should be reflected in EMoveRect editor script.
	
	// Rect Movement Variables
	public bool movementAllow = true;

	[SerializeField]
	private RectTransform rect;
	//Move Rect Variables
	private bool isAtStart;
	public Vector3 startPosition;
	public Vector3 finalPosition;
	

	private Vector3 targetPosition, currentStartingPosition;
	public float moveSpeed = 0.5f;
	private Vector3 velocity = Vector3.zero;
	private float movementDampLeeway = 1f;
	public UnityEvent moveStartedActions, moveEndedActions, FinishedAtStart, FinishedAtEnd;
	private Coroutine moveRoutine;

	//Rect Rotation Variables
	public bool rotationAllow = true;
	public float targetRotationValue, rotationSpeed;

	public UnityEvent rotateEndingEvent;

	private float rotationDampLeeway = 0.05f;
	private Coroutine rotateRoutine;
	private bool executePostRotationEvent = true;
	public bool PostRotationEventOnlyOnce = false;


	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		if(movementAllow)
		{
			SetToStartPosition();
		}
		if (rotationAllow)
		{
			executePostRotationEvent = true;
		}
	}

	public void SetAnchoredPosition(Vector2 passedPosition)
	{
		rect.anchoredPosition = passedPosition;
	}

	public void SetToStartPosition()
	{
		if(movementAllow)
		{
			if (moveRoutine != null)
			{
				StopCoroutine(moveRoutine);
			}
			rect.anchoredPosition = startPosition;
			isAtStart = true;
		}
	}

	public void SetToFinalPosition()
	{
		if (movementAllow)
		{if (movementAllow)
			{
				if (moveRoutine != null)
				{
					StopCoroutine(moveRoutine);
				}
				rect.anchoredPosition = finalPosition;
				isAtStart = false;
			}
		}
	}

	public void Reset()
	{
		StopAllCoroutines();
		Awake();
	}

	public void MoveTo(Vector3 moveToPosition)
	{
		targetPosition = moveToPosition;
		moveStartedActions.Invoke();
		StartAnimate(targetPosition);
	}

	public void MoveToEnd()
	{
		if(movementAllow)
		{
			MoveTo(finalPosition);
			isAtStart = false;
		}
	}

	public void MoveToStart()
	{
		if(movementAllow)
		{
			MoveTo(startPosition);
			isAtStart = true;
		}
	}

	public void ToggleMove()
	{
		if(movementAllow)
		{
			if (isAtStart)
			{
				MoveToEnd();
			}
			else
			{
				MoveToStart();
			}
		}
	}

	private void StartAnimate(Vector3 position)
	{
		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
		}
		currentStartingPosition = rect.anchoredPosition;
		moveRoutine = StartCoroutine(SmoothDampRoutine(position));
	}



	private IEnumerator SmoothDampRoutine(Vector3 moveToVector)
	{
		velocity = Vector3.zero;
		bool keepSmoothing = false;

		while (!keepSmoothing)
		{
			// Smoothly move the rect towards that target position
			rect.anchoredPosition = Vector3.SmoothDamp(rect.anchoredPosition, targetPosition, ref velocity, moveSpeed);
			float xDifference = Mathf.Abs(moveToVector.x - rect.anchoredPosition.x);
			float yDifference = Mathf.Abs(moveToVector.y - rect.anchoredPosition.y);

			if (xDifference <= movementDampLeeway && yDifference <= movementDampLeeway)
			{
				rect.anchoredPosition = moveToVector;
				keepSmoothing = true;
			}
			yield return null;
		}

		// Events invoke at end of movement
		moveEndedActions.Invoke();

		// Specific events for if moved to marked start or end
		if (isAtStart == false)
		{
			FinishedAtEnd.Invoke();
		}
		else
		{
			FinishedAtStart.Invoke();
		}
	}

	// Rotate Rect Sript

	/// <summary>
	/// Sets object to passed rotation Euler angle. Does not execute any events.
	/// </summary>
	/// <param name="angle"></param>
	public void SetRotation(float angle)
	{
		if (rotateRoutine != null)
		{
			StopCoroutine(rotateRoutine);
		}
		if (rect == null)
		{
			rect = GetComponent<RectTransform>();
		}
		rect.rotation = Quaternion.Euler(0, 0, angle);
	}

	/// <summary>
	/// Smoothly rotates object to passed angle.
	/// </summary>
	/// <param name="angle"></param>
	public void ExecuteRotation(float angle)
	{
		if(rotationAllow)
		{
			if (rotateRoutine != null)
			{
				StopCoroutine(rotateRoutine);
			}
			rotateRoutine = StartCoroutine(RotateOverTime(angle));
		}
		else
		{
			Debug.LogError("Rotation is not set to be allowed on this object.");
		}
	}


	/// <summary>
	/// Rotate object to saved target rotation.
	/// </summary>
	public void RotateToTarget()
	{
		ExecuteRotation(targetRotationValue);
	}

	private IEnumerator RotateOverTime(float finalAngle)
	{
		// rotate smoothly over the set period of time. Code modified from MoveRectTransformSmoothly
		bool keepSmoothing = false;

		while (!keepSmoothing)
		{
			// Smoothly rotate towards final rotation.
			rect.rotation = Quaternion.Slerp(rect.rotation, Quaternion.Euler(0, 0, finalAngle), rotationSpeed / 100f);

			float rotateDifference = (-360 - finalAngle) + transform.rotation.eulerAngles.z;
			if (Mathf.Abs(rotateDifference) <= rotationDampLeeway)
			{
				rect.rotation = Quaternion.Euler(0, 0, finalAngle);
				keepSmoothing = true;
			}
			yield return null;
		}
		// To only execute the event once (such as if the event is to do another rotation on the same object
		if (PostRotationEventOnlyOnce)
		{
			if (executePostRotationEvent)
			{
				executePostRotationEvent = false;
				rotateEndingEvent.Invoke();
			}
			else
			{
				executePostRotationEvent = true;
			}
		}
		else
		{
			rotateEndingEvent.Invoke();
		}
	}
}

