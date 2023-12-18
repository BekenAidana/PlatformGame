using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Camera mainCamera;
    BoxCollider2D collider;
    float velocity;

    void Start()
    {
        velocity = Random.Range(1,8);
        int scale = Random.Range(1,4);
        
        transform.localScale = new Vector3(scale, scale, scale);
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sortingOrder = scale - 5;

        mainCamera = Camera.main;
        collider = mainCamera.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
                            transform.position + Vector3.right, 
                            Time.deltaTime*velocity);
        checkOutBorders(collider, gameObject);
    }

    void checkOutBorders(BoxCollider2D collider, GameObject gameObject)
    {
        if ((collider.bounds.min[0]-10>gameObject.transform.position.x) || 
            (collider.bounds.max[0]+10<gameObject.transform.position.x))
        {
            Destroy(gameObject);
        }
    }
}
