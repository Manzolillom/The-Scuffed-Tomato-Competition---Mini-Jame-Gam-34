using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBehavior : MonoBehaviour
{
    [SerializeField] Ingredient ingredient;

    private void Awake()
    {
        //gets its 3d gameobject and 2d sprite
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.name == "Sprite") 
            { 
                SpriteRenderer spriteRender = transform.GetChild(i).GetComponent<SpriteRenderer>();
                ingredient.ingredientSprite.AddComponent<SpriteRenderer>();
                ingredient.ingredientSprite.GetComponent<SpriteRenderer>().sprite = spriteRender.sprite;
                ingredient.ingredientSprite = transform.GetChild(i).gameObject;
            }
            else
            {
                ingredient.ingredintName = transform.GetChild(i).name;
                ingredient.ingredientGameObj = transform.GetChild(i).gameObject;
            }
        }
    }
}
