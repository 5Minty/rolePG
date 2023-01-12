using UnityEngine;

public class FloatingTextCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var objs = FindObjectsOfType(typeof(FloatingTextCanvas));

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
