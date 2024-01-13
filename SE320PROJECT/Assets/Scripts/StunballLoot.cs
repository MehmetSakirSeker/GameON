using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StunballLoot : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<StunBall>().enabled = true;
        StartCoroutine(loadDungeon());
        Destroy(gameObject);
    }

    private IEnumerator loadDungeon()
    {
        SceneManager.LoadScene("Scenes/Modular Dungeon");
        yield return null;
    }
}
