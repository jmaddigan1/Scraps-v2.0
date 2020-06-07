using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;

	public Transform Target;

	[SerializeField] Vector3 offset = new Vector3(0, 3f, 4f);
	[SerializeField] float duration;

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		if (Target == null) Target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (Target)
		{
			transform.position = Vector3.Lerp(transform.position, Target.position + offset, Time.deltaTime * 2f);
		}
	}

	public void LookAtTarget(Transform newTarget, float duration)
	{
		StartCoroutine(coLookAtTarget(newTarget, duration));
	}

	IEnumerator coLookAtTarget(Transform newTarget, float duration)
	{
		Transform oldTarget = Target;
		float counter = 0.0f;

		do
		{
			yield return null;
			Target = newTarget;

		} while ((counter += Time.deltaTime) < duration);

		Target = oldTarget;
	}
}
