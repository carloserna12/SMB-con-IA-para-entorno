using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] 
public class DetectorPlayer : MonoBehaviour
{
    private GameStateManager t_GameStateManager;
    public Coordenadas coor;
    // Start is called before the first frame update
    void Start()
    {
        t_GameStateManager = FindObjectOfType<GameStateManager> ();
       
        
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			coor = new Coordenadas("caminoSuperior",0,0);
           t_GameStateManager.playerLevelData.Add(coor);

            
            if(gameObject.activeSelf){
                gameObject.SetActive(false);
            }
		}
	}


}
