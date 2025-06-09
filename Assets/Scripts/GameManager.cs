using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DungeonGenerator dungeonGenerator;
    private int currentLevel = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistente entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToNextLevel()
    {
        currentLevel++;
        Debug.Log("Cambiando al nivel: " + currentLevel);

        // Destruye todo menos el GameManager y el DungeonGenerator
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go == this.gameObject || go == dungeonGenerator.gameObject) continue;
            Destroy(go);
        }

        // Regenerar dungeon
        dungeonGenerator.GenerateDungeon();
    }
}
