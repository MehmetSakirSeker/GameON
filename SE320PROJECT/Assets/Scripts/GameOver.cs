using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   

   public void TryAgain()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
   }
   
   public void QuitGame()
   {
      Application.Quit();
   }
   
}
