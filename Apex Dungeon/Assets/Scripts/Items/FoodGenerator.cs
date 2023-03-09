using System;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject CreateFood(string name, string description, Sprite image)
    {
        GameObject item = new GameObject(name);
        string id = "food";
        item.AddComponent<SpriteRenderer>();
        item.GetComponent<SpriteRenderer>().sprite = image;
        item.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        item.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        Consumable food = new Consumable(id, name, description, "Restores 10% HP per turn for 2 turns", image);
        item.AddComponent<Pickup>();
        item.GetComponent<Pickup>().SetItem(food);
        item.tag = "Consumable";
        return item;
    }

    public GameObject Apple()
    {
        return CreateFood("Apple", "Keeps the doctor away!", apple);
    }
    public GameObject Lemon()
    {
        return CreateFood("Lemon", "Pucker up!", lemon);
    }
    public GameObject Orange()
    {
        return CreateFood("Orange", "Orange ya glad you found this", orange);
    }
    public GameObject Peach()
    {
        return CreateFood("Peach", "Looking just peachy.", peach);
    }
    public GameObject Pineapple()
    {
        return CreateFood("Pineapple", "Belongs on pizza.", pineapple);
    }
    public GameObject Banana()
    {
        return CreateFood("Banana", "Try not to slip on it.", banana);
    }
    public GameObject Watermelon()
    {
        return CreateFood("Watermelon", "", watermelon);
    }
    public GameObject Cherry()
    {
        return CreateFood("Cherry", "", cherry);
    }
    public GameObject Cookie()
    {
        return CreateFood("Cookie", "Welcome to the Dark Side.", cookie);
    }
    public GameObject PinkCandy()
    {
        return CreateFood("Pink Candy", "Grandma's hard candy.", pinkCandy);
    }
    public GameObject CandyCane()
    {
        return CreateFood("Candy Cane", "Keepin' it festive.", candyCane);
    }
    public GameObject ChocolateCake()
    {
        return CreateFood("Chocolate Cake", "It's a lie.", chocolateCake);
    }
    public GameObject Chocolate()
    {
        return CreateFood("Chocolate", "You rub it on your skin and it makes you live forever.", chocolate);
    }
    public GameObject BlueLolipop()
    {
        return CreateFood("Blue Lolipop", "", blueLolipop);
    }
    public GameObject ChocolateIceCream()
    {
        return CreateFood("Chocolate Ice Cream", "", chocolateIceCream);
    }
    public GameObject Honey()
    {
        return CreateFood("Honey", "Watch out for bees!", honey);
    }
    public GameObject VanillaIceCream()
    {
        return CreateFood("Vanilla Ice Cream", "", vanillaIceCream);
    }
    public GameObject StrawberryCake()
    {
        return CreateFood("Strawberry Cake", "Berry tasty.", strawberryCake);
    }
    public GameObject BlueCandy()
    {
        return CreateFood("Blue Candy", "Grandpa's hard candy.", blueCandy);
    }
    public GameObject GreenCandyCane()
    {
        return CreateFood("Green Candy Cane", "", greenCandyCane);
    }
    public GameObject WhiteChocolate()
    {
        return CreateFood("White Chocolate", "Not technically chocolate.", whiteChocolate);
    }
    public GameObject GreenLolipop()
    {
        return CreateFood("Green Lolipop", "", greenLolipop);
    }
    public GameObject Water()
    {
        return CreateFood("Water", "Contains high levels of lead", water);
    }
    public GameObject Cheese()
    {
        return CreateFood("Cheese", "Careful, it's extra sharp!", cheese);
    }
    public GameObject Meat()
    {
        return CreateFood("Meat", "Doesn't look like any animal you know...", meat);
    }
    public GameObject Egg()
    {
        return CreateFood("Egg", "Natures mystery box.", egg);
    }
    public GameObject Fish()
    {
        return CreateFood("Fish", "", fish);
    }
    public GameObject Sandwich()
    {
        return CreateFood("Sandwich", "Not quite a foot long.", sandwich);
    }
    public GameObject Potato()
    {
        return CreateFood("Potato", "If only I had butter...", potato);
    }
    public GameObject Steak()
    {
        return CreateFood("Steak", "Extra rare.", rawSteak);
    }
    public GameObject Bread()
    {
        return CreateFood("Bread", "The best thing since...my last meal.", bread);
    }
    public GameObject Carrot()
    {
        return CreateFood("Carrot", "", carrot);
    }
    public GameObject Turnip()
    {
        return CreateFood("Turnip", "", turnip);
    }
    public GameObject Tomato()
    {
        return CreateFood("Tomato", "Good for throwing at bards.", tomato);
    }
    public GameObject PurpleGrape()
    {
        return CreateFood("Purple Grape", "The snack of a king.", purpleGrape);
    }
    public GameObject Mushroom()
    {
        return CreateFood("Mushroom", "It's probably safe to eat.", mushroom);
    }
    public GameObject JellyDonut()
    {
        return CreateFood("JellyDonut", "Has a delicious filling.", riceball);
    }
    public GameObject Pizza()
    {
        return CreateFood("Pizza", "Dungeon style.", pizza);
    }
    public GameObject Corn()
    {
        return CreateFood("Corn", "Look a little under popped.", corn);
    }
    public GameObject CottonCandy()
    {
        return CreateFood("Cotton Candy", "", cottonCandy);
    }
    public GameObject Strawberry()
    {
        return CreateFood("Strawberry", "", strawberry);
    }
    public GameObject Ramen()
    {
        return CreateFood("Ramen", "Cooks instantly.", ramen);
    }
    public GameObject Pudding()
    {
        return CreateFood("Pudding", "Keeps the doctor away!", pudding);
    }
    public GameObject Curry()
    {
        return CreateFood("Curry", "Extra spicy.", curry);
    }
    public GameObject Pie()
    {
        return CreateFood("Pie", "Hopefully its not a bomb.", pie);
    }
    public GameObject Wheat()
    {
        return CreateFood("Wheat", "Is this even food?", wheat);
    }
    public GameObject Acorn()
    {
        return CreateFood("Acorn", "If its good enough for squirrels...", acorn);
    }
    public GameObject Eggplant()
    {
        return CreateFood("Eggplant", "Has a very interesting shape.", eggplant);
    }
    public GameObject Raddish()
    {
        return CreateFood("Raddish", "", raddish);
    }
    public GameObject Leek()
    {
        return CreateFood("Leek", "Good for spinning.", leek);
    }
    public GameObject Muffin()
    {
        return CreateFood("Muffin", "I think it wants to die.", muffin);
    }
    public GameObject Jam()
    {
        return CreateFood("Jam", "", jam);
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
