using UnityEngine;
using System.Collections;

public class LightScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Light> ().range = gameObject.transform.localScale.x * gameObject.transform.parent.localScale.x;
	}
}
