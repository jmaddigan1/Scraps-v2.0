using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
	[SerializeField] List<Transform> lights;

	// If we can interact with this again
	private bool canInteract = true;

	// Interact with the light switch and toggle the connected lights
	public override void Interact(Player player)
	{
		if (canInteract) CameraController.Instance.LookAtTarget(lights, Vector3.zero);
	}

	// Show what lights we are connected to
	private void OnDrawGizmos()
	{
		if (lights.Count == 0) return;

		Gizmos.color = new Color(0, 0, 1, 0.1f);
		Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one * 2);

		foreach (Transform light in lights)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, light.transform.position);
		}
	}
}
