using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Coin : MonoBehaviour {
	private LevelManager t_LevelManager;
	private GameStateManager arrayMov;
	
	Coordenadas coor;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		arrayMov = FindObjectOfType<GameStateManager> ();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			
			t_LevelManager.AddCoin ();
			Destroy (gameObject);
			
			//coor = new Coordenadas("COIN",Math.Round(transform.position.x),Math.Round(transform.position.y));
			
			//arrayMov.playerLevelData.Add(coor);
			
			//arrayMov.playerLevelData.Add("RCoin"+ Math.Round(transform.position.x) + "|" + Math.Round(transform.position.y));
			/*Debug.Log("ENTRO A LA MONEDA");
			foreach (var dato in mario.playerLevelData)
				{
					Debug.Log(dato);
				}*/
			
		}
	}
}
