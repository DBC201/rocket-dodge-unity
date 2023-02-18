using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
	private float score;
	private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
		text = this.gameObject.GetComponent<TMP_Text>();
    }

	public void addScore(int x) {
		score += x;
	}

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
		text.text = "Score: " + ((int)score).ToString();
    }
}
