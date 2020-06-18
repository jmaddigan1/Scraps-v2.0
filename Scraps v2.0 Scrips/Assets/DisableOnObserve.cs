using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnObserve : MonoBehaviour, IObservable
{
	public void OnCameraFocus()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
