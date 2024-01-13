using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
   [SerializeField] private Canvas gameOverCanvas;

   public void AfterDeath()
   {
      gameOverCanvas.enabled = true;
      Time.timeScale = 0;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
   }
   
   public void QuitGame()
   {
      Application.Quit();
   }
   
}
