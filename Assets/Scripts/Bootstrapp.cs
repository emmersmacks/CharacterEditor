using DefaultNamespace;
using UnityEngine;

public class Bootstrapp : MonoBehaviour
{
    public PartsLayer PartsLayer;
    // Start is called before the first frame update
    void Start()
    {
        PartsLayer.Set();
    }
}
