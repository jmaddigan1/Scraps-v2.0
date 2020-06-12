using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{ 
	// Whos goal is this?
	public OwnerType type;

	// Is there a person in this goal?
	public bool occupied;

	public bool ValidateOwner(Collider other)
	{
		if (other.gameObject.TryGetComponent<Player>(out Player player)) {
			if (player.ownerType == type)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (ValidateOwner(other)) {
			occupied = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (ValidateOwner(other)) {
			occupied = false;
		}
	}

	private void OnDrawGizmos()
	{
		Color color = occupied ? new Color(0, 1, 0, 0.2f) : new Color(1, 0, 0, 0.2f);

		Gizmos.color = color;
		Gizmos.DrawCube(transform.position + Vector3.up, new Vector3(4, 2, 2));
	}
}

public enum OwnerType { Fox, Robot }
