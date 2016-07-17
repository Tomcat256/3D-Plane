using UnityEngine;
using System.Collections;

public class AutoRotationCamera : MonoBehaviour {
	// Use this for initialization
	public float ChaseDistance = 20f;

	void Start () {

	}
		
	public void FollowTarget(GameObject target){
		Vector3 targetDirection = (target.transform.position - transform.position);
		transform.rotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		if (targetDirection.magnitude > ChaseDistance) {
			transform.position = target.transform.position - targetDirection.normalized * ChaseDistance;
		}
	}
}
