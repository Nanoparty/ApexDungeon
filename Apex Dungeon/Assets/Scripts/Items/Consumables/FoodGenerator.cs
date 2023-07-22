using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;

[CreateAssetMenu(fileName = "FoodGenerator", menuName = "ScriptableObjects/Food Generator")]
public class FoodGenerator : ScriptableObject
{
    public Sprite apple;
    public Sprite lemon;
    public Sprite orange;
    public Sprite peach;
    public Sprite pineapple;
    public Sprite banana;
    public Sprite cherry;
    public Sprite watermelon;
    public Sprite cookie;
    public Sprite pinkCandy;
    public Sprite candyCane;
    public Sprite chocolateCake;
    public Sprite chocolate;
    public Sprite blueLolipop;
    public Sprite chocolateIceCream;
    public Sprite honey;
    public Sprite vanillaIceCream;
    public Sprite strawberryCake;
    public Sprite blueCandy;
    public Sprite greenCandyCane;
    public Sprite whiteChocolate;
    public Sprite greenLolipop;
    public Sprite water;
    public Sprite cheese;
    public Sprite meat;
    public Sprite egg;
    public Sprite fish;
    public Sprite sandwich;
    public Sprite potato;
    public Sprite rawSteak;
    public Sprite bread;
    public Sprite carrot;
    public Sprite turnip;
    public Sprite tomato;
    public Sprite purpleGrape;
    public Sprite blueGrape;
    public Sprite mushroom;
    public Sprite riceball;
    public Sprite pizza;
    public Sprite corn;
    public Sprite cottonCandy;
    public Sprite strawberry;
    public Sprite ramen;
    public Sprite pudding;
    public Sprite curry;
    public Sprite pie;
    public Sprite wheat;
    public Sprite acorn;
    public Sprite eggplant;
    public Sprite raddish;
    public Sprite leek;
    public Sprite muffin;
    public Sprite jam;

    public GameObject CreateFood(string name, string description, Sprite image, FoodType type)
    {
        GameObject item = new GameObject(name);
        string id = "food";
        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = image;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        item.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        Food food = new Food(id, name, description, "Restores 10% HP per turn for 2 turns", image, type);
        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(food);
        item.tag = "Consumable";
        return item;
    }

    public GameObject Apple()
    {
        return CreateFood("Apple", "Keeps the doctor away!", apple, FoodType.APPLE);
    }
    public GameObject Lemon()
    {
        return CreateFood("Lemon", "Pucker up!", lemon, FoodType.LEMON);
    }
    public GameObject Orange()
    {
        return CreateFood("Orange", "Orange ya glad you found this", orange, FoodType.ORANGE);
    }
    public GameObject Peach()
    {
        return CreateFood("Peach", "Looking just peachy.", peach, FoodType.PEACH);
    }
    public GameObject Pineapple()
    {
        return CreateFood("Pineapple", "Belongs on pizza.", pineapple, FoodType.PINEAPPLE);
    }
    public GameObject Banana()
    {
        return CreateFood("Banana", "Try not to slip on it.", banana, FoodType.BANANA);
    }
    public GameObject Watermelon()
    {
        return CreateFood("Watermelon", "", watermelon, FoodType.WATERMELON);
    }
    public GameObject Cherry()
    {
        return CreateFood("Cherry", "", cherry, FoodType.CHERRY);
    }
    public GameObject Cookie()
    {
        return CreateFood("Cookie", "Welcome to the Dark Side.", cookie, FoodType.COOKIE);
    }
    public GameObject PinkCandy()
    {
        return CreateFood("Pink Candy", "Grandma's hard candy.", pinkCandy, FoodType.PINKCANDY);
    }
    public GameObject CandyCane()
    {
        return CreateFood("Candy Cane", "Keepin' it festive.", candyCane, FoodType.CANDYCANE);
    }
    public GameObject ChocolateCake()
    {
        return CreateFood("Chocolate Cake", "It's a lie.", chocolateCake, FoodType.CHOCOLATECAKE);
    }
    public GameObject Chocolate()
    {
        return CreateFood("Chocolate", "You rub it on your skin and it makes you live forever.", chocolate, FoodType.CHOCOLATE);
    }
    public GameObject BlueLolipop()
    {
        return CreateFood("Blue Lolipop", "", blueLolipop, FoodType.BLUELOLIPOP);
    }
    public GameObject ChocolateIceCream()
    {
        return CreateFood("Chocolate Ice Cream", "", chocolateIceCream, FoodType.CHOCOLATEICECREAM);
    }
    public GameObject Honey()
    {
        return CreateFood("Honey", "Watch out for bees!", honey, FoodType.HONEY);
    }
    public GameObject VanillaIceCream()
    {
        return CreateFood("Vanilla Ice Cream", "", vanillaIceCream, FoodType.VANILLAICECREAM);
    }
    public GameObject StrawberryCake()
    {
        return CreateFood("Strawberry Cake", "Berry tasty.", strawberryCake, FoodType.STRAWBERRYCAKE);
    }
    public GameObject BlueCandy()
    {
        return CreateFood("Blue Candy", "Grandpa's hard candy.", blueCandy, FoodType.BLUECANDY);
    }
    public GameObject GreenCandyCane()
    {
        return CreateFood("Green Candy Cane", "", greenCandyCane, FoodType.GREENCANDYCANE);
    }
    public GameObject WhiteChocolate()
    {
        return CreateFood("White Chocolate", "Not technically chocolate.", whiteChocolate, FoodType.WHITECHOCOLATE);
    }
    public GameObject GreenLolipop()
    {
        return CreateFood("Green Lolipop", "", greenLolipop, FoodType.GREENLOLIPOP);
    }
    public GameObject Water()
    {
        return CreateFood("Water", "Contains high levels of lead", water, FoodType.WATER);
    }
    public GameObject Cheese()
    {
        return CreateFood("Cheese", "Careful, it's extra sharp!", cheese, FoodType.CHEESE);
    }
    public GameObject Meat()
    {
        return CreateFood("Meat", "Doesn't look like any animal you know...", meat, FoodType.MEAT);
    }
    public GameObject Egg()
    {
        return CreateFood("Egg", "Natures mystery box.", egg, FoodType.EGG);
    }
    public GameObject Fish()
    {
        return CreateFood("Fish", "", fish, FoodType.FISH);
    }
    public GameObject Sandwich()
    {
        return CreateFood("Sandwich", "Not quite a foot long.", sandwich, FoodType.SANDWICH);
    }
    public GameObject Potato()
    {
        return CreateFood("Potato", "If only I had butter...", potato, FoodType.POTATO);
    }
    public GameObject Steak()
    {
        return CreateFood("Steak", "Extra rare.", rawSteak, FoodType.STEAK);
    }
    public GameObject Bread()
    {
        return CreateFood("Bread", "The best thing since...my last meal.", bread, FoodType.BREAD);
    }
    public GameObject Carrot()
    {
        return CreateFood("Carrot", "", carrot, FoodType.CARROT);
    }
    public GameObject Turnip()
    {
        return CreateFood("Turnip", "", turnip, FoodType.TURNIP);
    }
    public GameObject Tomato()
    {
        return CreateFood("Tomato", "Good for throwing at bards.", tomato, FoodType.TOMATO);
    }
    public GameObject PurpleGrape()
    {
        return CreateFood("Purple Grape", "The snack of a king.", purpleGrape, FoodType.GRAPE);
    }
    public GameObject Mushroom()
    {
        return CreateFood("Mushroom", "It's probably safe to eat.", mushroom, FoodType.MUSHROOM);
    }
    public GameObject JellyDonut()
    {
        return CreateFood("Jelly Donut", "Has a delicious filling.", riceball, FoodType.JELLYDONUT);
    }
    public GameObject Pizza()
    {
        return CreateFood("Pizza", "Dungeon style.", pizza, FoodType.PIZZA);
    }
    public GameObject Corn()
    {
        return CreateFood("Corn", "Look a little under popped.", corn, FoodType.CORN);
    }
    public GameObject CottonCandy()
    {
        return CreateFood("Cotton Candy", "", cottonCandy, FoodType.COTTONCANDY);
    }
    public GameObject Strawberry()
    {
        return CreateFood("Strawberry", "", strawberry, FoodType.STRAWBERRY);
    }
    public GameObject Ramen()
    {
        return CreateFood("Ramen", "Cooks instantly.", ramen, FoodType.RAMEN);
    }
    public GameObject Pudding()
    {
        return CreateFood("Pudding", "Keeps the doctor away!", pudding, FoodType.PUDDING);
    }
    public GameObject Curry()
    {
        return CreateFood("Curry", "Extra spicy.", curry, FoodType.CURRY);
    }
    public GameObject Pie()
    {
        return CreateFood("Pie", "Hopefully its not a bomb.", pie, FoodType.PIE);
    }
    public GameObject Wheat()
    {
        return CreateFood("Wheat", "Is this even food?", wheat, FoodType.WHEAT);
    }
    public GameObject Acorn()
    {
        return CreateFood("Acorn", "If its good enough for squirrels...", acorn, FoodType.ACORN);
    }
    public GameObject Eggplant()
    {
        return CreateFood("Eggplant", "This shape looks familiar...", eggplant, FoodType.EGGPLANT);
    }
    public GameObject Raddish()
    {
        return CreateFood("Raddish", "", raddish, FoodType.RADDISH);
    }
    public GameObject Leek()
    {
        return CreateFood("Leek", "Good for twirling.", leek, FoodType.LEEK);
    }
    public GameObject Muffin()
    {
        return CreateFood("Muffin", "I think it wants to die.", muffin, FoodType.MUFFIN);
    }
    public GameObject Jam()
    {
        return CreateFood("Jam", "", jam, FoodType.JAM);
    }


    public GameObject CreateRandomFood()
    {
        List<Func<GameObject>> foods = new List<Func<GameObject>>();
        foods.Add(Apple);
        foods.Add(Lemon);
        foods.Add(Orange);
        foods.Add(Peach);
        foods.Add(Pineapple);
        foods.Add(Banana);
        foods.Add(Cherry);
        foods.Add(Watermelon);
        foods.Add(PinkCandy);
        foods.Add(Cookie);
        foods.Add(CandyCane);
        foods.Add(ChocolateCake);
        foods.Add(Chocolate);
        foods.Add(BlueLolipop);
        foods.Add(ChocolateIceCream);
        foods.Add(Honey);
        foods.Add(VanillaIceCream);
        foods.Add(StrawberryCake);
        foods.Add(BlueCandy);
        foods.Add(GreenCandyCane);
        foods.Add(WhiteChocolate);
        foods.Add(GreenLolipop);
        foods.Add(Water);
        foods.Add(Cheese);
        foods.Add(Meat);
        foods.Add(Egg);
        foods.Add(Fish);
        foods.Add(Sandwich);
        foods.Add(Potato);
        foods.Add(Steak);
        foods.Add(Bread);
        foods.Add(Carrot);
        foods.Add(Turnip);
        foods.Add(Tomato);
        foods.Add(PurpleGrape);
        foods.Add(Mushroom);
        foods.Add(JellyDonut);
        foods.Add(Pizza);
        foods.Add(Corn);
        foods.Add(CottonCandy);
        foods.Add(Strawberry);
        foods.Add(Ramen);
        foods.Add(Pudding);
        foods.Add(Curry);
        foods.Add(Pie);
        foods.Add(Wheat);
        foods.Add(Acorn);
        foods.Add(Eggplant);
        foods.Add(Raddish);
        foods.Add(Leek);
        foods.Add(Muffin);
        foods.Add(Jam);

        Func<GameObject> function = foods[UnityEngine.Random.Range(0, foods.Count)];
        return function();
    }
}
