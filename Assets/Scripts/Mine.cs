using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : SimpleRocket {

	public float SplashDamageRadius = 10000;
	public float DetonationRadius = 10;

	public List<string> TargetTags = new List<string>(){"Obstacle", "Treasure"};
	public List<string> SplashTags = new List<string>(){"Obstacle", "Treasure", "Weapon"};

	// Use this for initialization
	override protected void Start () {
		float localDetonationRadius = DetonationRadius / Mathf.Max(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		gameObject.GetComponent<SphereCollider> ().radius = localDetonationRadius ;
		gameObject.GetComponent<Destructable> ().Scale = SplashDamageRadius;

		gameObject.transform.Find ("TriggerSphere").localScale = Vector3.one * 2 * localDetonationRadius ;
		Debug.Log(gameObject.transform.Find ("TriggerSphere").localScale);
		base.Start ();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (TargetTags.Contains (collider.gameObject.tag)) {
			gameObject.SendMessage ("Explode");
		}
	}

	void Explode (){
		splashDamage ();
	}

	private void splashDamage()
	{
		foreach (string tag in SplashTags) {
			GameObject[] targets = GameObject.FindGameObjectsWithTag (tag);
			foreach(GameObject target in targets)
			{
				if (Vector3.Distance (target.transform.position, transform.position) < SplashDamageRadius) {
					processCollision (target);
				}
			}
		}
	}
}
