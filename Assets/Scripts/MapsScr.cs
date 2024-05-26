using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsScr : MonoBehaviour
{
    [SerializeField] GameObject backGround;
    [SerializeField] int n = 10, m = 10;
    private BoxCollider2D bc;

    void Start()
    {
        GameObject parentObject = new GameObject("ParentObject"); // Tạo một GameObject mới để làm cha

        bc = GetComponent<BoxCollider2D>();
        Vector3 size = bc.bounds.size;
        for(int i =0;i<n;i++){
            for(int j = 0;j<m;j++){
                GameObject temp = Instantiate(backGround,new Vector3(transform.position.x + size.x * j, transform.position.y + size.y * i * -1, 0), Quaternion.identity);
                temp.transform.parent = parentObject.transform; // Gán parentObject.transform làm cha
            }
        }
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
