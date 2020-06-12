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
		if (canInteract) StartCoroutine(coToggleLight());
	}

	// Toggle lights
	IEnumerator coToggleLight()
	{
		// Send the list of lights to the camera
		CameraController.Instance.LookAtTarget(lights, Vector3.zero, 2f);

		// Set camera speed
		float defaultSpeed = CameraController.Instance.Speed;
		CameraController.Instance.Speed = 15.5f;

		// Make sure we cant interact with this twice
		canInteract = false;

		foreach (Transform light in lights)
		{
			yield return new WaitForSecondsRealtime(1);

			light.gameObject.SetActive(!light.gameObject.activeSelf);

			yield return new WaitForSecondsRealtime(1);
		}

		yield return new WaitForSecondsRealtime(1);

		CameraController.Instance.Speed = defaultSpeed;

		canInteract = true;
	}

	// Show what lights we are connected to
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 0, 1, 0.1f);
		Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one * 2);

		foreach (Transform light in lights)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, light.transform.position);
		}
	}
}
