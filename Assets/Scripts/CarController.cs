using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera bordersGO;
    [SerializeField] private GameObject[] deck;

    BoxCollider2D collider;

    void Start()
    {

        collider = bordersGO.GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        while(true)
        {
            var car = Instantiate(deck[Random.Range(0,4)]);
            car.transform.position = new Vector3(collider.bounds.min[0], 
                 collider.bounds.min[1] + Random.Range(0,collider.bounds.size[1]), 0);
            yield return new WaitForSeconds(2);
        }
    }
}
