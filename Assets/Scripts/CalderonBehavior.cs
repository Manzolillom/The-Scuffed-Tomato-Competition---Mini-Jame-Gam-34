using System.Collections.Generic;
using UnityEngine;

public class CalderonBehavior : MonoBehaviour
{
    public GameObject poison; //item output
    public List<Ingredient> requests; //what it asks for the potion
    [Header("Recipe Requests Config")]
    public List<Ingredient> ingredients; //randomizable needed ingredients
    [SerializeField] int minIngredients = 3, maxIngredients = 6;
    [Header("Other")]
    [SerializeField] Transform minigameCamHolder;
    [Header("Other scripts to reference")]
    [SerializeField] PlayerCamera playerCameraScript; //use this to change it's mode from following player to stay there
    [SerializeField] PlayerMovement playerMovementScript; //use this to change the commands from commanding the main character to it's small cofigure on the table

    bool isPlayerInCraftingRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isPlayerInCraftingRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isPlayerInCraftingRange = false;
    }

    private void Update()
    {
        if (!isPlayerInCraftingRange) return;
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if (requests.Count == 0)
            {
                GenerateNewRecipe();
            }
            ///opens minigame mode
        }
    }

    void GenerateNewRecipe()
    {
        int howManyIngredients = Random.Range(minIngredients, maxIngredients + 1);
        for(int i = 0; i<howManyIngredients; i++)
        {
            requests.Add(ingredients[Random.Range(0, ingredients.Count)]);
        }
    }
}
