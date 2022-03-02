using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] scenesNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();

            string sceneName = scenesNames[Random.Range(0, scenesNames.Length)];

            SceneManager.LoadScene(sceneName);
        }
    }
}
