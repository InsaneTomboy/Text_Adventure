using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure_SJ_SDG20
{
    class Location
    {
        string placeName = "";
        string placeDescription = "";
        List<Item> inventory;

        List<Exit> exits;

        //Constructor for the locations name and description
        public Location(string name, string description)
        {
            placeName = name;
            placeDescription = description;
            exits = new List<Exit>();
            inventory = new List<Item>();
        }

        //Get the locations name
        public string GetName()
        {
            return placeName;
        }

        //Overwrite the description in case of changes or re-entry
        public void SetDescription(string des)
        {
            placeDescription = des;
        }

        //Get the locations description
        public string GetDesription()
        {
            return placeDescription;
        }

        //Get the locations inventory
        public List<Item> GetInventory()
        {
            return new List<Item>(inventory);
        }

        //Add to the locations inventory
        public void ToInventory(Item addItem)
        {
            inventory.Add(addItem);
        }

        //This is for when an item needs to be removed from the inventory, like when the window breaks
        public void RemoveFromInventory(Item RemoveItem)
        {
            if (inventory.Contains(RemoveItem))
            {
                inventory.Remove(RemoveItem);
            }
        }

        //Checks if the given item is in the locations inventory, if so it removes the item from the rooms inventory and returns said item
        public Item GrabItem(string name)
        {
            foreach (Item item in inventory)
            {
                if (item.Name.ToLower() == name)
                {
                    Item itm = item;
                    inventory.Remove(itm);
                    return itm;
                }
            }
            return null;
        }

        //Adds the exit to the exit list
        public void AddExit(Exit exit)
        {
            exits.Add(exit);
        }

        //Removes the exit from the exit list if it exists
        public void RemoveExit(Exit exit)
        {
            if (exits.Contains(exit))
            {
                exits.Remove(exit);
            }
        }

        //Returns all exits
        public List<Exit> GetExits()
        {
            return new List<Exit>(exits);
        }
    }
}