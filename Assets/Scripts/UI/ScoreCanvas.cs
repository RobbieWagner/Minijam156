using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour
{
    public Canvas canvas;
    public static ScoreCanvas Instance {get; private set;}
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
}
