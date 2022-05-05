using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
public class MapMaker : MonoBehaviour
{

    public Tile tile;
    public Tilemap tilemap;
    
    public Transform suelo5x2;
    public Transform suelo2x2;
    public Transform bloqueCoin;
    public Transform bloqueBonif;
    public Transform bloqueVida;
    public Transform ladrilloComun;
    public Transform ladrilloCoin;
    public Transform ladrilloStar;
    public Transform enemyGoomba;
    public Transform enemyKoopa;
    public Transform enemyKoopaFly;
    public Transform enemyGoombaGreen;
    public Transform greenPipe2x2;
    public Transform greenPipe2x3;
    public Transform greenPipe2x4;
    public Transform marble1x1;
    public Transform marble1x2;
    public Transform marble1x3;
    public Transform marble1x4;
    public Transform marbleFinish;
    public Transform fireBar;
    public Transform coin;

    

    public int mapWidth;
    public int mapHeight;

    private int[,] mapData;

    private int prueb;
    private string obstaculo = "";
    public List<int> updateData = new List<int>();
    public List<int> pochoclo = new List<int>();

    private GameStateManager t_GameStateManager;


    private int[,] normalMarioWorld;
    // Start is called before the first frame update
    void Start()
    {

        this.normalMarioWorld = new int[,] {  { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 6, 0, 0, 0, 3, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 12, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 6, 12, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 7, 0, 0, 0, 3, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 1, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 1, 0, 12, 0, 0, 6, 0, 0, 0, 3, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 3, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 2, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};


        t_GameStateManager = FindObjectOfType<GameStateManager> ();
        this.mapData = new int[this.mapWidth,this.mapHeight];
        prueb = 0;
        int aux = 0;




        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                
               
                /*
                int ranNum= Random.Range(0, 70);
                if (ranNum == 1 || ranNum == 2 || ranNum == 3 ||ranNum == 4)
                {
                    this.mapData[i,j] = ranNum;
                }else
                {
                    this.mapData[i,j] = 0;
                }
                */

                
                if(i == 23  && j == 5 ){
                    this.mapData[i,j] = 1;
                }else {
                    this.mapData[i,j] = 0;
                }
                

                updateData.Add(this.mapData[i,j]);
                /*
                if(prueb < 6)
                {
                    this.mapData[i,j] = 0;
                    prueb++;
                }else if(prueb==6)
                {
                    this.mapData[i,j] = 1;
                    prueb=0;
                }
                */
            }
        }
        
        

        this.GenerateTiles();

        string str = string.Join(",",updateData);
        Debug.Log(str);
        
    }



    
    //Pinta los obstaculos en el tilemap
   /* void GenerateTiles()
    {
        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                
                if(this.mapData[i,j] == 1)
                {
                   /* this.tilemap.SetTile(
                        new Vector3Int(i,j,0),
                        this.tile
                    );
                    Instantiate(prefab1, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==2)
                {
                    Instantiate(prefab2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==3)
                {
                    Instantiate(prefab3, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==4)
                {
                    Instantiate(prefab4, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
        
    }
    */



    void GenerateTiles()
    {
        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                /*
                suelo 5x2 = 1
                suelo 2x2 = 2
                bloqueCoin = 3
                bloqueBonif = 4
                bloqueVida = 5
                ladrilloComun = 6
                ladrilloCoin = 7
                ladrilloStar = 8
                enemyGoomba =9
                enemyKoopa = 10
                enemyKoopaFly = 11
                enemyGoombaGreen = 12
                greenPipe2x2 = 13
                greenPipe2x3 = 14
                greenPipe2x4 = 15
                marble1x1 = 16
                marble1x2 = 17
                marble1x3 = 18
                marble1x4 = 19
                marbleFinish = 20
                fireBar = 21
                coin = 22
                */




                if(this.normalMarioWorld[i,j] == 1)
                {
                   /* this.tilemap.SetTile(
                        new Vector3Int(i,j,0),
                        this.tile
                    );*/
                    Instantiate(suelo5x2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==2)
                {
                    Instantiate(suelo2x2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==3)
                {
                    Instantiate(bloqueCoin, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==4)
                {
                    Instantiate(bloqueBonif, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==5)
                {
                    Instantiate(bloqueVida, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==6)
                {
                    Instantiate(ladrilloComun, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==7)
                {
                    Instantiate(ladrilloCoin, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==8)
                {
                    Instantiate(ladrilloStar, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==9)
                {
                    Instantiate(enemyGoomba, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==10)
                {
                    Instantiate(enemyKoopa, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==11)
                {
                    Instantiate(enemyKoopaFly, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==12)
                {
                    Instantiate(enemyGoombaGreen, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==13)
                {
                    Instantiate(greenPipe2x2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==14)
                {
                    Instantiate(greenPipe2x3, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==15)
                {
                    Instantiate(greenPipe2x4, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==16)
                {
                    Instantiate(marble1x1, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==17)
                {
                    Instantiate(marble1x2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==18)
                {
                    Instantiate(marble1x3, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==19)
                {
                    Instantiate(marble1x4, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==20)
                {
                    Instantiate(marbleFinish, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==21)
                {
                    Instantiate(fireBar, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.normalMarioWorld[i,j]==22)
                {
                    Instantiate(coin, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
