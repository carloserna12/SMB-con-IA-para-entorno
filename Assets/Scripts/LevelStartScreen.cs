using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Linq;
using Random = System.Random;

public class LevelStartScreen : MonoBehaviour {
	public GameStateManager t_GameStateManager;
	private float loadScreenDelay = 2;

	public Text WorldTextHUD;
	public Text ScoreTextHUD;
	public Text CoinTextHUD;
	public Text WorldTextMain;
	public Text livesText;
	public Text dificultadEnemy;
	public Text cantidadBonificadoresTomados;
	public Text enemigosEliminados;
	//public Text obstaculosGenerados;
	public Text enemigosEspecialesEliminados;
	public Text caminoInferiorTomado;
	public Text caminoSuperiorTomado;
	public List<string> updateData = new List<string>();
	public string jsonSave;

	public LevelManager t_LevelManager;
	
	//public MapMaker maper;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		//maper = FindObjectOfType<MapMaker>();
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		string worldName = t_GameStateManager.sceneToLoad;
		//Debug.Log(t_GameStateManager.editMarioWorld);
		
		WorldTextHUD.text = Regex.Split (worldName, "World ")[1];
		ScoreTextHUD.text = t_GameStateManager.scores.ToString ("D6");
		CoinTextHUD.text = "x" + t_GameStateManager.coins.ToString ("D2");
		WorldTextMain.text = worldName.ToUpper ();
		livesText.text = t_GameStateManager.lives.ToString ();
		dificultadEnemy.text = t_GameStateManager.controlVelocidad.ToString();

		StartCoroutine (LoadSceneDelayCo (t_GameStateManager.sceneToLoad, loadScreenDelay));

		Debug.Log (this.name + " Start: current scene is " + SceneManager.GetActiveScene ().name);

		////////////////
		////testeos////
		//////////////
		//Limpia la matriz de datos del jugador
		limpiarMatriz(t_GameStateManager.playerLevelData);
		
		///FUNCIONES DE AYUDA////       
		if ((t_GameStateManager.modifMap == 0 && t_GameStateManager.lives == 1))//Activador del facilitador
		{
			int cantidadMuerteCaida = t_GameStateManager.playerLevelData.Where(x=> x.mov == "OUT").Count();//Variable busca y guarda # de veces que jugador muere por caida
			int cantidadMuerteGoomba = t_GameStateManager.playerLevelData.Where(x=> x.mov == "DEADGreen Goomba(Clone)").Count();//Variable busca y guarda # de veces que jugador muere por Goomba
			int timeOut = t_GameStateManager.playerLevelData.Where(x=> x.mov == "TIMEOUT").Count();//Variable busca y guarda si jugador muere por tiempo
			
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

		}
		//FUNCIONES DIFICULTADORES
		else if (t_GameStateManager.modifMap == 2)//Activador de dificultadores
		{
			
			//Debug.Log(t_GameStateManager.timeLeftSavePerLevel+ "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
			int cantidadBonusRecolectados = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "marioPowerUp").Count());//variable que cuenta la cantidad de bonificadores que el jugador uso
			int cantidadEnemigosMatados = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "enemyDead").Count());
			int cantidadEnemigosEspecialesMatados = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "enemyEspecialDead").Count());
			int cantidadBonusEnMapa = buscarElementos(4);
			int cantidadMonedasEnMapa = buscarElementos(3);
			int cantidadGoombasMapa = buscarElementos(12);
			int cantidadKoopasMapa = buscarElementos(10);
			int cantidadKoopasVoladoresMapa = buscarElementos(11);
			int cantidadEnemigosTotal = cantidadGoombasMapa + cantidadKoopasMapa + cantidadKoopasVoladoresMapa;
			int caminoSuperiorDetectado = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "caminoSuperior").Count());
			int caminoInferiorDetectado = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "caminoInferior").Count());
			int cantidadMonedasRecolectados = (t_GameStateManager.playerLevelData.Where(x=> x.mov == "COIN").Count());
			limpiadorPorIteracion("marioPowerUp","marioPowerUpDone");
			limpiadorPorIteracion("enemyDead","enemyDeadDone");
			limpiadorPorIteracion("COIN","COINDone");
			limpiadorPorIteracion("enemyEspecialDead","enemyEspecialDeadDone");
			limpiadorPorIteracion("caminoSuperior","caminoSuperiorDone");
			limpiadorPorIteracion("caminoInferior","caminoInferiorDone");

			//INTERFAZ GRAFICA VARIABLES//
			cantidadBonificadoresTomados.text = (cantidadBonusRecolectados + cantidadMonedasRecolectados).ToString();
			enemigosEliminados.text = (cantidadEnemigosMatados).ToString();
			enemigosEspecialesEliminados.text = (cantidadEnemigosEspecialesMatados).ToString();
			caminoInferiorTomado.text = (caminoInferiorDetectado).ToString();
			caminoSuperiorTomado.text = (caminoSuperiorDetectado).ToString();
			//
			if (cantidadBonusRecolectados == cantidadBonusEnMapa || cantidadMonedasRecolectados == cantidadMonedasEnMapa)
			{
				Debug.Log("Entro en aleatorizar");
				aleatorizarBonusMap(cantidadBonusEnMapa,cantidadMonedasEnMapa,cantidadBonusRecolectados,cantidadMonedasRecolectados);
			}else if (cantidadBonusRecolectados != 0 || cantidadMonedasRecolectados != 0)
			{
				aleatorizarBonusMap(cantidadBonusEnMapa,cantidadMonedasEnMapa,cantidadBonusRecolectados,cantidadMonedasRecolectados);
			}
			safeZoneDestroyer(t_GameStateManager.playerLevelData);//Llama la funcion safeZoneDestroyer
			
			rageEnemy(cantidadEnemigosTotal,cantidadEnemigosMatados);

			tiempoPorNivel(t_GameStateManager.timeLeftSave, t_GameStateManager.timeLeftSavePerLevel);

			if (cantidadEnemigosEspecialesMatados != 0)
			{
				enemigosEspeciales(t_GameStateManager.playerLevelData);
			}

			if (caminoSuperiorDetectado != 0 && caminoInferiorDetectado == 0)
			{
				caminoTomado(caminoSuperiorDetectado);
			}else if (caminoSuperiorDetectado == 0 && caminoInferiorDetectado != 0)
			{
				caminoTomadoInferior(caminoInferiorDetectado);
			}else if (caminoSuperiorDetectado != 0 && caminoInferiorDetectado != 0){
				caminoTomado(caminoSuperiorDetectado);
				caminoTomadoInferior(caminoInferiorDetectado);
			}
			

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

	void limpiarMatriz(List<Coordenadas> listaDatosJugador){//Funcion que limpia la lista de informacion
        for (int i = 0; i < (listaDatosJugador).Count; i++)//Recorre la lista
		{
			if (i <= ((listaDatosJugador).Count)-5)//Resta 5 para evitar desbordamiento **"arreglar"
			{
				if (listaDatosJugador[i].mov == listaDatosJugador[i+1].mov &&//Elimina similitudes de 1 vs 1
					listaDatosJugador[i].cooX == listaDatosJugador[i+1].cooX &&
					listaDatosJugador[i].cooy == listaDatosJugador[i+1].cooy)
				{
					listaDatosJugador.RemoveAt(i+1);
					//Debug.Log("removio 1");
					

				}else if (	listaDatosJugador[i].mov   == listaDatosJugador[i+2].mov &&//Elimina similitudes de 2 vs 2
							listaDatosJugador[i+1].mov == listaDatosJugador[i+3].mov &&
							listaDatosJugador[i].cooX   == listaDatosJugador[i+2].cooX &&
							listaDatosJugador[i+1].cooX == listaDatosJugador[i+3].cooX &&
							listaDatosJugador[i].cooy   == listaDatosJugador[i+2].cooy &&
							listaDatosJugador[i+1].cooy == listaDatosJugador[i+3].cooy)
				{
					listaDatosJugador.RemoveAt(i+2);
					listaDatosJugador.RemoveAt(i+3);
					//Debug.Log("removio 2");

					

				}else if (	listaDatosJugador[i].mov   == listaDatosJugador[i+3].mov &&//Elimina similitudes de 3 vs 3
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
					
				}else
				{
					//Debug.Log("No se pudo limpiar mas");
				}
				
			}
		}
    }


//Funciones facilitadoras
	void mostrarVidasOcultas(){ //Funcion que recorre el mundo y cambia vidas ocultas por vidas visibles
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

	void bloqueBonificacion(){//Funcion que crea un bloque de bonificacion de tamaño arriba de spawn de Mario
		t_GameStateManager.editMarioWorld[0,5] = 4;
		t_GameStateManager.modifMap = 1;
	}

	void facilitadorPorCaida(int cantidadMuerteCaida){//Funcion que crea un bloque 1x2 suelo en las posiciones donde el jugador murio por caida
		for (int i = 0; i < cantidadMuerteCaida; i++)
		{
			int indice = t_GameStateManager.playerLevelData.FindIndex(x => x.mov == "OUT");
			double elemx = t_GameStateManager.playerLevelData[indice-1].cooX;
			double elemy = t_GameStateManager.playerLevelData[indice-1].cooy;
			t_GameStateManager.editMarioWorld[Convert.ToInt32(elemx),Convert.ToInt32(elemy)] = 2;
			t_GameStateManager.playerLevelData[indice].mov ="IN";	
		}
	}

	void facilitadorPorEnemigo(){//Funcion que reduce a la mitad la cantidad de enemigos
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

	void facilitadorPorTiempo(){// Funcion que aumenta 200seg el tiempo del jugador
		t_GameStateManager.timeLeft = 600.5f;
		t_GameStateManager.modifMap = 1;
	}

//Funciones dificultadoras
	void safeZoneDestroyer(List<Coordenadas> listaDatosJugador){//Funcion que pone obstaculo en las zonas donde el personaje se quede quieto por mucho tiempo
		double[,] posicion = new double[5,2]; //Matriz que guarda las 5 posiciones en x & donde se quedo quieto mas veces (5 por partida) ;
		double fx = 0;//variable que guarda la coordenada en X 
		double fy =0;//Variable que guarda la coordenada en Y
		int cont = 0;//Contador que guarda el numero de repeticiones 
		
		int contp1 = 0;//Variable para recorrer matriz "posicion" en X de 0 a 5
		Debug.Log("Iteracion nueva " + contp1);
		int contp2 = 0;//Variable para recorrer la matriz posicion en Y de 0 a 1;

		for (int i = 0; i < ((listaDatosJugador).Count)-1; i++)//Recorre la lista de datos del jugador
		{
			if (contp1 <5)//Solo guardara 5 obstaculizadores (rebalanceado)
			{
				if ( listaDatosJugador[i].cooX==listaDatosJugador[i+1].cooX &&
					listaDatosJugador[i].cooX < 185 ||
					 (listaDatosJugador[i].cooX != 0 &&  listaDatosJugador[i].cooX < 30 )||
					 (listaDatosJugador[i].cooX != 0 &&  listaDatosJugador[i].cooX > 35 ))//Compara coordenadas X iguales en 1 vs 1
				//if (listaDatosJugador[i].cooX==listaDatosJugador[i+1].cooX&&listaDatosJugador[i].cooy==listaDatosJugador[i+1].cooy)
				{
					cont++; //cont aumentara en 1 
					listaDatosJugador.RemoveAt(i);
					//Debug.Log("numero"+ listaDatosJugador[i].cooX);
				}
				else if(cont >=100)//Si ya no hay repetidos seguidos y contador llego a 100, guardara en posiciones las coordenadas en x & y
				{
					//Debug.Log("contador cuando pasa 100: "+ cont);
					fx = listaDatosJugador[i-1].cooX;//Guarda la ultima posicion repetida en X
					fy = listaDatosJugador[i-1].cooy;//Guarda la ultima posicion repetida en Y
					posicion[contp1,contp2] = fx;//Guarda en posicion la coordenada X
					posicion[contp1,contp2+1] = fy;//Guarda en posicion la coordenada Y
					//Debug.Log("fx guardado: "+ fx);
					//Debug.Log("fy guardado: "+ fy);
					contp1++;
					//fx = listaDatosJugador[i].cooX;//creo que sobran
					//fy = listaDatosJugador[i].cooy;//creo que sobran xd
					cont = 0;//Reinicia contador 
				}else
				{
					cont = 0;//Reinicia contador
				}		
			}	
		}

		for (int i = 0; i < 5; i++)//Recorre la matriz posicion
		{
			int j = 0;//variable para recorrer Y en 0 & 1
			double elemx = posicion[i,j]; //Guarda en elemx la ubicacion guardada en X en la posicion actual de posicion
			double elemy = posicion[i,j+1];//Guarda en elemy la ubicacion guardada en Y en la posicion actual de posicion
			//Debug.Log(elemx +"elemento x");
			if ((elemx != 0 && elemx < 30 )||  (elemx != 0 && elemx > 35))//Potege el spawn del jugador
			{
				//Debug.Log(elemx +"bomb has be planted" );
				t_GameStateManager.editMarioWorld[Convert.ToInt32(elemx),Convert.ToInt32(elemy)] = 21;//Pone el obstaculizador
			}
			
		}
	}

	void aleatorizarBonusMap(int cantidadBonusEnMapa, int cantidadMonedasEnMapa, int cantidadBonusRecolectados, int cantidadMonedasRecolectados){//Funcion para aleatorizar los bonificadores 
		//int sumCantidades = cantidadBonusEnMapa + cantidadMonedasEnMapa;//Suma la cantidad de bloques de monedas+bonificadores en el mapa
		int sumCantidades = cantidadBonusRecolectados + cantidadMonedasRecolectados;
		int[] listaRandomBonus = new int[sumCantidades];//Array del tamaño total de bloques de monedas+bonificadores
		//int bonus = cantidadBonusEnMapa;//guarda la cantidad de bonus del mapa 
		int bonus = cantidadBonusRecolectados;
		//Debug.Log(cantidadMonedasRecolectados + "LISTA RANDOM DE BONUS");
		for (int i = 0; i < listaRandomBonus.Length; i++)//Recorre el array vacio y va guardando una cantidad de bonus y despues rellena con monedas
		{
			if (bonus != 0)
			{
				listaRandomBonus[i] = 4;
				bonus--;
			}else
			{
				listaRandomBonus[i]= 3;
			}
		}
		

		var rnd = new Random(); //Genera un numero random cada que se llama
		//Debug.Log(listaRandomBonus.Length + "LISTA RANDOM DE BONUS TAMAÑO");
		for (int i = (listaRandomBonus.Length)- 1; i > 0; i--)//Mueve a posiciones diferentes del array todos los elementos
		{
			var j = rnd.Next(0, i);
			var temp = listaRandomBonus[i];
			listaRandomBonus[i] = listaRandomBonus[j];
			listaRandomBonus[j] = temp;
		}


		/*foreach(var s in listaRandomBonus)
            {
                Debug.Log(s);
            }
		*/
		int aux = 0;//variable auxiliar
		for (int i = 0; i < 185; i++)//Recorre el array del mapa, verifica si es moneda o bonificador y lo cambia por el orden del array 
		{
			for (int j = 0; j < 14; j++)
			{	
				if (aux != listaRandomBonus.Length)
				{
					if(t_GameStateManager.editMarioWorld[i,j] == 3 ||t_GameStateManager.editMarioWorld[i,j] == 4)
					{
						t_GameStateManager.editMarioWorld[i,j]= listaRandomBonus[aux];
						aux++;
					}
				}
				
			}
		}
	}

	int buscarElementos(int idElemento){//Busca la cantidad de elementos que hay en un array bidimensional
		int numElemento = 0;
		for (int i = 0; i < 185; i++)
		{
			for (int j = 0; j < 14; j++)
			{
				if(t_GameStateManager.editMarioWorld[i,j] == idElemento)
				{
					numElemento++;
				}
			}
		}
		//Debug.Log(numElemento + " numElñemento");
		return numElemento;
	}

	void rageEnemy(int cantidadEnemigosTotal,int cantidadEnemigosMatados){
		Debug.Log("cantidadEnemigosTotal: " + cantidadEnemigosTotal);
		Debug.Log("cantidadEnemigosMatados: " + cantidadEnemigosMatados);
		t_GameStateManager.enemigosKill = cantidadEnemigosMatados;////////////
		if (cantidadEnemigosMatados <= cantidadEnemigosTotal/3)
		{
			if (t_GameStateManager.controlVelocidad < 8)
			{
				t_GameStateManager.controlVelocidad++;
			}else 
			{
				t_GameStateManager.controlVelocidad = 8;
			}
			
			//t_GameStateManager.controlVelocidad++;
			//Debug.Log("Enemigos lvl " + t_GameStateManager.controlVelocidad);
			
		}
		else if (cantidadEnemigosMatados > cantidadEnemigosTotal/3 && cantidadEnemigosMatados <= cantidadEnemigosTotal/2)
		{
			//Debug.Log(cantidadEnemigosTotal/2 + "Cantidad enemigos en total div 2");
			if (t_GameStateManager.controlVelocidad <= 4)
			{
				t_GameStateManager.controlVelocidad = 5;
			}else if (t_GameStateManager.controlVelocidad > 4 && t_GameStateManager.controlVelocidad <= 6)
			{
				t_GameStateManager.controlVelocidad++;
			}else if(t_GameStateManager.controlVelocidad == 7)
			{
				t_GameStateManager.controlVelocidad++;
			}else if (t_GameStateManager.controlVelocidad == 8)
			{
				t_GameStateManager.controlVelocidad = 8;
			}
			//Debug.Log("Enemigos lvl " + t_GameStateManager.controlVelocidad);
			
		}else if (cantidadEnemigosMatados > cantidadEnemigosTotal/2)
		{
			//Debug.Log(cantidadEnemigosTotal + "Cantidad enemigos en total div nope");
			//Debug.Log(cantidadEnemigosMatados + "noentender");
			if (t_GameStateManager.controlVelocidad <= 4)
			{
				t_GameStateManager.controlVelocidad = 7;
			}else if (t_GameStateManager.controlVelocidad > 4 && t_GameStateManager.controlVelocidad <= 7)
			{
				t_GameStateManager.controlVelocidad = 8;
			}else
			{
				t_GameStateManager.controlVelocidad = 8;
			}
			//Debug.Log("Enemigos lvl " + t_GameStateManager.controlVelocidad);
			
		}
	}

	void limpiadorPorIteracion(string str1, string str2){
		foreach (var item in t_GameStateManager.playerLevelData)
			{
				if (item.mov == str1)
				{
					item.mov = str2;
				}
			}
	}

	void tiempoPorNivel(int tiempoFinalPorNivel, int tiempoFinalAcumulado){
		if (tiempoFinalPorNivel >= (tiempoFinalAcumulado*0.75))
		{
			t_GameStateManager.timeLeft = tiempoFinalAcumulado -Convert.ToSingle(tiempoFinalAcumulado*0.08) ;
			Debug.Log("Reduccion del -8% - "+"Tiempo total anterior:" + tiempoFinalAcumulado+"Tiempo restado: "+(Convert.ToSingle(tiempoFinalAcumulado*0.08))+ "Nuevo tiempo: "+t_GameStateManager.timeLeft);
		}else if (tiempoFinalPorNivel >= (tiempoFinalAcumulado*0.50))
		{
			t_GameStateManager.timeLeft = tiempoFinalAcumulado -Convert.ToSingle(tiempoFinalAcumulado*0.05) ;
			Debug.Log("Reduccion del -5% - "+"Tiempo total anterior:" + tiempoFinalAcumulado+"Tiempo restado: "+(Convert.ToSingle(tiempoFinalAcumulado*0.05))+ "Nuevo tiempo: "+t_GameStateManager.timeLeft);
		}else if (tiempoFinalPorNivel >= (tiempoFinalAcumulado*0.25))
		{
			t_GameStateManager.timeLeft = tiempoFinalAcumulado -Convert.ToSingle(tiempoFinalAcumulado*0.02) ;
			Debug.Log("Reduccion del -2% - "+"Tiempo total anterior:" + tiempoFinalAcumulado+"Tiempo restado: "+(Convert.ToSingle(tiempoFinalAcumulado*0.02))+ "Nuevo tiempo: "+t_GameStateManager.timeLeft);
		}
	}

	void enemigosEspeciales(List<Coordenadas> listaDatosJugador)
	{
		for (int i = 0; i < 185; i++)//Recorre el array del mapa 
		{
			for (int j = 0; j < 14; j++)
			{
				if(t_GameStateManager.editMarioWorld[i,j] == 10 || t_GameStateManager.editMarioWorld[i,j] == 11)
				{
					t_GameStateManager.editMarioWorld[i,j]= 11;

					if (t_GameStateManager.editMarioWorld[i-3,j] == 0)
					{
						t_GameStateManager.editMarioWorld[i-3,j]= 11;
					}else if (t_GameStateManager.editMarioWorld[i+3,j]== 0)
					{
						t_GameStateManager.editMarioWorld[i+3,j]= 11;
					}
					
				}
			}
		}
			
		
	}

	void caminoTomado(int caminoSuperiorDetectado)
	{
		var rnd = new Random();
		var j = rnd.Next(76, 89);
		var k = rnd.Next(10, 12);
		Debug.Log( t_GameStateManager.editMarioWorld[76,9]);
		var aux = 1;
		for (int i = 76; i < 89; i++)
		{
			if (t_GameStateManager.editMarioWorld[i,9]== 6 && aux != 0)
			{
				t_GameStateManager.editMarioWorld[j,9]= 0;
				t_GameStateManager.editMarioWorld[j,k]= 6;
				//Debug.Log("EFECTIVAMENTE " + t_GameStateManager.editMarioWorld[j,k]);
			}
		}
		aux = 1;
		
		
	}

	void caminoTomadoInferior(int caminoInferiorDetectado)
	{
		var rnd = new Random();
		var j = rnd.Next(67, 112);
		var k = rnd.Next(0, 1);
		Debug.Log("inferior activado");
		var aux = 1;
		//var aux2 = 0;
		for (int i = 67; i < 112; i++)
		{
			if (t_GameStateManager.editMarioWorld[i,0]== 2 && aux != 0)
			{
				Debug.Log("Activo por deteccion de bloque libre");
				if (t_GameStateManager.editMarioWorld[i-1,0]!= 0)
				{
					verificarAdyacentes(i-1,j);
				}else if (t_GameStateManager.editMarioWorld[i-2,0]!= 0)
				{
					verificarAdyacentes(i-2,j);
				}else if (t_GameStateManager.editMarioWorld[i-3,0]!= 0)
				{
					verificarAdyacentes(i-3,j);
				}
			}
		}
		aux = 1;
	}

	void verificarAdyacentes(int i, int j)
	{
		int aux2 = 0;
		for (int b = 1; b <= 5; b++)
		{
			if (t_GameStateManager.editMarioWorld[i+b,0]==0)
			{
				aux2++;
			}
		}
		if (aux2<=4)
		{
			t_GameStateManager.editMarioWorld[j,0]= 0;
			t_GameStateManager.editMarioWorld[j,1]= 0;
			Debug.Log("Eliminacion inferior" + t_GameStateManager.editMarioWorld[j,0]);
		}
	}
}
