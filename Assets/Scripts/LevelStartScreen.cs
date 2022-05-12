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
	public GameStateManager t_GameStateManager;
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
		//Limpia la matriz de datos del jugador
		limpiarMatriz(t_GameStateManager.playerLevelData);
		///FUNCIONES DE AYUDA////

		                     //Activador del facilitador//
		if ((t_GameStateManager.modifMap == 0 && t_GameStateManager.lives == 1))
		{
			int cantidadMuerteCaida = t_GameStateManager.playerLevelData.Where(x=> x.mov == "OUT").Count();
			int cantidadMuerteGoomba = t_GameStateManager.playerLevelData.Where(x=> x.mov == "DEADGreen Goomba(Clone)").Count();
			int timeOut = t_GameStateManager.playerLevelData.Where(x=> x.mov == "TIMEOUT").Count();
			
			//Facilitador por caida
			if (cantidadMuerteCaida >= 2 && cantidadMuerteGoomba<=1)
			{
				facilitadorPorCaida(cantidadMuerteCaida);
				//General:[Bloque bonificacion]
				bloqueBonificacion();
				//General:[Mostrar Vidas Ocultas]
				mostrarVidasOcultas();
			}
			//Facilitador de muerte por enemigo
			else if (cantidadMuerteGoomba>=1 && cantidadMuerteCaida<=1)
			{
				facilitadorPorEnemigo();
				//General:[Bloque bonificacion]
				bloqueBonificacion();
				//General:[Mostrar Vidas Ocultas]
				mostrarVidasOcultas();
			}
			//Facilitador de muerte por falta de tiempo
			else if (timeOut >= 0)
			{
				facilitadorPorTiempo();
				//General:[Bloque bonificacion]
				bloqueBonificacion();
				//General:[Mostrar Vidas Ocultas]
				mostrarVidasOcultas();
			}
			//Facilitador por inconcistencia
			else
			{
				facilitadorPorTiempo();
				facilitadorPorEnemigo();
				facilitadorPorCaida(cantidadMuerteCaida);
				//General:[Bloque bonificacion]
				bloqueBonificacion();
				//General:[Mostrar Vidas Ocultas]
				mostrarVidasOcultas();
			}		

		}else if (t_GameStateManager.modifMap == 2)
		{
			safeZoneDestroyer(t_GameStateManager.playerLevelData);

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

	void limpiarMatriz(List<Coordenadas> listaDatosJugador){
        for (int i = 0; i < (listaDatosJugador).Count; i++)
		{
			if (i <= ((listaDatosJugador).Count)-5)
			{
				if (listaDatosJugador[i].mov == listaDatosJugador[i+1].mov &&
					listaDatosJugador[i].cooX == listaDatosJugador[i+1].cooX &&
					listaDatosJugador[i].cooy == listaDatosJugador[i+1].cooy)
				{
					listaDatosJugador.RemoveAt(i+1);
					//Debug.Log("removio 1");
					

				}else if (	listaDatosJugador[i].mov   == listaDatosJugador[i+2].mov &&
							listaDatosJugador[i+1].mov == listaDatosJugador[i+3].mov &&
							listaDatosJugador[i].cooX   == listaDatosJugador[i+2].cooX &&
							listaDatosJugador[i+1].cooX == listaDatosJugador[i+3].cooX &&
							listaDatosJugador[i].cooy   == listaDatosJugador[i+2].cooy &&
							listaDatosJugador[i+1].cooy == listaDatosJugador[i+3].cooy)
				{
					listaDatosJugador.RemoveAt(i+2);
					listaDatosJugador.RemoveAt(i+3);
					//Debug.Log("removio 2");

					

				}else if (	listaDatosJugador[i].mov   == listaDatosJugador[i+3].mov &&
							listaDatosJugador[i+1].mov == listaDatosJugador[i+4].mov &&
							listaDatosJugador[i+2].mov == listaDatosJugador[i+5].mov &&
							listaDatosJugador[i].cooX   == listaDatosJugador[i+3].cooX &&
							listaDatosJugador[i+1].cooX == listaDatosJugador[i+4].cooX &&
							listaDatosJugador[i+2].cooX == listaDatosJugador[i+5].cooX &&
							listaDatosJugador[i].cooy   == listaDatosJugador[i+3].cooy &&
							listaDatosJugador[i+1].cooy == listaDatosJugador[i+4].cooy &&
							listaDatosJugador[i+2].cooy == listaDatosJugador[i+5].cooy)				{
					listaDatosJugador.RemoveAt(i+3);
					listaDatosJugador.RemoveAt(i+4);
					listaDatosJugador.RemoveAt(i+5);

					//Debug.Log("removio 3");
					
				}
				
			}
		}
    }


//Funciones facilitadoras
	void mostrarVidasOcultas(){
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

	void bloqueBonificacion(){
		t_GameStateManager.editMarioWorld[0,5] = 4;
		t_GameStateManager.modifMap = 1;
	}

	void facilitadorPorCaida(int cantidadMuerteCaida){
		for (int i = 0; i < cantidadMuerteCaida; i++)
		{
			int indice = t_GameStateManager.playerLevelData.FindIndex(x => x.mov == "OUT");
			double elemx = t_GameStateManager.playerLevelData[indice-1].cooX;
			double elemy = t_GameStateManager.playerLevelData[indice-1].cooy;
			t_GameStateManager.editMarioWorld[Convert.ToInt32(elemx),Convert.ToInt32(elemy)] = 2;
			t_GameStateManager.playerLevelData[indice].mov ="IN";	
		}
	}

	void facilitadorPorEnemigo(){
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
						aux = 0;
					}
					else
					{
						aux = 1;
					}
					
				}
			}
		}
	}

	void facilitadorPorTiempo(){
		t_GameStateManager.timeLeft = 600.5f;
		t_GameStateManager.modifMap = 1;
	}

//Funciones dificultadoras
	void safeZoneDestroyer(List<Coordenadas> listaDatosJugador){
		double[,] posicion = new double[5,2]{{0,0},
										{0,0},
										{0,0},
										{0,0},
										{0,0}};
		double fx = 0;
		double fy =0;
		int cont = 0;
		
		int contp1 = 0;
		int contp2 = 0;
		int cantidadBonusEnMapa = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "marioPowerUp").Count());
		Debug.Log(cantidadBonusEnMapa + " Cantidad  bonus en el mapa recogidos");
		for (int i = 0; i < ((listaDatosJugador).Count)-1; i++)
		{


			///////////////////////////////////////////////////////////
			if (contp1 <5)
			{
				if (listaDatosJugador[i].cooX==listaDatosJugador[i+1].cooX)
				//if (listaDatosJugador[i].cooX==listaDatosJugador[i+1].cooX&&listaDatosJugador[i].cooy==listaDatosJugador[i+1].cooy)
				{
					cont++;
					Debug.Log("numero"+ listaDatosJugador[i].cooX);
				}
				else if(cont >=100)
				{
					Debug.Log("contador cuando pasa 100: "+ cont);
					fx = listaDatosJugador[i-1].cooX;
					fy = listaDatosJugador[i-1].cooy;
					posicion[contp1,contp2] = fx;
					posicion[contp1,contp2+1] = fy;
					Debug.Log("fx guardado: "+ fx);
					Debug.Log("fy guardado: "+ fy);
					contp1++;
					fx = listaDatosJugador[i].cooX;
					fy = listaDatosJugador[i].cooy;	
					cont = 0;
				}else
				{
					cont = 0;
				}		
			}	
		}



		for (int i = 0; i < 5; i++)
		{
			int j = 0;
			double elemx = posicion[i,j];
			double elemy = posicion[i,j+1];
			if (elemx != 0)
			{
				t_GameStateManager.editMarioWorld[Convert.ToInt32(elemx),Convert.ToInt32(elemy)] = 21;
			}
			
		}
		
		
		
	}
}
