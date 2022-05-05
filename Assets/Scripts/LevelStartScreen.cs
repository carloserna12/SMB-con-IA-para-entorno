using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Linq;

public class LevelStartScreen : MonoBehaviour {
	private GameStateManager t_GameStateManager;
	private float loadScreenDelay = 2;

	public Text WorldTextHUD;
	public Text ScoreTextHUD;
	public Text CoinTextHUD;
	public Text WorldTextMain;
	public Text livesText;
	public List<string> updateData = new List<string>();
	public string jsonSave;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		string worldName = t_GameStateManager.sceneToLoad;

		WorldTextHUD.text = Regex.Split (worldName, "World ")[1];
		ScoreTextHUD.text = t_GameStateManager.scores.ToString ("D6");
		CoinTextHUD.text = "x" + t_GameStateManager.coins.ToString ("D2");
		WorldTextMain.text = worldName.ToUpper ();
		livesText.text = t_GameStateManager.lives.ToString ();

		StartCoroutine (LoadSceneDelayCo (t_GameStateManager.sceneToLoad, loadScreenDelay));

		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);

		
		////testeos////
		Debug.Log((t_GameStateManager.playerLevelData).Count);
		//Limpia la matriz de datos del jugador
		for (int i = 0; i < (t_GameStateManager.playerLevelData).Count; i++)
		{
			if (i <= ((t_GameStateManager.playerLevelData).Count)-5)
			{
				if (t_GameStateManager.playerLevelData[i].mov == t_GameStateManager.playerLevelData[i+1].mov &&
					t_GameStateManager.playerLevelData[i].cooX == t_GameStateManager.playerLevelData[i+1].cooX &&
					t_GameStateManager.playerLevelData[i].cooy == t_GameStateManager.playerLevelData[i+1].cooy)
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+1);
					Debug.Log("removio 1");
					

				}else if (	t_GameStateManager.playerLevelData[i].mov   == t_GameStateManager.playerLevelData[i+2].mov &&
							t_GameStateManager.playerLevelData[i+1].mov == t_GameStateManager.playerLevelData[i+3].mov &&
							t_GameStateManager.playerLevelData[i].cooX   == t_GameStateManager.playerLevelData[i+2].cooX &&
							t_GameStateManager.playerLevelData[i+1].cooX == t_GameStateManager.playerLevelData[i+3].cooX &&
							t_GameStateManager.playerLevelData[i].cooy   == t_GameStateManager.playerLevelData[i+2].cooy &&
							t_GameStateManager.playerLevelData[i+1].cooy == t_GameStateManager.playerLevelData[i+3].cooy)
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+2);
					t_GameStateManager.playerLevelData.RemoveAt(i+3);
					Debug.Log("removio 2");

					

				}else if (	t_GameStateManager.playerLevelData[i].mov   == t_GameStateManager.playerLevelData[i+3].mov &&
							t_GameStateManager.playerLevelData[i+1].mov == t_GameStateManager.playerLevelData[i+4].mov &&
							t_GameStateManager.playerLevelData[i+2].mov == t_GameStateManager.playerLevelData[i+5].mov &&
							t_GameStateManager.playerLevelData[i].cooX   == t_GameStateManager.playerLevelData[i+3].cooX &&
							t_GameStateManager.playerLevelData[i+1].cooX == t_GameStateManager.playerLevelData[i+4].cooX &&
							t_GameStateManager.playerLevelData[i+2].cooX == t_GameStateManager.playerLevelData[i+5].cooX &&
							t_GameStateManager.playerLevelData[i].cooy   == t_GameStateManager.playerLevelData[i+3].cooy &&
							t_GameStateManager.playerLevelData[i+1].cooy == t_GameStateManager.playerLevelData[i+4].cooy &&
							t_GameStateManager.playerLevelData[i+2].cooy == t_GameStateManager.playerLevelData[i+5].cooy)				{
					t_GameStateManager.playerLevelData.RemoveAt(i+3);
					t_GameStateManager.playerLevelData.RemoveAt(i+4);
					t_GameStateManager.playerLevelData.RemoveAt(i+5);

					Debug.Log("removio 3");
				}
				
			}
		}
		
		//Listar elementos únicos: agrupando coincidencias y contando los elementos por grupo
		foreach (var item in (t_GameStateManager.playerLevelData).GroupBy(x => x))
		{
			Debug.Log($"{item.Key.cooX} encontrado {item.Count()} veces");
			
		}
			

		
		//Convertir en JSON la lista y guardarla en un .txt
	
		jsonSave = JsonUtility.ToJson(t_GameStateManager);
		string path = Path.Combine(Application.persistentDataPath, "playerData.data");
		File.WriteAllText(path,jsonSave);
		Debug.Log(path);
	}


	
	IEnumerator LoadSceneDelayCo(string sceneName, float delay) {
		yield return new WaitForSecondsRealtime (delay);
		SceneManager.LoadScene (sceneName);
		
	}
}
