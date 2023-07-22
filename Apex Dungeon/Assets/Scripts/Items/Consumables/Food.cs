using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gloves;
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
        
        this.level = level;
        this.id = id;
        foodType = type;
        image = GetImage();
    }

    public Food(SaveConsumable sc)
    {
        itemName = sc.itemName;
        flavorText = sc.rank;
        description = sc.description;
        level = sc.itemLevel;
        id = sc.id;
        foodType = System.Enum.Parse<FoodType>(sc.type);
        image = GetImage();
    }

    public override Sprite GetImage()
    {
        FoodGenerator fg = Resources.Load<FoodGenerator>("ScriptableObjects/FoodGenerator");

        if (foodType == FoodType.APPLE) return fg.apple;
        if (foodType == FoodType.LEMON) return fg.lemon;
        if (foodType == FoodType.ORANGE) return fg.orange;
        if (foodType == FoodType.PEACH) return fg.peach;
        if (foodType == FoodType.PINEAPPLE) return fg.pineapple;
        if (foodType == FoodType.BANANA) return fg.banana;
        if (foodType == FoodType.CHERRY) return fg.cherry;
        if (foodType == FoodType.WATERMELON) return fg.watermelon;
        if (foodType == FoodType.COOKIE) return fg.cookie;
        if (foodType == FoodType.PINKCANDY) return fg.pinkCandy;
        if (foodType == FoodType.CANDYCANE) return fg.candyCane;
        if (foodType == FoodType.CHOCOLATECAKE) return fg.chocolateCake;
        if (foodType == FoodType.CHOCOLATE) return fg.chocolate;
        if (foodType == FoodType.BLUELOLIPOP) return fg.blueLolipop;
        if (foodType == FoodType.CHOCOLATEICECREAM) return fg.chocolateIceCream;
        if (foodType == FoodType.HONEY) return fg.honey;
        if (foodType == FoodType.VANILLAICECREAM) return fg.vanillaIceCream;
        if (foodType == FoodType.STRAWBERRYCAKE) return fg.strawberryCake;
        if (foodType == FoodType.BLUECANDY) return fg.blueCandy;
        if (foodType == FoodType.GREENCANDYCANE) return fg.greenCandyCane;
        if (foodType == FoodType.WHITECHOCOLATE) return fg.whiteChocolate;
        if (foodType == FoodType.GREENLOLIPOP) return fg.greenLolipop;
        if (foodType == FoodType.WATER) return fg.water;
        if (foodType == FoodType.CHEESE) return fg.cheese;
        if (foodType == FoodType.EGG) return fg.egg;
        if (foodType == FoodType.MEAT) return fg.meat;
        if (foodType == FoodType.FISH) return fg.fish;
        if (foodType == FoodType.SANDWICH) return fg.sandwich;
        if (foodType == FoodType.POTATO) return fg.potato;
        if (foodType == FoodType.STEAK) return fg.rawSteak;
        if (foodType == FoodType.BREAD) return fg.bread;
        if (foodType == FoodType.CARROT) return fg.carrot;
        if (foodType == FoodType.TURNIP) return fg.turnip;
        if (foodType == FoodType.TOMATO) return fg.tomato;
        if (foodType == FoodType.GRAPE) return fg.purpleGrape;
        if (foodType == FoodType.MUSHROOM) return fg.mushroom;
        if (foodType == FoodType.JELLYDONUT) return fg.riceball;
        if (foodType == FoodType.PIZZA) return fg.pizza;
        if (foodType == FoodType.CORN) return fg.corn;
        if (foodType == FoodType.COTTONCANDY) return fg.cottonCandy;
        if (foodType == FoodType.STRAWBERRY) return fg.strawberry;
        if (foodType == FoodType.RAMEN) return fg.ramen;
        if (foodType == FoodType.PUDDING) return fg.pudding;
        if (foodType == FoodType.CURRY) return fg.curry;
        if (foodType == FoodType.PIE) return fg.pie;
        if (foodType == FoodType.WHEAT) return fg.wheat;
        if (foodType == FoodType.ACORN) return fg.acorn;
        if (foodType == FoodType.EGGPLANT) return fg.eggplant;
        if (foodType == FoodType.RADDISH) return fg.raddish;
        if (foodType == FoodType.LEEK) return fg.leek;
        if (foodType == FoodType.MUFFIN) return fg.muffin;
        if (foodType == FoodType.JAM) return fg.jam;

        return null;
    }
}
