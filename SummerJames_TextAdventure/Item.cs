using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure_SJ_SDG20
{
    class Item
    {
        public string name;
        private bool useable;
        private string description;

        //Constructor setting the name of the item, whether it can be picked up and the description.
        public Item(string n, bool canUse, string d)
        {
            name = n;
            useable = canUse;
            description = d;
        }

        //get the name
        public string Name
        {
            get { return name; }
        }

        //get true or false for pick up
        public bool CanPickUp
        {
            get { return useable; }
        }

        //get description
        public string Description
        {
            get { return description; }
        }

        //To change description of an item
        public void SetDescription(string d)
        {
            description = d;
        }
    }
}
