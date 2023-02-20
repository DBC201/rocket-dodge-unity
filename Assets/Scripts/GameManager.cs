using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float spawnInterval = 0;
	public float spawnIntervalDecrease = 1; // bigger means game faster quicker (> 1)
	public float spawnIntervalDecreaseIncrease = 1; // smaller means game gets slowly faster (< 1)
	public Player player;
	public GameObject rocketPrefab;
	public float rocketSpeed = 0;
	public float rocketRotationSpeed = 0;
	public Score score;
	public bool isInfinite = false;
	public ParticleSystem explosionPrefab;

	private float m_interval;
	private Vector3 m_origin;

	private float leftCameraAxis;
	private float rightCameraAxis;
	private float topCameraAxis;
	private float bottomCameraAxis;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnInterval == 0) {
			spawnInterval = 1;
		}
		m_interval = spawnInterval;

		m_origin = player.transform.position;
		
		if (!isInfinite) {
			setAxes();
		}
	}

	void setAxes() {
		Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		Vector3[] corners = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Plane plane = frustumPlanes[i];
            float distance;
            plane.Raycast(new Ray(Vector3.zero, Vector3.right), out distance);
            float x = distance;
            plane.Raycast(new Ray(Vector3.zero, Vector3.up), out distance);
            float y = distance;
            plane.Raycast(new Ray(Vector3.zero, Vector3.forward), out distance);
            float z = distance;
            corners[i] = new Vector3(x, y, z);
			//Debug.Log(corners[i]);
        }
		leftCameraAxis = corners[0][0];
		rightCameraAxis = corners[1][0];
		topCameraAxis = corners[3][2];
		bottomCameraAxis = corners[2][2];
    }

	public void SpawnRocket() {
		int edge = Random.Range(0, 3);
		float x,y,z;
		y = player.transform.position.y;
		float relative_x = isInfinite ? player.transform.position.x : m_origin.x;
		float relative_z = isInfinite ? player.transform.position.z : m_origin.z;
		if (edge == 0) { // up
			x = Random.Range(relative_x + leftCameraAxis, relative_x + rightCameraAxis);
			z = topCameraAxis + relative_z;
		}
		else if (edge == 1) { // left 
			x = leftCameraAxis + relative_x;
			z = Random.Range(bottomCameraAxis + relative_z, topCameraAxis + relative_z);
		}
		else if (edge == 2) { // right
			x = rightCameraAxis;
			z = Random.Range(bottomCameraAxis + relative_z, topCameraAxis + relative_z);
		}
		else { // down
			x = Random.Range(relative_x + leftCameraAxis, relative_x + rightCameraAxis);
			z = bottomCameraAxis + relative_z;
		}
		GameObject rocketGameObject = Instantiate(rocketPrefab, new Vector3(x, y, z), Quaternion.identity);
		rocketGameObject.GetComponent<Rocket>().player = player;
		rocketGameObject.GetComponent<Rocket>().speed = rocketSpeed;
		rocketGameObject.GetComponent<Rocket>().rotationSpeed = rocketRotationSpeed;
		rocketGameObject.GetComponent<Rocket>().score = score;
		rocketGameObject.GetComponent<Rocket>().gameManager = this;
		rocketGameObject.GetComponent<Rocket>().explosionPrefab = explosionPrefab;
	}

    // Update is called once per frame
    void Update()
    {
		m_interval -= Time.deltaTime;
		if (m_interval <= 0) {
			//Debug.Log("rocket spawned");
			if (isInfinite) {
				setAxes();
			}

			m_interval = spawnInterval/spawnIntervalDecrease;
			
			if (spawnIntervalDecrease > 1) {
				spawnIntervalDecrease *= spawnIntervalDecreaseIncrease;
			}
			
			if (spawnIntervalDecrease < 1) {
				spawnIntervalDecrease = 1;
			}
			SpawnRocket();
		}
		if (!isInfinite) {
			KeepInCamera(player.gameObject);
		}
    }

	public void MainMenu() {
		if (isInfinite) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
		}
		else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}

	public void KeepInCamera(GameObject gameObject) {
		if (gameObject.transform.position.x >= rightCameraAxis) {
			gameObject.transform.position = new Vector3(rightCameraAxis, gameObject.transform.position.y, gameObject.transform.position.z);
		}

		if (gameObject.transform.position.x <= leftCameraAxis) {
			gameObject.transform.position = new Vector3(leftCameraAxis, gameObject.transform.position.y, gameObject.transform.position.z);
		}

		if (gameObject.transform.position.z >= topCameraAxis) {
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, topCameraAxis);
		}

		if (gameObject.transform.position.z <= bottomCameraAxis) {
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, bottomCameraAxis);
		}
	}
}
