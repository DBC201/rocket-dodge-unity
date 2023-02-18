using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 0;
	public float rotationSpeed = 0;
	private bool forward = false;
	private bool left = false;
	private bool right = false;

	private void Start() {
	}

    private void Awake()
    {
		
    }

    private void Update()
    {		
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			forward = true;
        }

		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
			forward = false;
		}

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			left = true;
        }

		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
			left = false;
        }
        
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			right = true;
        }

		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
			right = false;
        }

		if (forward) {
            transform.position += transform.forward * speed * Time.deltaTime;
		}

		if (left) {
			transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime * -1);
		}

		if (right) {
			transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
		}
    }
}
