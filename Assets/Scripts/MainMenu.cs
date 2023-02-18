using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayNonInfinite() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void PlayInfinite() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

	public void Quit() {
		Application.Quit();
		Debug.Log("Quit called.");
	}
}
