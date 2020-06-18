using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Wall : PressurePlate, IObservable
{
	[SerializeField] private Transform wall;

	public override void OnDown()
	{
		CameraController.Instance.LookAtTarget(new List<Transform>() { transform, wall }, Vector3.zero);
	}

	public void OnCameraFocus()
	{

	}
}
