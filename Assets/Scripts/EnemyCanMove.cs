using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanMove : Trap
{
    [SerializeField] private int _distance;
    private int direction = -1;
    private Vector3 initialPosition;

    private Animator _animator;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while(true)
        {
            for(int i=0; i<_distance*100; i++)
            {
                transform.position = Vector3.Lerp(transform.position,
                                        transform.position + new Vector3(1*direction, 0, 0), 
                                        Time.deltaTime);
                yield return null;
            }
        
            _sprite.flipX = !_sprite.flipX;
            direction = direction * (-1);
            yield return null;
        }

        
    }


}


