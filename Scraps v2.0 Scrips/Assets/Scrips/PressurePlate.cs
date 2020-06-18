using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressurePlate : MonoBehaviour
{
	private IPushable pushable;

	private void Update()
	{
		if (pushable == null) return;

		if (pushable.GetState() == true)
		{
			// That means this item was picked up by a player
			pushable = null;
		}
		else
		{
			// Do pressure plate stuff
			OnPress();
		}
	}

	// On Initial press
	public virtual void OnDown()
	{
		
	}

	// On held down
	public virtual void OnPress()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IPushable>(out IPushable targetPushable))
		{
			// If we (the player) are not pushing this item
			if (targetPushable.GetState() == false) {
				pushable = targetPushable;

				// trigger pressure plate stuff (one time)
				OnDown();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<IPushable>(out IPushable targetPushable))
		{
			// If we (the player) are not pushing this item
			if (targetPushable.GetState() == false) {
				pushable = null;
			}
		}
	}
}
