using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class Food : Consumable
{
    public FoodType foodType;

    public enum FoodType
    {
        NONE,
        APPLE,
        LEMON,
        ORANGE,
        PEACH,
        PINEAPPLE,
        BANANA,
        CHERRY,
        WATERMELON,
        COOKIE,
        PINKCANDY,
        CANDYCANE,
        CHOCOLATECAKE,
        CHOCOLATE,
        BLUELOLIPOP,
        CHOCOLATEICECREAM,
        HONEY,
        VANILLAICECREAM,
        STRAWBERRYCAKE,
        BLUECANDY,
        GREENCANDYCANE,
        WHITECHOCOLATE,
        GREENLOLIPOP,
        WATER,
        CHEESE,
        EGG,
        MEAT,
        FISH,
        SANDWICH,
        POTATO,
        STEAK,
        BREAD,
        CARROT,
        TURNIP,
        TOMATO,
        GRAPE,
        MUSHROOM,
        JELLYDONUT,
        PIZZA,
        CORN,
        COTTONCANDY,
        STRAWBERRY,
        RAMEN,
        PUDDING,
        CURRY,
        PIE,
        WHEAT,
        ACORN,
        EGGPLANT,
        RADDISH,
        LEEK,
        MUFFIN,
        JAM
    }

    public Food(string id, string name, string flavor, string desc, Sprite img, FoodType type, int level = 1)
    {
        itemName = name;
        flavorText = flavor;
        description = desc;
        image = img;
        this.level = level;
        this.id = id;
        foodType = type;
    }

    public Food(SaveConsumable sc)
    {
        itemName = sc.itemName;
        flavorText = sc.rank;
        description = sc.description;
        level = sc.itemLevel;
        id = sc.id;
        foodType = System.Enum.Parse<FoodType>(sc.type);
    }
}
