using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] int value;

    protected abstract void Movement();

    public virtual void GotHit()
    {
        GameManager.Instance.AddScore(value);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
