using UnityEngine;
using System.Collections;

public abstract class BoundsProcessorBase {

	protected Vector3 areaSizes;

	public BoundsProcessorBase(Vector3 sizes)
	{
		areaSizes = sizes;
	}

	abstract public GameObject placeObjectRandomly (Object original, Quaternion rotation);

	public GameObject placeObjectRandomly(Object original)
	{
		Quaternion rndRotation = Quaternion.Euler (new Vector3 (Random.Range(0,360),Random.Range(0,360),Random.Range(0,360)));
		return placeObjectRandomly (original, rndRotation);
	}

	abstract public void buildBorders (GameObject gameObject);
}

public class BoundsProcessorRect : BoundsProcessorBase {

	public BoundsProcessorRect(Vector3 sizes) : base(sizes){}

	override public GameObject placeObjectRandomly(Object original, Quaternion rotation)
	{
		int z = (int)Random.Range (-areaSizes.z/2, areaSizes.z/2);
		int x = (int)Random.Range (-areaSizes.x/2, areaSizes.x/2);
		int y = (int)Random.Range (0, areaSizes.y);

		return (GameObject)MonoBehaviour.Instantiate (original, new Vector3(x,y,z), rotation );
	}

	override public void buildBorders (GameObject prefab)
	{
		//Consider gameObject is a wall prefab
		GameObject wall;
		//Bottom
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(0,0,0), Quaternion.identity);
		wall.transform.localScale = new Vector3(areaSizes.x, 1, areaSizes.z);
		//Top
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(0,areaSizes.y,0), Quaternion.identity);
		wall.transform.localScale = new Vector3(areaSizes.x, 1, areaSizes.z);
		//Front
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(0,areaSizes.y/2,areaSizes.z/2), Quaternion.identity);
		wall.transform.localScale = new Vector3(areaSizes.x, areaSizes.y, 1);
		//Rare
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(0,areaSizes.y/2,-areaSizes.z/2), Quaternion.identity);
		wall.transform.localScale = new Vector3(areaSizes.x, areaSizes.y, 1);
		//Left
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(areaSizes.x/2,areaSizes.y/2,0), Quaternion.identity);
		wall.transform.localScale = new Vector3(1, areaSizes.y, areaSizes.z);
		//Right
		wall = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3(-areaSizes.x/2,areaSizes.y/2,0), Quaternion.identity);
		wall.transform.localScale = new Vector3(1, areaSizes.y, areaSizes.z);

	}
}

public class BoundsProcessorEllipse : BoundsProcessorBase {

	public BoundsProcessorEllipse(Vector3 sizes) : base(sizes){}

	override public GameObject placeObjectRandomly(Object original, Quaternion rotation)
	{

		float fi = Random.Range(0f, 2*Mathf.PI);
		float th = Random.Range(0f, 2*Mathf.PI);
		float r = Random.Range (0f, 1f);

		Vector3 coords = new Vector3 (r*Mathf.Sin(th)*Mathf.Cos(fi)*areaSizes.x, r*Mathf.Sin(th)*Mathf.Cos(fi)*areaSizes.y, r*Mathf.Cos(th)*areaSizes.z);

		return (GameObject)MonoBehaviour.Instantiate (original, coords, rotation );
	}

	override public void buildBorders (GameObject prefab)
	{
		//Consider gameObject is a bounding sphere prefab
		GameObject sphere = (GameObject)MonoBehaviour.Instantiate (prefab, Vector3.zero, Quaternion.identity);
		sphere.transform.localScale = new Vector3 (areaSizes.x, areaSizes.y, areaSizes.z);
	}
}