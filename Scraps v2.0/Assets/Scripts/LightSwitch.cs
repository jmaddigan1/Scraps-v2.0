using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
	[SerializeField] Light light;

	private bool canInteract = true;

	public override void Interact(Player player) {
		if (canInteract) StartCoroutine(coToggleLight());
	}
	public override void OnEnterInteractable()
	{
		Debug.Log("Enter Trigger");
	}

	public IEnumerator coToggleLight()
	{

		CameraController.Instance.LookAtTarget(light.transform, 3f);
		canInteract = false;

		yield return new WaitForSecondsRealtime(1);

		light.gameObject.SetActive(!light.gameObject.activeSelf);

		yield return new WaitForSecondsRealtime(2);
		canInteract = true;
	}

	private void OnDrawGizmos()
	{
		if (light != null) Gizmos.DrawLine(transform.position, light.transform.position);
	}
}
