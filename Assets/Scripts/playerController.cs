using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class playerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource au;
	public float speed, tilt_x, tilt_z;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	// Use this for initialization
	void Start () {
        rb = GetComponent <Rigidbody>();	
		au = GetComponent <AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			au.Play ();
		}
	}
	

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		Vector3 posn = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.position = posn;
		rb.rotation = Quaternion.Euler (rb.velocity.z * tilt_x, 0.0f, rb.velocity.x * -tilt_z);
    }
}
