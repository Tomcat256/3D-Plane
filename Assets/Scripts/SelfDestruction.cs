using UnityEngine;
using System.Collections;

public class SelfDestruction : MonoBehaviour {

	public float destructionTimeout = 10f;
	// Use this for initialization
	void Start () {
		StartCoroutine (removeMyself ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator removeMyself()
	{
		yield return new WaitForSeconds (destructionTimeout);
		Destroy (gameObject);
	}
}
