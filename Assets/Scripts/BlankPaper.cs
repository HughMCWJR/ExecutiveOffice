using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankPaper : MonoBehaviour
{

    public string type;

    public List<Sprite> sprites = new List<Sprite>();

    SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        
        switch (type)
        {

            case "BILL":
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
                break;

        }

    }

    void Update()
    {
        


    }

}
