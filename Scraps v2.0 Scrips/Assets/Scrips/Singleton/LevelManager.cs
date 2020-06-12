using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	// Singleton
	public static LevelManager Instance;

	// Public
	public float FadeSpeed = 5;

	// Private
	private CanvasGroup loadingScreen;
	private CanvasGroup loadingBarUI;
	private Slider loadingBar;

	private List<AsyncOperation> currentlyLoading = new List<AsyncOperation>();

	private bool loading;
	private int current;

	// Setup Singleton
	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		// Find the loading screen
		// BUG:(Nathen) For some reason (In Editor) If you have the persistent scene selected this wont find the Loading screen
		// NOTE:(Nathen) This works fine if the selected scene in the one with the loading screen
		loadingScreen = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();
		loadingBar = loadingScreen.GetComponentInChildren<Slider>();
		loadingBarUI = loadingBar.GetComponent<CanvasGroup>();

		// BUILD:(Nathen) This is for when we build the game
		// SceneManager.LoadSceneAsync((int)SceneIndex.MAIN_MENU, LoadSceneMode.Additive);
		// current = (int)SceneIndex.MAIN_MENU;

		// Get the current scene
		// NOTE:(Nathen) This checks if we started the game from a scene that wassent the persistent scene
		// BUILD:(Nathen) Remove this when we build
		if (SceneManager.GetActiveScene().buildIndex != (int)SceneIndex.MANAGER)
		{
			// We set the current index to the current scene so we can unload it later
			current = SceneManager.GetActiveScene().buildIndex;
		}
	}
	private void Update()
	{
		// Fade IN and OUT the loading screen
		if (loading)
		{
			loadingScreen.alpha += Time.deltaTime * FadeSpeed;

			if (loadingScreen.alpha == 1) {
				loadingScreen.GetComponent<Image>().raycastTarget = true;
				loadingBarUI.alpha += Time.deltaTime * FadeSpeed / 5;
			}
		}
		else
		{
			loadingBarUI.alpha -= Time.deltaTime * FadeSpeed / 5;

			if (loadingBarUI.alpha == 0) {
				loadingScreen.GetComponent<Image>().raycastTarget = false;
				loadingScreen.alpha -= Time.deltaTime * FadeSpeed / 5;
			}
		}
	}

	public void LoadLevel(SceneIndex toLoad)
	{
		StartCoroutine(coLoadLevel(toLoad));
	}
	IEnumerator coLoadLevel(SceneIndex toLoad)
	{
		loading = true;

		// Wait for the loading screen to fade in
		yield return new WaitForSecondsRealtime(1.0f);

		// TODO:(Nathen) Add a loading bar..

		// Unload the last scene if we are not the manager scene
		if (current != (int)SceneIndex.MANAGER) {
			currentlyLoading.Add(SceneManager.UnloadSceneAsync(current));
		}

		currentlyLoading.Add(SceneManager.LoadSceneAsync((int)toLoad, LoadSceneMode.Additive));
		current = (int)toLoad;

		float percent;

		// Update the progress bar
		// For each scene we are loading / unloading
		for (int i = 0; i < currentlyLoading.Count; i++)
		{
			while (currentlyLoading[i].isDone == false)
			{
				percent = 0;

				// Add the completion percent of the current operation (scene being loaded / unloaded)
				foreach (AsyncOperation operation in currentlyLoading) {
					percent += operation.progress;
				}

				// Turn the into a percent
				percent = (percent / currentlyLoading.Count) * 100.0f;

				// Apply it to the loading bar
				loadingBar.value = Mathf.Round(percent);

				yield return null;
			}
		}

		yield return new WaitForSecondsRealtime(1.5f);

		loading = false;
	}
}
