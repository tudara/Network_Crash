using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerKey : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.CollectHacker();
            Destroy(gameObject);
            Debug.Log("Hacker Collected");
        }
    }
}
