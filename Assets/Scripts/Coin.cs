using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	private LevelManager t_LevelManager;
	private GameStateManager arrayMov;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		arrayMov = FindObjectOfType<GameStateManager> ();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			
			t_LevelManager.AddCoin ();
			Destroy (gameObject);
			
			arrayMov.playerLevelData.Add("RCoin");
			/*Debug.Log("ENTRO A LA MONEDA");
			foreach (var dato in mario.playerLevelData)
				{
					Debug.Log(dato);
				}*/
			
		}
	}
}
