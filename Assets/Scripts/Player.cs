using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 0;
	public float rotationSpeed = 0;
	public float shieldRechargeInSeconds = 15f;
	public float shieldDurationInSeconds = 5f;
	public GameManager gameManager;

	private float m_shieldRecharge;
	private float m_shieldDuration;

	private bool forward = false;
	private bool left = false;
	private bool right = false;
	private bool shieldEnabled = false;

	private void Start() {
		m_shieldRecharge = shieldRechargeInSeconds;
		m_shieldDuration = shieldDurationInSeconds;
	}

    private void Awake()
    {
		
    }

    private void Update()
    {		
		if (shieldEnabled) {
			m_shieldDuration -= Time.deltaTime;
			if (m_shieldDuration <= 0) {
				shieldEnabled = false;
				m_shieldDuration = shieldDurationInSeconds;
				m_shieldRecharge = shieldRechargeInSeconds;
				Debug.Log("shield deactivated");
			}
		}
		else {
			m_shieldRecharge -= Time.deltaTime;
			if (m_shieldRecharge < 0) {
				m_shieldRecharge = 0;
			}
		}


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

		if (Input.GetKeyDown(KeyCode.Space) && m_shieldRecharge == 0 && !shieldEnabled) {
			shieldEnabled = true;
			Debug.Log("shield activated");
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

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Rocket" && !shieldEnabled) {
			gameManager.MainMenu();
		}
	}
}
