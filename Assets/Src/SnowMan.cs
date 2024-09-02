using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : MonoBehaviour
{
    public SpriteRenderer sprite;
    void Start()
    {
        sprite.sprite = Resources.LoadAll<Sprite>("SnowMan")[Random.Range(0,18)];
        MySystem.sound.PlayOneShot(Resources.Load("SnowMan_voice") as AudioClip);

    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
