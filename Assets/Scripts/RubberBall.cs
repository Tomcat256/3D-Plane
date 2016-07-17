using UnityEngine;
using System.Collections;

public class RubberBall : MonoBehaviour {

	public float velocity = 1;
	public bool rotationWhileMoving = false;
	protected Vector3 direction = Vector3.zero;

	// Use this for initialization
	protected void Start () {
		direction = transform.TransformDirection(Vector3.forward);
	}
	
	// Update is called once per frame
	protected void Update () {
		doMove ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Obstacle") {
			invertVelocity (collision);
		}
	}

	void invertVelocity(Collision collision)
	{
		Vector3 meanContactNormals = Vector3.zero;
		foreach (ContactPoint point in collision.contacts) {
			meanContactNormals += point.normal;
		}
		direction = Vector3.Reflect(direction, meanContactNormals).normalized;
	}

	void doMove()
	{
		transform.position += direction * velocity;
		if (rotationWhileMoving) {
			transform.Rotate (Vector3.forward);
		}
	}

}
