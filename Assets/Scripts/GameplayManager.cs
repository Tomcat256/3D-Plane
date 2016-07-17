using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour {

	public Camera FollowingCamera;


	public GameObject GUI;
	public GameObject PlanePrefab;
	public GameObject ChestPrefab;
	public GameObject WallPrefab;
	public GameObject BorderSpherePrefab;
	public GameObject AstroidPrefab;
	public GameObject ExplosionPrefab;

	public bool UseSphericalBorder = true;

	public uint AreaWidth = 500;
	public uint AreaLength = 500;
	public uint AreaHeight = 300;

	public uint GameDuration = 120;

	public uint NumberOfChests = 10;
	public uint NumberOfAstroids = 20;
	public uint NumberOfMissilesInStack = 10;

	public bool DestroyByWallCollision = true;


	private GameObject plane;
	private uint score = 0;
	private uint timeLeft;

	private bool gameIsOver = false;

	private List<GameObject> chests = new List<GameObject>();
	private List<GameObject> astroids = new List<GameObject>();

	private BoundsProcessorBase boundsProcessor;

	private LevelStatistics stats;

	private Dictionary<KeyCode, string> keyboardMap = new Dictionary<KeyCode, string>(){
		{KeyCode.W,"IncreaseSpeed"},
		{KeyCode.S,"DecreaseSpeed"},
		{KeyCode.UpArrow,"RotateUp"},
		{KeyCode.DownArrow,"RotateDown"},
		{KeyCode.LeftArrow,"RotateLeft"},
		{KeyCode.RightArrow,"RotateRight"},
		{KeyCode.Z,"HandBreak"},
		{KeyCode.Space,"Fire"}
	};

	void Start () {

		if (UseSphericalBorder) {
			boundsProcessor = new BoundsProcessorEllipse (new Vector3 (AreaWidth, AreaHeight, AreaLength));
			boundsProcessor.buildBorders (BorderSpherePrefab);
		} else {
			boundsProcessor = new BoundsProcessorRect (new Vector3 (AreaWidth, AreaHeight, AreaLength));
			boundsProcessor.buildBorders (WallPrefab);
		}

		startRound ();
	}

	private void startRound()
	{
		gameIsOver = false;
		score = 0;
		stats = new LevelStatistics();

		placeChests (NumberOfChests);
		placeAstroids (NumberOfAstroids);
		placePlane ();
		startTimer ();
	}

	private void cleanup()
	{
		foreach (GameObject it in chests) {
			chests.Remove (it);
			Destroy (it);
		}

		foreach (GameObject it in astroids) {
			astroids.Remove (it);
			Destroy (it);
		}

		stats = new LevelStatistics ();

		GUI.SendMessage ("Reset");
	}
	
	void Update () {
		if( gameIsOver )
		{
			if (Input.GetKey (KeyCode.R)) {
				cleanup ();
				startRound ();
			}
			return;
		}
		
		FollowingCamera.SendMessage("FollowTarget", plane);
		//followingCamera.SendMessage("FollowTarget", GameObject.Find("astroid"));
		procesKeyboardInput ();
	}
		
	private void procesKeyboardInput()
	{
		foreach (KeyValuePair<KeyCode, string> item in keyboardMap) {
			if (Input.GetKey (item.Key)) {
				plane.SendMessage(item.Value);
			}
		}
	}

	public void OnTreasureHit(GameObject target)
	{
		chests.Remove (target);
		target.SendMessage ("Explode");
		plane.SendMessage ("addMissiles", NumberOfMissilesInStack);
		increaseScore (stats.ScoresPerCrate);
		stats.ChestsCollected += 1;
	}

	public void OnObstacleHit(GameObject target)
	{
		DestroyedGameOver ();
	}

	public void OnWallHit(GameObject target)
	{
		if (DestroyByWallCollision) {
			DestroyedGameOver ();
		}
	}

	public void OnObjectRocketHit(GameObject target)
	{
		target.SendMessage ("Explode");
		increaseScore (stats.ScoresPerAstroid);
		stats.AstroidsDestroyed += 1;
	}

	private void increaseScore(uint amount)
	{
		score += amount;
		GUI.SendMessage ("SetScore", score);
	}

	private void placePlane()
	{
		plane = (GameObject)Instantiate (PlanePrefab, Vector3.up*AreaHeight/2, Quaternion.identity);
		plane.GetComponent<Plane>().Controller = this.gameObject;
		plane.SendMessage ("addMissiles", NumberOfMissilesInStack);
	}

	private void placeChests(uint chestsNumber)
	{
		for (int i = 0; i < chestsNumber; i++) {
			chests.Add (boundsProcessor.placeObjectRandomly(ChestPrefab, Quaternion.identity));
		}
	}

	private void placeAstroids (uint astroidsNumber)
	{
		for (int i = 0; i < astroidsNumber; i++) {
			GameObject astroid = boundsProcessor.placeObjectRandomly(AstroidPrefab);
			astroid.transform.localScale = new Vector3( Random.Range (15, 20), Random.Range (15, 20), Random.Range (15, 20));
			astroid.GetComponent<RubberBall> ().velocity = Random.Range (0.5f, 1.3f);
			astroids.Add (astroid);
		}
	}

	private void startTimer()
	{
		timeLeft = GameDuration;
		StopCoroutine ("updateTimer");
		StartCoroutine ("updateTimer");
	}

	private IEnumerator updateTimer()
	{
		while (timeLeft > 0) {
			timeLeft--;
			GUI.SendMessage ("SetSecondsLeft", timeLeft);
			yield return new WaitForSeconds (1f);		
		}
		timeOutGameOver ();
	}

	private void timeOutGameOver()
	{
		GUI.SendMessage ("ShowLevelComplete", stats);
		Destroy (plane);
		gameIsOver = true;
	}

	private void DestroyedGameOver()
	{
		GUI.SendMessage ("ShowGameOver");
		plane.SendMessage ("Explode");

		gameIsOver = true;
	}

	public void OnMissilesNumberUpdated(uint number)
	{
		GUI.SendMessage ("SetMissilesLeft", number);
		stats.MissilesLeft = number;
	}


}
