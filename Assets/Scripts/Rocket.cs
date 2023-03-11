using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	public float speed = 0;
	public float rotationSpeed = 0;
	public Player player;
	public Score score;
	public ParticleSystem explosionPrefab;
	public AudioClip explosionAudio;
	public bool autoExplode = false;
	public float explodeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(autoExplode) {
			if (explodeTime < 0) {
				AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
			explodeTime -= Time.deltaTime;
		}
    	Vector3 direction = player.transform.position - transform.position;
    	Quaternion rotation = Quaternion.LookRotation(direction);
    	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

	void OnCollisionEnter(Collision collision)
    {
		AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
        if (collision.gameObject.tag == "Rocket")
        {
            Destroy(collision.gameObject);
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			score.addScore(10);
			Destroy(this.gameObject);
        }
		else if (collision.gameObject.tag == "Player") {
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			score.addScore(10);
			Destroy(this.gameObject);
		}
    }
}
