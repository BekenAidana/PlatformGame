using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected virtual void HurtPlayer(IPlayer player)
    {
        player.Hurt();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
        {
            HurtPlayer(player);
        }
    }
}
