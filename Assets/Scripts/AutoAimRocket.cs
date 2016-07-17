using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoAimRocket : SimpleRocket {

	public List<string> TargetTags = new List<string>(){"Obstacle", "Treasure"};
	public float AutoAimRadius = 10;

	override protected void doMove()
	{
		GameObject target = findNearestTarget ();
		if ( target != null) {
			correctDirection (target);
		}

		base.doMove();
	}

	private void correctDirection(GameObject target)
	{
		transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);
	}


	private GameObject findNearestTarget()
	{
		GameObject target = null;
		float targetDistance = float.MaxValue;

		foreach (string tag in TargetTags) {
			GameObject[] targets = GameObject.FindGameObjectsWithTag (tag);
			foreach (GameObject potentialTarget in targets) {
				float distance = Vector3.Distance (transform.position, potentialTarget.transform.position);
				if( (distance < AutoAimRadius) && (distance < targetDistance) )
				{
					targetDistance = distance;
					target = potentialTarget;
				}
			}
		}
		return target;
	}
}
