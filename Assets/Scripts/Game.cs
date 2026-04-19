using UnityEngine;

public class Game : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Playing,
        Boss,
        BossAttack,
        Result
    }
    
    public static Game Instance;

    public static GameState state = GameState.Playing;

    public Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
}

public static class GameUtils
{
    public static T GetRandomEntry<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }
}
