using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure_SJ_SDG20
{
    class Exit
    {
		//All directions
		public enum Directions
		{
			Unknown, Up, Down, Right, Left, Forward, Back
		};

		public static string[] shortDirections = { "Null", "U", "D", "R", "L", "F", "B"};

		Location leadsTo;
		Directions direction;

		//Constructor to set the direction and the location it leads to
        public Exit(Directions d, Location path)
		{
			direction = d;
			leadsTo = path;
		}

		//returns direction as a string so it can be checked and written
        public override string ToString()
        {
            return direction.ToString();
        }

        //For if I decide to show available directions
        public Directions GetDirection()
        {
            return direction;
        }

        //Gets the abbreviation
        public string GetAbbrev()
        {
			//Checks the index of direction and returns the abbreviation with the same index
            return shortDirections[(int)direction].ToLower();
        }

		//Returns the location the exit will lead to
		public Location GetPath()
		{
			return leadsTo;
		}
	}
}
