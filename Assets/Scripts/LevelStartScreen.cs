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
	//public MapMaker maper;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		//maper = FindObjectOfType<MapMaker>();
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		string worldName = t_GameStateManager.sceneToLoad;
		Debug.Log(t_GameStateManager.editMarioWorld);
		
		WorldTextHUD.text = Regex.Split (worldName, "World ")[1];
		ScoreTextHUD.text = t_GameStateManager.scores.ToString ("D6");
		CoinTextHUD.text = "x" + t_GameStateManager.coins.ToString ("D2");
		WorldTextMain.text = worldName.ToUpper ();
		livesText.text = t_GameStateManager.lives.ToString ();

		StartCoroutine (LoadSceneDelayCo (t_GameStateManager.sceneToLoad, loadScreenDelay));

		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);

		////////////////
		////testeos////
		//////////////
	//	Debug.Log((t_GameStateManager.playerLevelData).Count);

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
				//	Debug.Log("removio 1");
					

				}else if (	t_GameStateManager.playerLevelData[i].mov   == t_GameStateManager.playerLevelData[i+2].mov &&
							t_GameStateManager.playerLevelData[i+1].mov == t_GameStateManager.playerLevelData[i+3].mov &&
							t_GameStateManager.playerLevelData[i].cooX   == t_GameStateManager.playerLevelData[i+2].cooX &&
							t_GameStateManager.playerLevelData[i+1].cooX == t_GameStateManager.playerLevelData[i+3].cooX &&
							t_GameStateManager.playerLevelData[i].cooy   == t_GameStateManager.playerLevelData[i+2].cooy &&
							t_GameStateManager.playerLevelData[i+1].cooy == t_GameStateManager.playerLevelData[i+3].cooy)
				{
					t_GameStateManager.playerLevelData.RemoveAt(i+2);
					t_GameStateManager.playerLevelData.RemoveAt(i+3);
				//	Debug.Log("removio 2");

					

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

				//	Debug.Log("removio 3");
				}
				
			}
		}
		
		///FUNCIONES DE AYUDA////

		                     //Activador del facilitador//
		if ((t_GameStateManager.modifMap == 0 && t_GameStateManager.lives == 1))
		{
			int cantidadMuerteCaida = t_GameStateManager.playerLevelData.Where(x=> x.mov == "OUT").Count();
			int cantidadMuerteGoomba = t_GameStateManager.playerLevelData.Where(x=> x.mov == "DEADGreen Goomba(Clone)").Count();
			int timeOut = t_GameStateManager.playerLevelData.Where(x=> x.mov == "TIMEOUT").Count();
			
			//Facilitador por caida
			if (cantidadMuerteCaida >= 2 && cantidadMuerteGoomba<2)
			{
				for (int i = 0; i < cantidadMuerteCaida; i++)
				{
					int indice = t_GameStateManager.playerLevelData.FindIndex(x => x.mov == "OUT");
					double elemx = t_GameStateManager.playerLevelData[indice-1].cooX;
					double elemy = t_GameStateManager.playerLevelData[indice-1].cooy;
					t_GameStateManager.editMarioWorld[Convert.ToInt32(elemx),Convert.ToInt32(elemy)] = 2;
					t_GameStateManager.playerLevelData[indice].mov ="IN";	
				}
				//General:[Bloque bonificacion]
				t_GameStateManager.editMarioWorld[0,5] = 4;
				t_GameStateManager.modifMap = 1;

				//General:[Mostrar Vidas Ocultas]
				for (int i = 0; i < 185; i++)
        		{
					for (int j = 0; j < 14; j++)
					{
						if(t_GameStateManager.editMarioWorld[i,j] == 23)
						{
							t_GameStateManager.editMarioWorld[i,j]= 5;
						}
					}
				}
			}
			//Facilitador de muerte por enemigo
			else if (cantidadMuerteGoomba>=2 && cantidadMuerteCaida<2)
			{
				int aux = 1;
				for (int i = 0; i < 185; i++)
        		{
					for (int j = 0; j < 14; j++)
					{
						if(t_GameStateManager.editMarioWorld[i,j] == 12)
						{
							if (aux == 1)
							{
								t_GameStateManager.editMarioWorld[i,j] = 0;
								Debug.Log(t_GameStateManager.editMarioWorld[i,j] + "CAMBIE MATRIZ");
								aux = 0;
							}else
							{
								aux = 1;
							}
							
						}
					}
				}
				//General:[Bloque bonificacion]
				t_GameStateManager.editMarioWorld[0,5] = 4;
				t_GameStateManager.modifMap = 1;

				//General:[Mostrar Vidas Ocultas]
				for (int i = 0; i < 185; i++)
        		{
					for (int j = 0; j < 14; j++)
					{
						if(t_GameStateManager.editMarioWorld[i,j] == 23)
						{
							t_GameStateManager.editMarioWorld[i,j]= 5;
						}
					}
				}
			}
			//Facilitador de muerte por falta de tiempo
			else if (timeOut >= 0)
			{
				t_GameStateManager.timeLeft = 600.5f;
				t_GameStateManager.modifMap = 1;
				//General:[Bloque bonificacion]
				t_GameStateManager.editMarioWorld[0,5] = 4;

				//General:[Mostrar Vidas Ocultas]
				for (int i = 0; i < 185; i++)
        		{
					for (int j = 0; j < 14; j++)
					{
						if(t_GameStateManager.editMarioWorld[i,j] == 23)
						{
							t_GameStateManager.editMarioWorld[i,j]= 5;
						}
					}
				}
			}		

		}else if (t_GameStateManager.modifMap == 1)
		{
			Debug.Log("Genial mijo");
		}
		
		
		//Debug.Log(maper.editMarioWorld);
		
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
