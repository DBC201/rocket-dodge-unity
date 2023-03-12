using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
	public GameManager gameManager;
	public TMP_Text scoreText;

    public void PlayAgain() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu() {
		gameManager.MainMenu();
	}

	public void SetScore(float score) {
		scoreText.text = "Score: " + ((int)score).ToString();
	}
}
