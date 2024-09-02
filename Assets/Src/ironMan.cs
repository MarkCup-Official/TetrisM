using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironMan : MonoBehaviour
{
    public SpriteRenderer sprite;
    void Start()
    {
        sprite.sprite = Resources.LoadAll<Sprite>("IronMan")[Random.Range(0,6)];
        MySystem.sound.PlayOneShot(Resources.Load("IronMan_voice") as AudioClip);

    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
