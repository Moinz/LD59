using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
}
