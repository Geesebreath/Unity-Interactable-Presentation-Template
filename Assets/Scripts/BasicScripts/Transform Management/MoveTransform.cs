using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTransform : MonoBehaviour, CommonInterface
{
	//Changes to this class need to be reflected in the EMoveTransform script as well for editor clarity.
	// Move Transform Variables
	public bool movementAllow = true;

	[SerializeField]
	private Transform tform;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public float moveSpeed = 0.5f;
	public UnityEvent moveEndedEvent, finishedAtStartEvent, finishedAtEndEvent;

	public float dampLeeway = 1f;
	private bool isAtStart;
	private Vector3 targetPosition, currentStartingPosition, velocity = Vector3.zero;
	private Coroutine moveRoutine;

	// Rotate Transform Variables
	public bool rotationAllow = true;

	public Vector3 startRotation;
	public Vector3 rotationSpeeds;
	private bool rotate;

	//Shared Methods
	public void Awake()
	{
		if (tform == null)
		{
			tform = GetComponent<Transform>();
		}
	}

	public void Reset()
	{
		if (tform == null)
		{
			tform = GetComponent<Transform>();
		}

		StopAllCoroutines();
		SetPosition(startPosition);
		StopRotating();
		ResetRotation();
	}

	void Update()
	{
		// Move Transform Code

		// Rotate Transform Code
		if (rotate)
		{
			base.transform.Rotate(rotationSpeeds * Time.deltaTime);
		}
	}

	/// <summary>
	/// Set position to passed Vector3
	/// </summary>
	/// <param name="passedPosition"></param>
	public void SetPosition(Vector3 passedPosition)
	{
		StopMoving();
		tform.position = passedPosition;
	}

	//Move Transform Methods
	/// <summary>
	/// Place at end Position
	/// </summary>
	public void MoveToEnd()
	{
		if(movementAllow)
		{
			MoveTo(endPosition);
			isAtStart = false;
		}
	}

	/// <summary>
	/// Place at start position.
	/// </summary>
	public void MoveToStart()
	{
		if(movementAllow)
		{
			MoveTo(startPosition);
			isAtStart = true;
		}
	}

	/// <summary>
	/// Lerp to passed position
	/// </summary>
	/// <param name="moveToPosition"></param>
	public void MoveTo(Vector3 moveToPosition)
	{
		StopMoving();
		targetPosition = moveToPosition;
		StartAnimate(targetPosition);
	}

	/// <summary>
	/// Move smoothly to end if at start, and vice versa.
	/// </summary>
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

	/// <summary>
	/// Stops moving the transform.
	/// </summary>
	private void StopMoving()
	{
		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
		}
	}

	/// <summary>
	/// Begins the movement coroutine toward passed Vector3 position
	/// </summary>
	/// <param name="position"></param>
	private void StartAnimate(Vector3 position)
	{
		StopMoving();
		currentStartingPosition = tform.localPosition;
		moveRoutine = StartCoroutine(SmoothDampRoutine(position));
	}

	private IEnumerator SmoothDampRoutine(Vector3 moveToVector)
	{
		velocity = Vector3.zero;
		bool keepSmoothing = false;

		while (!keepSmoothing)
		{
			// Smoothly move the camera towards that target position
			tform.localPosition = Vector3.SmoothDamp(tform.localPosition, targetPosition, ref velocity, moveSpeed);
			float xDifference = Mathf.Abs(moveToVector.x - tform.localPosition.x);
			float yDifference = Mathf.Abs(moveToVector.y - tform.localPosition.y);
			float zDifference = Mathf.Abs(moveToVector.z - tform.localPosition.z);
			Debug.Log($"{xDifference},{yDifference},{zDifference}");
			if (xDifference <= dampLeeway && yDifference <= dampLeeway && zDifference <= dampLeeway)
			{
				//tf.localPosition = moveToVector;
				keepSmoothing = true;
			}
			yield return null;
		}
		moveEndedEvent.Invoke();

		if (isAtStart == false)
		{
			finishedAtEndEvent.Invoke();
		}
		else
		{
			finishedAtStartEvent.Invoke();
		}
	}

	//Rotate Transform Methods

	public void StartRotating()
	{
		if(rotationAllow)
		{
			rotate = true;
		}
	}

	public void StopRotating()
	{
		if (rotationAllow)
		{
			rotate = false;
		}
	}

	public void ResetRotation()
	{
		if (rotationAllow)
		{
			base.transform.rotation = Quaternion.Euler(startRotation);
		}
	}

}
