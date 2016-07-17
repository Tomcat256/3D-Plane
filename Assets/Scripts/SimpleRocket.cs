using UnityEngine;
using System.Collections;

public class RocketCollisionResult {
	public bool DestroyRocket = false;

	public RocketCollisionResult(bool destroyRocket)
	{
		DestroyRocket = destroyRocket;
	}
}


public class SimpleRocket : MonoBehaviour {

	public float Speed;

	public GameObject Controller;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		doMove ();
	}

	void OnCollisionEnter(Collision collision)
	{
		RocketCollisionResult rcr = processCollision (collision.gameObject);
		if (rcr.DestroyRocket) {
			gameObject.SendMessage ("Explode");
		}

	}

	virtual protected RocketCollisionResult processCollision(GameObject target)
	{
		switch (target.tag) {
		case "Treasure":
			Controller.SendMessage ("OnTreasureHit", target);
			return new RocketCollisionResult (true);
		case "Obstacle":
			Controller.SendMessage ("OnObjectRocketHit", target);
			return new RocketCollisionResult (true);
		case "Wall":
			return new RocketCollisionResult (true);
		}

		return new RocketCollisionResult (false);
	}

	virtual protected void doMove()
	{
		transform.position += transform.TransformDirection (Vector3.forward) * Speed;
	}
}
