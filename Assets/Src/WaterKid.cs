using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterKid : MonoBehaviour
{
    public int x, y, n;
    public SpriteRenderer sprite;
    public void Hello()
    {
        name = "Water" + x.ToString() + ',' + y.ToString();
        sprite.size =new Vector2(0.16f, 0.01f * n);
        transform.position = new Vector2(-2.8f + x * 0.4f, -4f + y * 0.025f+0.025f*(n/2)); 
        int yy = 0;
        while (yy < n)
        {
            if(y+yy<320)
                Water.showingWater[x, y + yy] = 1;
            yy++;
        }
    }

    public void GoodBye()
    {
        int yy=0;
        while (yy < n)
        {
            Water.showingWater[x, y + yy] = 0;
            yy++;
        }
        Destroy(gameObject);
    }

    public void Wei(int nn)
    {
        if (nn==0)
        {
            Destroy(gameObject);
        }
        int yy = 0;
        while (yy < n)
        {
            Water.showingWater[x, y + yy] = 0;
            yy++;
        }
        n = nn;
        Hello();
    }
}
