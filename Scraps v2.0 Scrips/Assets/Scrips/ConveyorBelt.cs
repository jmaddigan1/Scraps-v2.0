using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
	private Transform target;
	private IPushable pushable;

    // Update is called once per frame
    void Update()
    {
		if (target == null) return;

		if (pushable.GetState() == true)
		{
			// That means this item was picked up by a player
			target = null;
			pushable = null;
		}
		else
		{
			target.position += new Vector3(0, 0, 1) * Time.deltaTime;
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IPushable>(out IPushable targetPushable))
		{
			// If we (the player) are not pushing this item
			if (targetPushable.GetState() == false) {
				target = other.transform;
				pushable = targetPushable;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<IPushable>(out IPushable targetPushable))
		{
			// If we (the player) are not pushing this item
			if (targetPushable.GetState() == false)
			{
				target = null;
				pushable = null;
			}
		}
	}
}
