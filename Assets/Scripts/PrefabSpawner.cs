using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        PlayButton.OnButtonPressed += SpawnRandomPrefab;
    }

    private void OnDestroy()
    {
        PlayButton.OnButtonPressed -= SpawnRandomPrefab;
    }

    public void SpawnRandomPrefab()
    {
        if (prefabs == null || prefabs.Count == 0 || mainCamera == null)
        {
            Debug.LogWarning("Prefab list is empty or Main Camera is not assigned.");
            return;
        }

        //Choose the random prefab that will spawn
        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

        Vector2 spawnPosition = GetRandomScreenPosition();

        Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomScreenPosition()
    {
        //screen bounds in world space
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize * 2f;

        float x = Random.Range(-screenWidth / 2f, screenWidth / 2f);
        float y = Random.Range(-screenHeight / 2f, screenHeight / 2f);

        return new Vector2(x, y);
    }
}
