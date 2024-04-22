using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedScript : MonoBehaviour
{
    [SerializeField] private float time = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("create a collected");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0){
            Destroy(gameObject);
        }
    }
}
