using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillPlane : MonoBehaviour {
	private LevelManager t_LevelManager;
	public Coordenadas coor;
	public GameStateManager ArrayMov;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		ArrayMov = FindObjectOfType<GameStateManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			coor = new Coordenadas("OUT",Math.Round(transform.position.x),Math.Round(transform.position.y));
			ArrayMov.playerLevelData.Add(coor);
			t_LevelManager.MarioRespawn ();
		} else {
			Destroy (other.gameObject);
		}
	}
}
