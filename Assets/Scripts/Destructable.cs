using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public float Scale = 1;

	public void Explode()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		explosion.transform.localScale = Vector3.one * Scale;

		Destroy (gameObject);
	}
}
