using UnityEngine;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private GameObject visuals;
    
    public void Init()
    {
        visuals.SetActive(true);
        gameObject.transform.parent = null;
    }
    
    public void Clear()
    {
        visuals.SetActive(false);
        gameObject.transform.parent = PlayerCursor.Instance.transform;
    }
}