using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Move continuously, flipping direction if hit on the side by non-Player. Optionally
 * bounce up if hit ground while moving.
 * Applicable to: 1UP Mushroom, Big Mushroom, Starman, Goomba, Koopa
 */

public class MoveAndFlip : MonoBehaviour {
	public bool canMove = false;
	public bool canMoveAutomatic = true;
	private float minDistanceToMove = 14f;

	public float directionX = 1;
	public Vector2 Speed = new Vector2 (3, 0);
	private Rigidbody2D m_Rigidbody2D;
	private GameObject mario;
	public GameStateManager t_GameStateManager;
	public int enemigosMuertos;
	// Use this for initialization
	void Start () {
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		mario = FindObjectOfType<Mario> ().gameObject;
		OrientSprite ();
		enemigosMuertos = t_GameStateManager.enemigosKill;
		//Debug.Log(enemigosMuertos + "saaaaaaaaaaaaaaaaaaaaaaaaaaaapeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
	}

									
	void Update() {
		if (!canMove & Mathf.Abs (mario.transform.position.x - transform.position.x) <= minDistanceToMove && canMoveAutomatic) {
			canMove = true;
		}
	}

//	void OnBecameVisible() {
//		if (canMoveAutomatic) {
//			canMove = true;
//		}
//	}

	// Assuming default sprites face right
	
	
	//int numRam2 = 2;
	void OrientSprite() {
		if (directionX > 0) {	
			transform.localScale = new Vector3 (1, 1, 1);
		} else if (directionX < 0) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}

///////Speed.x controla la velocidad de movimiento
	void FixedUpdate () {
		if (canMove) {
			if (t_GameStateManager.modifMap == 2)
			{
				m_Rigidbody2D.velocity = new Vector2(t_GameStateManager.controlVelocidad * directionX, m_Rigidbody2D.velocity.y);
				//Debug.Log(m_Rigidbody2D.velocity);
			}else
			{
				
				m_Rigidbody2D.velocity = new Vector2(Speed.x * directionX, m_Rigidbody2D.velocity.y);
				//Debug.Log(m_Rigidbody2D.velocity);
				
			}
			
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		Vector2 normal = other.contacts[0].normal;
		Vector2 leftSide = new Vector2 (-1f, 0f);
		Vector2 rightSide = new Vector2 (1f, 0f);
		Vector2 bottomSide = new Vector2 (0f, 1f);
		bool sideHit = normal == leftSide || normal == rightSide;
		bool bottomHit = normal == bottomSide;

		if (m_Rigidbody2D.tag == "Enemy") {//Esto pone a saltar a los enemigos
			if (t_GameStateManager.controlVelocidad >= 3&& t_GameStateManager.controlVelocidad <= 4)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			}else if (t_GameStateManager.controlVelocidad > 4 && t_GameStateManager.controlVelocidad <= 6)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 7);
			}else if (t_GameStateManager.controlVelocidad > 6 && t_GameStateManager.controlVelocidad <= 8)
			{
				int numRam = Random.Range(10, 17);
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, numRam);
			}
			
		}
		// reverse direction
		if (other.gameObject.tag != "Player" && sideHit) {
			directionX = -directionX;
			OrientSprite ();
		}else if (other.gameObject.tag.Contains("Platform") && bottomHit && canMove && m_Rigidbody2D.tag != "Enemy" ) {
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Speed.y);
		}
	}

}

