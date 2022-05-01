using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using System.IO;

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
		//Limpia la matriz de datos del jugador
		for (int i = 0; i < (t_GameStateManager.playerLevelData).Count; i++)
		{
			if (i <= ((t_GameStateManager.playerLevelData).Count)-5)
			{
				if (t_GameStateManager.playerLevelData[i] == t_GameStateManager.playerLevelData[i+1])
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+1);

					

				}else if (	t_GameStateManager.playerLevelData[i]   == t_GameStateManager.playerLevelData[i+2] &&
							t_GameStateManager.playerLevelData[i+1] == t_GameStateManager.playerLevelData[i+3])
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+2);
					t_GameStateManager.playerLevelData.RemoveAt(i+3);

					

				}else if (	t_GameStateManager.playerLevelData[i]   == t_GameStateManager.playerLevelData[i+3] &&
							t_GameStateManager.playerLevelData[i+1] == t_GameStateManager.playerLevelData[i+4] &&
							t_GameStateManager.playerLevelData[i+2] == t_GameStateManager.playerLevelData[i+5])
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+3);
					t_GameStateManager.playerLevelData.RemoveAt(i+4);
					t_GameStateManager.playerLevelData.RemoveAt(i+5);

					
				}
				
			}
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
