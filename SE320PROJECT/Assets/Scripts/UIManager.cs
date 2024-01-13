using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
  

public class UIManager : MonoBehaviour
{
  [SerializeField] private TextMeshPro ammoText;

  public void UpdateAmmo(int count)
  {
    ammoText.text = "Ammo" + count;
  }

  private void Start()
  {
    ammoText = GetComponentInChildren<TextMeshPro>();
  }
}
