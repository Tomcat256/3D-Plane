using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	public float Speed = 0.1f;
	public float AngularAcceleration = 0.1f;
	public float Acceleration = 0.001f;
	public float MaxSpeed = 0.3f;

	public GameObject Controller;
	public GameObject ExplosionPrefab;

	public GameObject DefaultRocketPrefab;

	private bool rocketReady = true;
	private float rocketCooldown = 0.3f;
	private uint missilesLeft = 0;

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		doMove ();
	}

	void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag) {
		case "Treasure":
			Controller.SendMessage ("OnTreasureHit", collision.gameObject);
			break;
		case "Obstacle":
			Controller.SendMessage ("OnObstacleHit", collision.gameObject);
			break;
		case "Wall":
			Controller.SendMessage ("OnWallHit", collision.gameObject);
			break;
		}
	}


	public void IncreaseSpeed()	{
		Speed += Acceleration;
		if (Speed > MaxSpeed) {
			Speed = MaxSpeed;
		}
	}

	public void DecreaseSpeed()	{
		Speed -= Acceleration;
		if (Speed < 0) {
			Speed = 0;
		}
	}

	public void HandBreak()	{
		Speed = 0;
	}

	public void RotateLeft ()
	{
		transform.Rotate (Vector3.forward*AngularAcceleration);
	}

	public void RotateRight ()
	{
		transform.Rotate (-Vector3.forward*AngularAcceleration);
	}

	public void RotateUp ()
	{
		transform.Rotate (-Vector3.left*AngularAcceleration);
	}

	public void RotateDown ()
	{
		transform.Rotate (Vector3.left*AngularAcceleration);
	}

	public void Fire()
	{
		if (!rocketReady)
			return;

		if (missilesLeft == 0)
			return;
		
		GameObject rocket = (GameObject)Instantiate (DefaultRocketPrefab, transform.position, transform.rotation);
		rocket.GetComponent<SimpleRocket>().Controller = this.Controller;
		removeMissiles (1);

		rocketReady = false;
		StartCoroutine ("rechargeRocket");
	}

	private IEnumerator rechargeRocket()
	{
		yield return new WaitForSeconds (rocketCooldown);
		rocketReady = true;
	}

	public void addMissiles(uint missilesNumber)
	{
		missilesLeft += missilesNumber;
		Controller.SendMessage ("OnMissilesNumberUpdated", missilesLeft);
	}

	public void removeMissiles(uint missilesNumber)
	{
		missilesLeft -= missilesNumber;
		if (missilesLeft < 0)
			missilesLeft = 0;
		Controller.SendMessage ("OnMissilesNumberUpdated", missilesLeft);
	}

	protected void doMove(){
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		transform.position += direction * Speed;
	}
		
}
