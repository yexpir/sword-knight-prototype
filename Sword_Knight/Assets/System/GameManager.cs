using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Movement player;

    public Vector2 entrance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadSceneAsync("PlayerContainment", LoadSceneMode.Additive);

        SceneManager.LoadSceneAsync("TestScene 1", LoadSceneMode.Additive);
    }

    public void SetPlayer(Movement _player)
    {
        player = _player;
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneByName(sceneName);

        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name != newScene.name && scene.name != "PlayerContainment" && scene.name != "BaseScene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    public void SetStartingPoint(Vector2 pointEntrance)
    {
        entrance = pointEntrance;

        player.gameObject.transform.position = entrance;
    }
}
