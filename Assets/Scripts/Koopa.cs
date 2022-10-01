using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : Enemy {
	public GameObject KoopaShell;
	private Animator m_Animator;
	private float stompedDuration = 0.3f;
	// Use this for initialization
	void Start () {
		starmanBonus = 200;
		rollingShellBonus = 500;
		hitByBlockBonus = 100; // ???
		fireballBonus = 200;
		stompBonus = 100;
		m_Animator = GetComponent<Animator> ();
	}

	public override void StompedByMario() {
		//isBeingStomped = true;
		//StartCoroutine (SpawnKoopaShellCo ());
		//isBeingStomped = true;
		//StopInteraction ();
		Debug.Log (this.name + " StompedByMario: stopped interaction");
		Rigidbody2D m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		m_Animator.SetTrigger ("flipped");
		m_Rigidbody2D.velocity +=  new Vector2(0, 3);
		gameObject.layer = LayerMask.NameToLayer ("Falling to Kill Plane");
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground Effect";
		//Destroy (gameObject, stompedDuration);
		//isBeingStomped = false;
		
	}

	
	IEnumerator SpawnKoopaShellCo() {
		StopInteraction ();
		Debug.Log (this.name + " SpawnKoopaShellCo: stopped interaction");
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSecondsRealtime(.05f); // prevent immediate damage by shell
		Instantiate (KoopaShell, transform.position, Quaternion.identity);
		Debug.Log (this.name + " SpawnKoopaShellCo: koopa shell spawned");
		Destroy (gameObject);
		isBeingStomped = false;
	}
}
