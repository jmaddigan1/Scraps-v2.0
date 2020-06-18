using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;

	public List<Transform> PlayerCharacters;
	public List<Transform> ObjectTargets;

	public Transform Target;

	public int CharacterIndex = 0;

	public bool Looking = false;

	public Vector3 Offset = new Vector3(0, 3f, 4f);

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		// Find all player object in the scene.
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{

			// Add each player to the player character list..
			// so we can cycle through then.
			PlayerCharacters.Add(player.transform);

			// Turn off each player
			player.GetComponent<Player>().SetPlayerActive(false);
		}

		// Target the first player object found.
		// And turn it on.
		if (PlayerCharacters.Count > 0)
		{
			Target = PlayerCharacters[0];
			TogglePlayer(PlayerCharacters[CharacterIndex], true);
		}
	}

	// Update is called once per frame
	// Move to the "Target"
	void Update()
	{
		// Sawp characters
		if (Input.GetKeyDown(KeyCode.T)) CameraController.Instance.SpawnActiveCharacter();

		if (Target == null) return;

		// TODO:(Nathen) Improve the cameras lerping code

		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Target.position + Offset;

		if (Looking)
		{
			// Smooth Move
			transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * 6.5f);
		}
		else
		{
			// Lerp Move
			transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * 2.0f);
		}
	}

	// Change the state of a player from active to false or vice versa
	public void TogglePlayer(Transform currentPlayer, bool state)
	{
		if (currentPlayer.TryGetComponent<Player>(out Player disable))
		{
			disable.SetPlayerActive(state);
		}
	}

	// Change the character we are controlling
	public void SpawnActiveCharacter()
	{
		// Turn off current player
		TogglePlayer(PlayerCharacters[CharacterIndex], false);

		// Update selected character
		CharacterIndex = (CharacterIndex + 1) % PlayerCharacters.Count;

		// Target and turn on new character
		if (Looking == false)
		{
			Target = PlayerCharacters[CharacterIndex];
		}

		TogglePlayer(PlayerCharacters[CharacterIndex], true);
	}

	// Look at a target in the environment
	// Start a coroutine
	public void LookAtTarget(Transform targets, Vector3 offset, bool pauseOnTarget = true)
	{
		StartCoroutine(coLookAtTarget(new List<Transform> { targets }, offset, pauseOnTarget));
	}
	public void LookAtTarget(List<Transform> targets, Vector3 offset, bool pauseOnTarget = true)
	{
		StartCoroutine(coLookAtTarget(targets, offset, pauseOnTarget));
	}

	private IEnumerator coLookAtTarget(List<Transform> targets, Vector3 offset, bool pauseOnTarget = true)
	{
		// If we are currently looking at something.
		// We wait for that to end.
		while (Looking == true) yield return null;

		Looking = true;

		// Loop through each target
		foreach (Transform target in targets)
		{
			// Set the target the camera will move towards
			Target = target;

			// [WAIT] Till we get to the current target
			while (Vector3.Distance(Target.position + Offset, transform.position) > 0.1f) {
				yield return null;
			}

			// Pause on a target
			if (pauseOnTarget) yield return new WaitForSeconds(0.5f);

			// Call any extra functionality on objects that have an Observable interface
			if (target.TryGetComponent<IObservable>(out IObservable observable)) observable.OnCameraFocus();
		}

		// Look back at the player
		Target = PlayerCharacters[CharacterIndex];
		
		Looking = false;
	}
}
