using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Coordenadas {
    public GameStateManager ArrayMov;
    public string mov;
    public double cooX;
    public double cooy;
        
    public Coordenadas(string mv, double cX, double cY)
    {
        mov = mv;
        cooX = cX;
        cooy = cY;
        Debug.Log("entro esta wea");
    }
    
    
    
}
