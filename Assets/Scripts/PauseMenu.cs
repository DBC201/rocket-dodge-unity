using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenuCanvas;
	public GameManager gameManager;
	bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused) {
				Play();
			}
			else {
				Stop();
			}
		}
    }

	void Stop() {
		pauseMenuCanvas.SetActive(true);
		Time.timeScale = 0f;
		paused = true;
	}

	public void Play() {
		pauseMenuCanvas.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
	}

	public void MainMenu() {
		gameManager.MainMenu();
	}
}
