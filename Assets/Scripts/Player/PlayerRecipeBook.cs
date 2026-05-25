using System.Collections.Generic;
using UnityEngine;

public class PlayerRecipeBook : MonoBehaviour
{
    [SerializeField] private Dictionary<int, Recipe> recipeBook;

    void Awake()
    {
        recipeBook = new();
    }





}