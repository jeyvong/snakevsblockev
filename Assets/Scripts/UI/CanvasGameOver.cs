using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameOver : MonoBehaviour
{
    private Game _GameScrypt;
    private ButtonScrypt _ButtonScrypt;
    private Canvas myCanvas;
    public GameObject MovedElement;
    private float sinMultiplerY;
    private float sinMultiplerX;

    private void Awake() 
    {
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _ButtonScrypt = GameObject.Find("CanvasMainMenu").GetComponent<ButtonScrypt>();
        _ButtonScrypt.GameCanvasGameOver = gameObject;
        _GameScrypt.GameCanvasGameOver = gameObject;
        myCanvas = GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.SetActive(false);
    }

}
