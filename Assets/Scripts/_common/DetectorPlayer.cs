using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] 
public class DetectorPlayer : MonoBehaviour
{
    private GameStateManager arrayMov;
    // Start is called before the first frame update
    void Start()
    {
        arrayMov = FindObjectOfType<GameStateManager> ();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			
           //arrayMov.playerLevelData.Add(gameObject.name);

            
            if(gameObject.activeSelf){
                gameObject.SetActive(false);
            }
		}
	}


}
