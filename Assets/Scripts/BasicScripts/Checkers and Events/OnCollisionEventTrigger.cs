using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Triggers UnityEvent on Collision
/// </summary>
public class OnCollisionEventTrigger : MonoBehaviour
{
	//TODO: combine with 2dCollsion event script
	public UnityEvent collisionEvent;
	public void OnCollisionEnter(Collision collision)
	{
		collisionEvent.Invoke();
	}

	public void OnCollisionEnter2D()
	{
		collisionEvent.Invoke();
	}
}
