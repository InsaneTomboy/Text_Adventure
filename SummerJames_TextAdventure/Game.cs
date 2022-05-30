using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure_SJ_SDG20
{
    //Ik heb comments geplaats om de code uit te leggen en goed te laten zien dat ik de code begrijp.
    //Deze zijn in het Engels omdat dit duidelijker is voor mij.
    class Game
    {
        public NPC Orc = new NPC();
        public PlayerState pState;
        public static bool easyMode;

        public bool inCombat = false;
        public static bool monsterDead = false;
        public bool monsterEaten = false;

        Location currentLocation;

        public int cleanerRoute = 0;

        public bool eaten = false;
        public bool cleanedToilet = false;
        public bool cleanedBed = false;
        public bool cleanedKitchen = false;

        public bool gameOver = false;
        public bool isQuiting = false;

        //Ending tracker bools
        public static bool windowEnding = false;
        public static bool cleanerEnding = false;
        public static bool trueEnding = false;
        public static bool doorEnding = false;
        public static bool brickEnding = false;
        public static bool bleachEnding = false;
        public static bool eatingEnding = false;
        public static bool jokeEnding = false;
        public static bool defeatEnding = false;
        public static bool friendsEnding = false;

        public static int playerHealth = 20;
        public static int playerDamage = 2;

        public static int endings = 0;

        List<Item> inventory;

        #region Creating locations
        //This is how locations are created
        Location cell = new Location("Cell", "You wake up in a dark room.\nOn closer inspection, you appear to be in some sort of cell.\nWhat are you doing here?\n" +
                "Your head is throbbing and you can't seem to remember anything\nOn your right is a bed (Why weren't you in it?).\nBehind you is a window,\n" +
                "to your left are the cell bars and in front of you is... A My Little Pony Poster!? What the hell, dude...");

        Location corridor = new Location("Corridor", "You have reached a corridor.\nAll you can see is rows of large...cages?\n" +
            "They sure look more luxurious than the cell you woke up on." +
            "\nThe lights are dull and flickering." +
                "\nThere looks to be a doorway at the end of the hall to your left, with light on the other side.\n" +
            "At the end of the hall on your right is a door." +
            "\nThere is a note on the floor by one of the cages\n");

        Location kitchen = new Location("Kitchen", "The kitchen is huge, obviously meant to feed a lot of people." +
            "\nThere seems to be rotting food everywhere and the smell is unbearable.\nThere are some cleaning supplies on the counter (ironic)" +
            "\nOn the other end is a huge hole in the wall revealing a tunnel, bricks scattered all around it.\n" +
            "The tunnel is dark. I don't know if its a good idea to go in there...\n");

        Location office = new Location("Office", "A small, bright office.\nOn the desk are some notes.\nThere is a trapdoor leading down in the corner.\n" +
            "You probably shouldn't go in there.");

        Location cellar = new Location("Cellar", "Its pretty dark in here, you can't see a whole lot other than some barrels and boxes\n...\nAnd a silhouette of some sort of creature.\n" +
            "You already know what's about to happen.\nI told you not to go in here didn't I?");

        Location bathroom = new Location("Bathroom", "This place smells awful. There's a mirror to your left and locked cubicles on your right.\n" +
            "Only one of them is unlocked and the sight staring back at you leaves you petrified;\nThe toilet looks like it hasn't been cleaned in years!");

        Location tunnel = new Location("Tunnel", "After walking for a while you see light at the end.\n" +
            "I'm not so sure whether going there is a good idea.\n" +
            "I think that if you go back, there will be some other way of escape...");

        Location newFacility = new Location("New Facility", "This place is blinding and your eyes need a minute to adjust\n" +
            "The room is HUGE, but nothing's in it other than a large door in front of you.\nThe door is unlocked.");

        Location cage = new Location("Cage", "The door behind you slams closed.\nAfter the door slams you hear a loud cry.\nIt sounded like a bird?\n" +
            "You look infront of you and you see...\n\n" +
            "A Phoenix!?\n\n" +
            "It's locked up in a small, glass cage.\n" +
            "It's staring at you intently, it looks like it trusts you will help?\n" +
            "If only you had something to help free it.\n" +
            "Looking past the cage you spot a door, it looks like it leads outside.\n" +
            "Freedom...\n" +
            "So, what will you do?");

        Location endOfCorridor = new Location("End of Corridor", "In front of you is a door with a busted lock and to your left is a bathroom.");
        #endregion

        #region Creating Items
        //Creating items
        //I'm too lazy to create items for everything so sometimes it will say something doesn't exist when it clearly does.
        Item window = new Item("window", false, "An old dirty window. It's locked shut. Doesn't look like you'll be able to do much to it.");
        Item poster = new Item("poster", true, "A rather... questionable image is printed on the front.");
        Item bed = new Item("bed", false, "An old rusted bed. The sheets on it are torn and stained. The 'mattress' looks more like cardboard.\nNo wonder you didn't sleep in there.");
        Item noteCorridor = new Item("note", false, "The note reads:\n\n'Centaur\n\nFound injured in the forest.\n" +
            "Take caution, has proven to be aggressive.\n" +
            "Will soon be moved to the new facility to continue rehabilitation.");
        Item brick = new Item("brick", true, "A brick. Just a very simple brick.");
        Item cleaningSupplies = new Item("cleaning supplies", false, "Some basic cleaning stuff like bleach and Mr Clean.\nIs that Jo- you know what never mind.");
        Item bleach = new Item("bleach", true, "It's literally just bleach. What else do you want to know?");
        Item food = new Item("food", true, "I have no idea what this stuff is but God is it gross.");
        Item toilet = new Item("toilet", false, "God this poor thing has been through some rought time.\nIt smells and looks disgusting!");
        Item mirror = new Item("mirror", false, "Oh my this thing has seen better days.\nMaybe if you step out of the way it'll look better...");
        Item brokenMirror = new Item("broken mirror", false, "The mirror is completely shattered, spread all over the floor.");
        Item key = new Item("key", true, "The golden key like object the Orc gave you.\nWhat's it for and why did it give it to you?");
        Item itemCage = new Item("cage", false, "The phoenix is waiting, what will you do?");
        Item door = new Item("door", false, "Upon closer inspection, you see the door does lead outside.\nYou can escape.");
        Item phoenix = new Item("phoenix", false, "A majestic, fiery bird\nIt looks tired and worn out.\nIt is looking at you with great intent.");
        Item orc = new Item("orc", false, "The Orc is still collapsed, sulking in its defeat.\nOh boy it sure is ugly...");
        Item pipe = new Item("pipe", true, "A pipe hidden behind the mirror. I really wonder why it was there...Story convenience, maybe?");
        Item notes = new Item("notes", false, "There are 3 notes\n\nNote 1 reads:\n\n'We made a huge amount of money today, selling those parts.\n" +
            "This business truly was a great idea\nAnd that fool still doesn't suspect a thing.'\n\n" +
            "Note 2 reads:\n\nWe succesfully hunted down that Orc.\nHarvesting seems to be a challenge however, its much stronger that we had anticipated.\n\n" +
            "Note 3 reads:\n\nWe 'moved' that centaur today.\nThe idiot even helped us out, thinking they were doing that thing a favour.\nPathetic.");
        #endregion

        public Game(bool ez)
        {
            Console.Clear();
            easyMode = ez;

            #region Intro and title scene
            //Intro / title screen
            Console.Title = "Caged.";

            Console.Write(@" ██████╗ █████╗  ██████╗ ███████╗██████╗    
██╔════╝██╔══██╗██╔════╝ ██╔════╝██╔══██╗   
██║     ███████║██║  ███╗█████╗  ██║  ██║                                            Created by: Summer James
██║     ██╔══██║██║   ██║██╔══╝  ██║  ██║                                            Class: SDG20
╚██████╗██║  ██║╚██████╔╝███████╗██████╔╝██╗
 ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═════╝ ╚═╝
                                            ");

            Console.WriteLine("\n\n\nWelcome young traveler, to my text adventure game.\n");
            Console.WriteLine("I hope you enjoy playing this broken game more than I enjoyed creating it :)\n");
            Console.WriteLine("Press 'h' or type 'help' to see all the useable commands.\n\nOr you know, just wing it. I can't tell you what to do.\n");
            Console.WriteLine("There are 10 different endings.\n");
            Console.WriteLine("Endings achieved:\n");

            #region Endings achievements
            if (windowEnding)
            {
                Console.WriteLine("A broken window's all that's left");
                endings++;
            }
            if (cleanerEnding)
            {
                Console.WriteLine("Spotless");
                endings++;
            }
            if (trueEnding)
            {
                Console.WriteLine("A good life");
                endings++;
            }
            if (doorEnding)
            {
                Console.WriteLine("What have you done?");
                endings++;
            }
            if (brickEnding)
            {
                Console.WriteLine("Why would you do that?");
                endings++;
            }
            if (bleachEnding)
            {
                Console.WriteLine("Baron Samedi");
                endings++;
            }
            if (eatingEnding)
            {
                Console.WriteLine("Gluttony");
                endings++;
            }
            if (jokeEnding)
            {
                Console.WriteLine("Nice.");
                endings++;
            }
            if (defeatEnding)
            {
                Console.WriteLine("Better luck next time");
                endings++;
            }
            if (friendsEnding)
            {
                Console.WriteLine("Best friends forever");
                endings++;
            }
            if (endings >= 10)
            {
                Console.WriteLine("Congratulations! you have achieved every ending!");
                Console.WriteLine("Your reward is:\n\nNothing.");
            }
            if (endings <= 0)
            {
                Console.WriteLine("None");
            }
            #endregion

            Console.WriteLine("\n\nPress 'enter' to continue.");
            Console.ReadKey();
            Console.Clear();
            #endregion

            inventory = new List<Item>();

            #region Adding things to location inventories and adding initial exits
            //Adding items to the rooms inventory
            cell.ToInventory(window);
            cell.ToInventory(bed);
            cell.ToInventory(poster);

            corridor.ToInventory(noteCorridor);

            cellar.ToInventory(orc);

            kitchen.ToInventory(brick);
            kitchen.ToInventory(cleaningSupplies);
            kitchen.ToInventory(bleach);
            kitchen.ToInventory(food);

            bathroom.ToInventory(toilet);
            bathroom.ToInventory(mirror);

            cage.ToInventory(itemCage);
            cage.ToInventory(door);
            cage.ToInventory(phoenix);

            office.ToInventory(notes);

            //Setting room exits

            corridor.AddExit(new Exit(Exit.Directions.Back, cell));
            corridor.AddExit(new Exit(Exit.Directions.Left, kitchen));
            corridor.AddExit(new Exit(Exit.Directions.Right, endOfCorridor));

            endOfCorridor.AddExit(new Exit(Exit.Directions.Forward, office));
            endOfCorridor.AddExit(new Exit(Exit.Directions.Left, bathroom));
            endOfCorridor.AddExit(new Exit(Exit.Directions.Back, corridor));

            office.AddExit(new Exit(Exit.Directions.Back, endOfCorridor));
            office.AddExit(new Exit(Exit.Directions.Down, cellar));

            cellar.AddExit(new Exit(Exit.Directions.Up, office));

            bathroom.AddExit(new Exit(Exit.Directions.Back, endOfCorridor));

            kitchen.AddExit(new Exit(Exit.Directions.Back, corridor));
            kitchen.AddExit(new Exit(Exit.Directions.Forward, tunnel));

            tunnel.AddExit(new Exit(Exit.Directions.Back, kitchen));
            tunnel.AddExit(new Exit(Exit.Directions.Forward, newFacility));

            newFacility.AddExit(new Exit(Exit.Directions.Back, tunnel));
            newFacility.AddExit(new Exit(Exit.Directions.Forward, cage));

            cage.AddExit(new Exit(Exit.Directions.Back, newFacility));
            #endregion

            currentLocation = cell;

            ShowLocation();
        }

        public void ShowLocation()
        {
            //Showing the location name
            Console.WriteLine($"\n{currentLocation.GetName()}\n");
            //Showing the location description
            Console.WriteLine(currentLocation.GetDesription());

            #region Show items and exits of location for easy mode

            //I had feedback that things could sometimes be very unclear so I added an easy mode
            //I don't want to make things obvious as the whole point of the game is exploring and trial and error

            if (easyMode)
            {
                //Checking if there is something in the room
                if (currentLocation.GetInventory().Count > 0)
                {
                    Console.WriteLine("\nThe room contains:\n");

                    for (int i = 0; i < currentLocation.GetInventory().Count; i++)
                    {
                        //Showing the names of all items in the rooms inventory
                        Console.WriteLine(currentLocation.GetInventory()[i].Name);
                    }
                }

                //Showing available exits
                Console.WriteLine("\nAvailable Exits: \n");

                foreach (Exit exit in currentLocation.GetExits())
                {
                    //Shows the name of all exits for the room
                    Console.WriteLine(exit.GetDirection());
                }

                //If there are no exits
                if (currentLocation.GetExits().Count == 0)
                {
                    Console.WriteLine("\nNone\n");
                }
            }
            #endregion

            Console.WriteLine();
        }

        //Input handling
        public void DoAction(string input)
        {
            #region Route check
            if (cleanerRoute >= 3)
            {
                //This could easily be expanded on by setting a new current location, making multiple dimensions and story paths easy to implement and playable.
                Console.WriteLine("\n'Hold that thought!'\nYou hear a loud voice scream out of no where\n" +
                    "Suddenly a short male appears infront of you.\n" +
                    "He is wearing a green hood with blue and white wings printed on the back?\n" +
                    "'You!' He says in a stern voice while pointing directly at you.\n" +
                    "So much authority in his voice...\n" +
                    "'You will work for me!' Wait, wha-\n" +
                    "Before you can object, he knocks you unconscious.\n");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("You wake up in a bright room.\n" +
                    "You look outside, all you see are houses and...WALLS?\n" +
                    "Those walls must be at least 50 meters high!\n" +
                    "'Good, you're awake.'\n" +
                    "It's the male from before.\n" +
                    "'Here' he says while handing you cleaning supplies\n" +
                    "'I want this whole place spotless when I come back from killing titans.'\n" +
                    "He walks out the door and you are left alone, cleaning supplies in hand.\n" +
                    "This is your life now.");
                cleanerEnding = true;
                gameOver = true;
                return;
            }

            if (currentLocation.GetName().ToLower() == "cellar" && Orc.GetState() != NPCState.Dead && !monsterEaten)
            {
                inCombat = true;
            }
            if (inCombat)
            {
                PlayCombat();
                return;
            }
            if (monsterDead)
            {
                cellar.SetDescription("Its pretty dark in here, you can't see a whole lot other than some barrels and boxes\n...\n" +
            "And of course the collapsed Orc\n\nOopsie...?");

                Console.WriteLine("As you go in to deliver the finishing blow to the defeated Orc, it looks up at you desperately.\n" +
                    "You pause, not having the heart to kill something so defenseless.\n" +
                    "The Orc uses the last of its strength to turn around and grab something from behind it.\n" +
                    "It turns back to you and hands you a...key?\n\n" +
                    "'What's this for?' You ask, but the Orc refuses to elaborate\n" +
                    "\nThat's on you, what did you expect?\n" +
                    "A nice little conversation with an Orc?\n");

                inventory.Add(key);
                //This is so this only triggers once
                monsterDead = false;
                return;
            }
            #endregion

            #region Help input
            if (input == "help" || input == "h")
            {
                Console.Clear();
                Console.WriteLine("-------------------WELCOME MY BOY-------------------\n\n");
                Console.WriteLine("'look' / 'look around':        Gives a description of the room.\n");
                Console.WriteLine("'Look at X':         Gives you information about the specified item (x) in the room or your inventory.\n");
                Console.WriteLine("'pick up X':         Attempts to pick up the specified item (x).\n");
                Console.WriteLine("'use X':             Attempts to use the specified item (x).\n" +
                    "                     After this there will be text asking 'Use what?'\n" +
                    "                     Here, just input the item you wish to use x on.\n");
                Console.WriteLine("'i' / 'inventory':   Allows you to see the items in your inventory.\n");
                Console.WriteLine("'q' / 'quit':        Quits the game. Nothing bad will happen if you do this, I promise :)\n");
                //Threatening you
                Console.WriteLine(@"──────▄▀▀▄────────────────▄▀▀▄────
─────▐▒▒▒▒▌──────────────▌▒▒▒▒▌───
─────▌▒▒▒▒▐─────────────▐▒▒▒▒▒▐───
────▐▒▒▒▒▒▒▌─▄▄▄▀▀▀▀▄▄▄─▌▒▒▒▒▒▒▌──
───▄▌▒▒▒▒▒▒▒▀▒▒▒▒▒▒▒▒▒▒▀▒▒▒▒▒▒▐───
─▄▀▒▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌───
▐▒▒▒▌▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▐───
▌▒▒▌▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌──
▒▒▐▒▒▒▒▒▒▒▒▒▄▀▀▀▀▄▒▒▒▒▒▄▀▀▀▀▄▒▒▐──
▒▒▌▒▒▒▒▒▒▒▒▐▌─▄▄─▐▌▒▒▒▐▌─▄▄─▐▌▒▒▌─
▒▐▒▒▒▒▒▒▒▒▒▐▌▐█▄▌▐▌▒▒▒▐▌▐█▄▌▐▌▒▒▐─
▒▌▒▒▒▒▒▒▒▒▒▐▌─▀▀─▐▌▒▒▒▐▌─▀▀─▐▌▒▒▒▌
▒▌▒▒▒▒▒▒▒▒▒▒▀▄▄▄▄▀▒▒▒▒▒▀▄▄▄▄▀▒▒▒▒▐
▒▌▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄▄▄▒▒▒▒▒▒▒▒▒▒▒▐
▒▌▒▒▒▒▒▒▒▒▒▒▒▒▀▒▀▒▒▒▀▒▒▒▀▒▀▒▒▒▒▒▒▐
▒▌▒▒▒▒▒▒▒▒▒▒▒▒▒▀▒▒▒▄▀▄▒▒▒▀▒▒▒▒▒▒▒▐
▒▐▒▒▒▒▒▒▒▒▒▒▀▄▒▒▒▄▀▒▒▒▀▄▒▒▒▄▀▒▒▒▒▐
▒▓▌▒▒▒▒▒▒▒▒▒▒▒▀▀▀▒▒▒▒▒▒▒▀▀▀▒▒▒▒▒▒▐
▒▓▓▌▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌
▒▒▓▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌─
▒▒▓▓▀▀▄▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▐──
▒▒▒▓▓▓▓▓▀▀▄▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄▄▀▀▒▌─
▒▒▒▒▒▓▓▓▓▓▓▓▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▒▒▒▒▒▐─
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌
▒▒▒▒▒▒▒█▒█▒█▀▒█▀█▒█▒▒▒█▒█▒█▒▒▒▒▒▒▐
▒▒▒▒▒▒▒█▀█▒█▀▒█▄█▒▀█▒█▀▒▀▀█▒▒▒▒▒▒▐
▒▒▒▒▒▒▒▀▒▀▒▀▀▒▀▒▀▒▒▒▀▒▒▒▀▀▀▒▒▒▒▒▒▐
█▀▄▒█▀▄▒█▀▒█▀█▒▀█▀▒█▒█▒█▒█▄▒█▒▄▀▀▐
█▀▄▒█▀▄▒█▀▒█▄█▒▒█▒▒█▀█▒█▒█▀██▒█▒█▐
▀▀▒▒▀▒▀▒▀▀▒▀▒▀▒▒▀▒▒▀▒▀▒▀▒▀▒▒▀▒▒▀▀▐
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▐");
                Console.WriteLine();
                Console.WriteLine("\nDirections can be input as either the full word or the abbreviation.\n\nDirections are forward, back, left, right, up and down.\n");
                Console.WriteLine("There may be some secret inputs available but I guess you'll just have to find those yourself. :P");
                Console.WriteLine("Sometimes you may have to press enter to continue a part of the story.\n");
                Console.WriteLine("Have fun!");
                return;
            }
            #endregion

            #region inventory input
            //Accessing the player's inventory
            if (input == "inventory" || input == "i")
            {
                ShowInventory();
                Console.WriteLine();
                return;
            }
            #endregion

            #region Pick up input
            //Checking for pick up 
            //Using substring to be able to check for specific inputs in this case checking from position 0 to 7
            if (input.Length >= 7 && input.Substring(0, 7) == "pick up")
            {
                //If nothing is specified
                if (input == "pick up")
                {
                    Console.WriteLine("\nPick up what?\n");
                    return;
                }

                //For some reason it will not check if substring(8) is orc and will execute the if statement regardless 
                if (input.Substring(8) == "orc" && currentLocation.GetInventory().Exists(x => x.Name == input.Substring(8)) && Orc.GetState() == NPCState.Dead && !monsterEaten)
                {
                    Console.WriteLine("\nYou pick up and cradle the sad Orc in your arms.\n" +
                        "You comfort the Orc all night, getting to know each other and becoming great friends.\n" +
                        "When it comes time for you to leave, the Orc begs you to stay.\n" +
                        "How could you say no to such a great friend?\n\n" +
                        "You decide to stay and live your life together with the Orc in the basement.\n" +
                        "You are happy.\n");
                    friendsEnding = true;
                    gameOver = true;
                    return;
                }

                //Checking if the specified item exists in the room's inventory                              
                for (int i = 0; i < currentLocation.GetInventory().Count; i++)
                {
                    //Checking if current item is equal to the input string and that pick up is true
                    //Using substring to check as of position 8 in the string
                    if (currentLocation.GetInventory()[i].CanPickUp == true && currentLocation.GetInventory()[i].Name == input.Substring(8))
                    {
                        //Adding it to the player inventory which also removes it from the location inventory
                        inventory.Add(currentLocation.GrabItem(input.Substring(8)));
                        Console.WriteLine($"\nYou pick up the {input.Substring(8)}.\n");

                        //Checking for specific inputs to further in the story
                        //These will only happen once due to the item being removed from the location inventory
                        if (input.Substring(8) == "poster")
                        {
                            Console.WriteLine("\nPicking up the poster has revealed a small passage in the wall, there is light on the other side.\n");
                            //This adds the corridor as an exit only when the poster has been picked up
                            cell.AddExit(new Exit(Exit.Directions.Forward, corridor));
                            cell.SetDescription("The dark room you woke up in.\n" +
                            "On your right is a bed, behind you is a window,\n" +
                            "to your left are the cell bars and in front of you is a passage in the wall, leading to the corridor.");
                        }
                        if (input.Substring(8) == "pipe")
                        {
                            Console.WriteLine("\nThis will probably come in handy later...\n");
                            playerDamage = 4;
                        }
                        break;
                    }
                    //Checking if the item is equal to the name of current item in the loop and also checking if pick up is false
                    else if (currentLocation.GetInventory()[i].CanPickUp == false && currentLocation.GetInventory()[i].Name == input.Substring(8))
                    {
                        Console.WriteLine($"\nYou cannot pick up the {input.Substring(8)}.\n");
                        break;
                    }
                    else if (!currentLocation.GetInventory().Exists(x => x.Name == input.Substring(8)) && inventory.Exists(x => x.Name == input.ToLower().Substring(8)))
                    {
                        Console.WriteLine($"\n{input.Substring(8)} is already in your inventory!\n");
                        break;
                    }
                    //Using lambda to check if x (item) exists in the inventory or not
                    //If not, the for loop breaks but if it does exist the for loop continues to the next item
                    else if (!currentLocation.GetInventory().Exists(x => x.Name == input.Substring(8)))
                    {
                        Console.WriteLine($"\n{input.Substring(8)} does not exist.\n");
                        break;
                    }
                }
                return;
            }
            #endregion

            #region Look around input
            if (input == "look" || input == "look around")
            {
                ShowLocation();
                //Checking if there is anything in the rooms inventory
                if (currentLocation.GetInventory().Count == 0)
                {
                    Console.WriteLine("There seems to be nothing of interest in the room.\n");
                }
                return;
            }
            #endregion

            #region Look at item input
            //Checking for look at // uses similar methods to pick up
            if (input.Length >= 7 && input.Substring(0, 7) == "look at")
            {
                //If nothing is specified
                if (input == "look at")
                {
                    Console.WriteLine("\nLook at what?\n");
                    return;
                }
                if (currentLocation.GetInventory().Exists(x => x.Name == input.Substring(8)))
                {
                    //Using one simple line to find the item with a name equal to the substring and output the description
                    Console.WriteLine($"\n{currentLocation.GetInventory().Find(x => x.Name == input.Substring(8)).Description}\n");
                    return;
                }
                //Checking if item exists in the inventory
                else if (inventory.Exists(x => x.Name == input.ToLower().Substring(8)))
                {
                    Console.WriteLine($"\n{inventory.Find(x => x.Name == input.Substring(8)).Description}\n");
                    return;
                }
                else
                {
                    Console.WriteLine($"\n{input.ToLower().Substring(8)} does not exist\n\nOr I just haven't declared it as an item...\n");
                    return;
                }
            }
            #endregion

            #region Drink and eat input
            //Hidden inputs
            //Same methods 
            if (input.Length >= 5 && input.Substring(0, 5) == "drink")
            {
                if (input == "drink")
                {
                    Console.WriteLine("\nDrink what?\n");
                    return;
                }

                if (inventory.Exists(x => x.Name == input.Substring(6)))
                {
                    if (input.Substring(6) == "bleach")
                    {
                        Console.WriteLine("\nWhat is wrong with you?\n\nIs my game so bad that you're gonna resort to that to end it?\nFine.\n");
                        Console.WriteLine("You down the bleach.\nYour throat feels like it's on fire but you finish the bottle anyway.\n\n" +
                            "Your vision starts to go hazy but before everything goes black, you see 6 men approach you.\n" +
                            "They're carrying some sort of wooden box.\n\n" +
                            "All of a sudden, music starts playing.\n" +
                            "Astronomia.\n" +
                            "The men start dancing as they approach you and everything goes black." +
                            "\n\nWell, you got what you wanted.");
                        bleachEnding = true;
                        gameOver = true;
                    }
                    return;
                }
                else if (currentLocation.GetInventory().Exists(x => x.Name == input.Substring(6)))
                {
                    Console.WriteLine($"\nYou cannot drink the {input.Substring(6)}.");
                    return;
                }
                else
                {
                    Console.WriteLine($"{input.Substring(6)} does not exist.");
                    return;
                }
            }

            if (input.Length >= 3 && input.Substring(0, 3) == "eat")
            {
                if (input == "eat")
                {
                    Console.WriteLine("\nEat what?\n");
                    return;
                }

                if (inventory.Exists(x => x.Name == input.Substring(4)))
                {
                    if (input.Substring(4) == "food")
                    {
                        Console.WriteLine("\nYou eat the rotten food.\n\nI'm not sure that was a good idea...\n");
                        inventory.Remove(food);
                        eaten = true;
                    }
                    return;
                }
                else if (currentLocation.GetInventory().Exists(x => x.Name == input.Substring(4)))
                {
                    Console.WriteLine($"\nYou cannot eat the {input.Substring(4)}.");
                    return;
                }
                else
                {
                    Console.WriteLine($"{input.Substring(4)} does not exist.");
                    return;
                }
            }
            #endregion

            #region Use input
            //Checking for use // Uses same methods as pick up and look at
            if (input.Length >= 3 && input.Substring(0, 3) == "use")
            {
                if (input == "use")
                {
                    Console.WriteLine("\nUse what?\n");
                    return;
                }
                if (inventory.Exists(x => x.Name == input.ToLower().Substring(4)))
                {
                    //This could be changed to work in just one line using multiple substrings but this works fine too
                    if (input.Substring(4) == "brick")
                    {
                        Console.WriteLine($"\nUse {input.Substring(4)} on?\n");
                        string target = Console.ReadLine();
                        if (currentLocation.GetInventory().Exists(x => x.Name == target))
                        {
                            if (target.Contains("window"))
                            {
                                Item smashedWindow = new Item("broken window", false, "The window is completely shattered, leaving a clear way out.");
                                currentLocation.ToInventory(smashedWindow);
                                foreach (Item item in currentLocation.GetInventory())
                                {
                                    if (item.Name.ToLower() == "window")
                                    {
                                        currentLocation.RemoveFromInventory(item);
                                        break;
                                    }
                                }
                                Console.WriteLine("\nYou smash in the window.\n");
                                return;
                            }
                            else if (target.Contains("mirror"))
                            {
                                Console.WriteLine("You smash the mirror to pieces, that's 7 years of bad luck for you!\n\n" +
                                    "Behind the mirror was a cavity in the wall, in it is a pipe.\n" +
                                    "Weird...");
                                bathroom.ToInventory(pipe);
                                bathroom.ToInventory(brokenMirror);
                                bathroom.RemoveFromInventory(mirror);
                                if (!cleanedToilet)
                                {
                                    bathroom.SetDescription("This place smells awful. The broken mirror is on your left and locked cubicles on your right.\n" +
                                    "Only one of them is unlocked and the sight staring back at you leaves you petrified;\nThe toilet looks like it hasn't been cleaned in years!");
                                }
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Not gonna happen chief.");
                                return;
                            }
                        }
                        else if (target.Contains("self"))
                        {
                            Console.WriteLine("Why would you-\n\nYou know what, who am I to stop you?");
                            Console.WriteLine("\n\nYou dropped the brick on yourself and now you ded.");
                            brickEnding = true;
                            gameOver = true;
                            return;
                        }
                        else
                        {
                            //Not gonna happen chief
                            Console.WriteLine("Not gonna happen chief.");
                            return;
                        }
                    }
                    if (input.Substring(4) == "key")
                    {
                        Console.WriteLine($"\nUse {input.Substring(4)} on?\n");
                        string target = Console.ReadLine();
                        if (currentLocation.GetInventory().Exists(x => x.Name == target))
                        {
                            if (target.Contains("cage"))
                            {
                                #region True ending
                                Console.WriteLine("\nYou open the cage.\n\n" +
                                    "The phoenix quickly escapes and flies up high to stretch its wings.\n" +
                                    "You stand there mesmerized, taking in what is happening.\n" +
                                    "The phoenix flies towards you at great speed.\n\n" +
                                    "You close your eyes, is this what you get for being kind?\n\n" +
                                    "After a while of nothing, you open your eyes and see the phoenix hovering in front of you.\n" +
                                    "Suddenly, all your memories come flooding back.\n");
                                Console.ReadKey();
                                Console.WriteLine("That's right...\n");
                                Console.ReadKey();
                                Console.WriteLine("I remember now.");
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("You were an employee at a mythical creature rescue and rehabilitation centre.\n\n" +
                                    "That is, until you found out what was really going on...\n");
                                Console.ReadKey();
                                Console.WriteLine("They would send the beings to the 'New Facility' to continue their rehabilitation and treatment.\n" +
                                    "Or so they said, anyway.\n\n" +
                                    "But the truth is, they'd exploit the phoenix's tears, using their extraordinary healing powers to continously\n" +
                                    "farm other beings for their prized body parts.\n" +
                                    "Griffin wings, beaks and claws, unicorn horns and blood.\n" +
                                    "Hell, even Orc teeth sell for a fortune.\n");
                                Console.ReadKey();
                                Console.WriteLine("When you found out you were furious, and exposed their whole scheme.\n" +
                                    "After all, this place was supposed to rescue these magnificent creatures from poachers and other dangers.\n" +
                                    "That was all you wanted to do, help these beings.\n" +
                                    "Not be a part of this horrific torture.\n");
                                Console.ReadKey();
                                Console.WriteLine("When they found out that you exposed the whole operation, they threw you into a cell.\n\n" +
                                    "But it was too late.\n\n" +
                                    "You had already told the police of everything and in no time the whole place was raided, leaving you locked up.\n" +
                                    "But it was worth it, knowing you managed to save any other creatures from harm.\n" +
                                    "And now you've even saved the phoenix...well done.\n\n" +
                                    "'Thank you' you gratefully thank the phoenix, it restored your memories after all.\n\n" +
                                    "The phoenix gives you one last acknowledging glance, before flying out of the door.\n" +
                                    "You follow it, going outside.\n" +
                                    "You take a deep breath.\n\n" +
                                    "This is gonna be a good life.\n");
                                #endregion

                                trueEnding = true;
                                gameOver = true;
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Not gonna happen chief.");
                                return;
                            }
                        }
                    }
                    Console.WriteLine($"\nUse {input.Substring(4)} on?\n");
                    return;
                }

                if (currentLocation.GetInventory().Exists(x => x.Name == input.ToLower().Substring(4)))
                {
                    if (input.ToLower().Substring(4) == "window")
                    {
                        Console.WriteLine("\nThe window's locked tight, with no way of opening it.\n");
                        return;
                    }
                    if (input.ToLower().Substring(4) == "poster")
                    {
                        Console.WriteLine("\nWhat exactly do you wish to achieve with that?\n");
                        return;
                    }
                    if (input.ToLower().Substring(4) == "broken window")
                    {
                        Console.WriteLine("\nYou quickly clamber out of the window. You aren't sticking around this place any longer.\nYou run and run until you find a small village." +
                        "\nYou start a new life and continue your collection of My Little Pony merchandise.\nBut you are always left wondering what would have happened if you had stuck around that place...\n");
                        windowEnding = true;
                        gameOver = true;
                        return;
                    }
                    if (input.ToLower().Substring(4) == "door")
                    {
                        Console.Write("\nYou quickly run through the door without a second thought, survival of the fittest after all.\n" +
                            "You build up a new life but it is always filled with regret.\n" +
                            "You never got back any of your memories and your life feels unfulfilled.\n" +
                            "Maybe you made the wrong choice\n");
                        doorEnding = true;
                        gameOver = true;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nWhat are you trying to use? The air?\n");
                    return;
                }
            }
            #endregion

            #region Cleaner stuff
            //I want this to be secret so if the wrong item is given it will look like clean is an invalid input
            if (input.Length >= 5 && input.Substring(0, 5) == "clean")
            {
                if (inventory.Exists(x => x.Name == "bleach"))
                {
                    if (input == "clean")
                    {
                        Console.WriteLine("\nInvalid command, are you confused?\n");
                        return;
                    }

                    if (input.Substring(6) == "toilet" && currentLocation.GetInventory().Exists(x => x.Name == input.Substring(6)) && !cleanedToilet)
                    {
                        Console.WriteLine("\nYou scrub and scrub the toilet\nYou're a brave soul.\n" +
                            "After scrubbing for what feels like forever, the toilet is left shining.\n" +
                            "'Thank you.' A soft voice mutters in your ear.\n" +
                            "'No problem.' you utter back, grinning like a fool.\n" +
                            "After what seems like forever, the situation finally dawns on you.\n" +
                            "who. the. hell. said. that?\n");
                        bathroom.SetDescription("This place smells blissful, you did a great job cleaning it.\n" +
                            "The toilet is shining at you and all you feel is joy.\n");
                        toilet.SetDescription("You could eat off it.");
                        cleanedToilet = true;
                        cleanerRoute++;
                        return;
                    }
                    else if (input.Substring(6) == "toilet" && currentLocation.GetInventory().Exists(x => x.Name == input.Substring(6)) && cleanedToilet)
                    {
                        Console.WriteLine("\nCalm down there, it's already clean enough!\n");
                        return;
                    }
                    if (input.Substring(6) == "bed" && currentLocation.GetInventory().Exists(x => x.Name == input.Substring(6)) && !cleanedBed)
                    {
                        Console.WriteLine("\nYou thoroughly clean your bed.\nAre you planning to sleep in it or something?\n" +
                            "Oh well, I've always wanted to sleep on cardboard drenched in bleach.\n");
                        bed.SetDescription("A bleach drenched bed, but at leasts it's clean...");
                        cleanedBed = true;
                        cleanerRoute++;
                        return;
                    }
                    else if (input.Substring(6) == "bed" && currentLocation.GetInventory().Exists(x => x.Name == input.Substring(6)) && cleanedBed)
                    {
                        Console.WriteLine("\nCalm down there, it's already clean enough!\n");
                        return;
                    }
                    if (input.Substring(6) == "kitchen" && currentLocation.GetName().ToLower() == input.Substring(6) && !cleanedKitchen)
                    {
                        Console.WriteLine("\nYou clean that kitchen like Gordon Ramsay is your boss.\nIt's like you were born to be a housewife.\n" +
                            "The kitchen is left spotless, nothing like the smell of some fresh bleach.\n");
                        kitchen.SetDescription("The kitchen is huge, obviously meant to feed a lot of people." +
            "\nThe floors are now shining brightly.\nThere are some cleaning supplies on the counter, well you put those to good use!" +
            "\nOn the other end is a huge hole in the wall revealing a tunnel, bricks scattered all around it.\n" +
            "The tunnel is dark. I don't know if its a good idea to go in there...");
                        cleanedKitchen = true;
                        cleanerRoute++;
                        return;
                    }
                    else if (input.Substring(6) == "kitchen" && currentLocation.GetName().ToLower() == input.Substring(6) && cleanedKitchen)
                    {
                        Console.WriteLine("\nCalm down there, it's already cleaned enough!\n");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid command, are you confused?\n");
                    return;
                }
            }
            #endregion

            #region Other stuff
            //Nice.
            if (input == "69")
            {
                Console.WriteLine("Nice.");
                jokeEnding = true;
                gameOver = true;
                return;
            }
            #endregion

            if (input.ToLower() == "hp" || input.ToLower() == "health")
            {
                Console.WriteLine($"You have {playerHealth} HP.");
                return;
            }

            if (ChangeLocation(input))
                return;

            Console.WriteLine("\nInvalid command, are you confused?\n");
        }

        private bool ChangeLocation(string command)
        {
            foreach (Exit exit in currentLocation.GetExits())
            {
                if (command == exit.ToString().ToLower() || command == exit.GetAbbrev().ToLower())
                {
                    currentLocation = exit.GetPath();
                    Console.WriteLine($"\nYou move {exit.ToString().ToLower()} to the:\n");
                    ShowLocation();
                    return true;
                }
            }
            return false;
        }

        private void ShowInventory()
        {
            if (inventory.Count > 0)
            {
                Console.WriteLine("\nYour inventory contains:\n");

                foreach (Item item in inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            else
            {
                Console.WriteLine("\nLmao you're empty handed.\n");
            }
        }

        public void PlayCombat()
        {
            if (inCombat)
            {
                Console.WriteLine("\nUpon taking a closer look, you see the silhouette was an Orc!\nThere's no way you can get away unnoticed...\n");

                //Makes the game play while monster isn't dead
                while (Orc.State != NPCState.Dead && !gameOver && inCombat)
                {
                    ChooseOptionCombat();
                }

                inCombat = false;
            }
        }

        #region Combat stuff
        public void ChooseOptionCombat()
        {
            NPCState mState = Orc.GetState();

            switch (mState)
            {
                //While monster state is idle
                case NPCState.Idle:
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("\nYou have ran out of strength and are defeated, better luck next time...\n");
                        defeatEnding = true;
                        gameOver = true;
                        break;
                    }
                    Console.WriteLine("Will you [c]harge, [w]ait or [s]neak closer?\n");
                    var choice = Console.ReadLine().ToLower();
                    if (Char.TryParse(choice, out char output))
                    {
                        if (output == 'c')
                        {
                            //Checking if food was eaten for a dumb ending
                            if (eaten)
                            {
                                Console.WriteLine("\nJust as you are about to charge at the Orc, you start feeling super sick and dizzy.\n" +
                                    "It was definitely the food.\n" +
                                    "You should have listened to me.\n" +
                                    "You collapse onto the floor and just before you pass out, you see the Orc approach you.\n");
                                Console.WriteLine("This is your end.\n");
                                eatingEnding = true;
                                gameOver = true;
                                break;
                            }
                            //Sets the monsters state to attacking
                            Orc.StartAttacking();

                            Console.WriteLine("As soon as you charge at the Orc it turns and readies its weapons!\nPrepare to fight!\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else if (output == 'w')
                        {
                            //Sets the monsters state to patrolling
                            Orc.StartPatrolling();

                            Console.WriteLine("You wait so you can observe the Orc more.\nIt gets up and starts to patrol the area.\n" +
                                "Let's hope it doesn't get too close.\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else if (output == 's')
                        {
                            Console.WriteLine("In your attempt to sneak closer, you accidentally trip over some stones on the ground.\n" +
                                "Smooth, well done!\nYou cautiously look to see if the monster has heard you.\n" +
                                "\nYou should try being more careful next time, honestly.\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice, please try again!\n");
                            ChooseOptionCombat();
                        }
                    }
                    else
                    {
                        if (choice == "give food" && inventory.Exists(x => x.Name == "food") || choice == "throw food" && inventory.Exists(x => x.Name == "food"))
                        {
                            Console.WriteLine("\nYou throw the rotten food at the Orc\n" +
                                "The Orc quickly grabs the food and starts to scoff it down\n" +
                                "The Orc seems very pleased and reaches into its pockets.\n" +
                                "After searching in its pockets for a bit, it throws something to you.\n" +
                                "You quickly catch the item and look at it.\n" +
                                "A key?\n" +
                                "You look up to ask the Orc more but the Orc is too busy eating its delicious meal.\n" +
                                "Oh well...");

                            cellar.SetDescription("Its pretty dark in here, you can't see a whole lot other than some barrels and boxes\n...\n" +
                                 "And of course a happy Orc munching away\n\nHow adorable...");
                            orc.SetDescription("The Orc is happily munching away at the food you gave it.\nBut oh boy it sure is ugly...");
                            inventory.Remove(food);
                            monsterEaten = true;
                            inCombat = false;
                            break;
                        }
                        Console.WriteLine("Invalid input, try again.");
                        return;
                    }
                    break;

                //While monster state is patrolling
                case NPCState.Patrolling:
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("\nYou have ran out of strength and are defeated, better luck next time...\n");
                        defeatEnding = true;
                        gameOver = true;
                        break;
                    }
                    Console.WriteLine("The Orc is patrolling, it hasn't detected you yet.");
                    Console.WriteLine("Will you [c]harge, [w]ait or [s]neak closer?\n");
                    var choiceSec = Console.ReadLine().ToLower();
                    if (Char.TryParse(choiceSec, out char choice2))
                    {
                        if (choice2 == 'c')
                        {
                            if (eaten)
                            {
                                Console.WriteLine("\nJust as you are about to charge at the Orc, you start feeling super sick and dizzy.\n" +
                                    "It was definitely the food.\n" +
                                    "You should have listened to me.\n" +
                                    "You collapse onto the floor and just before you pass out, you see the Orc approach you.\n");
                                Console.WriteLine("This is your end.\n");
                                eatingEnding = true;
                                gameOver = true;
                                break;
                            }
                            //Sets the monsters state to attacking
                            Orc.StartAttacking();

                            Console.WriteLine("As soon as you charge at the Orc it turns and readies its weapons!\nPrepare to fight!\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else if (choice2 == 'w')
                        {
                            //Sets the monsters state to patrolling
                            Orc.StartPatrolling();

                            Console.WriteLine("You wait so you can observe the Orc more.\nIt gets up and starts to patrol the area.\n" +
                                "Let's hope it doesn't get too close.\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else if (choice2 == 's')
                        {
                            Console.WriteLine("In your attempt to sneak closer, you accidentally trip over some stones on the ground.\n" +
                                "Smooth, well done!\nYou cautiously look to see if the monster has heard you.\n" +
                                "\nYou should try being more careful next time, honestly.\n");

                            //This decides what the monster does
                            Orc.TakeTurn(pState);
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice, please try again!\n");
                            ChooseOptionCombat();
                        }
                    }
                    else
                    {
                        if (choiceSec == "give food" && inventory.Exists(x => x.Name == "food") || choiceSec == "throw food" && inventory.Exists(x => x.Name == "food"))
                        {
                            Console.WriteLine("\nYou throw the rotten food at the Orc\n" +
                                "The Orc quickly grabs the food and starts to scoff it down\n" +
                                "The Orc seems very pleased and reaches into its pockets.\n" +
                                "After searching in its pockets for a bit, it throws something to you.\n" +
                                "You quickly catch the item and look at it.\n" +
                                "A key?\n" +
                                "You look up to ask the Orc more but the Orc is too busy eating its delicious meal.\n" +
                                "Oh wel...");

                            cellar.SetDescription("Its pretty dark in here, you can't see a whole lot other than some barrels and boxes\n...\n" +
                                 "And of course a happy Orc munching away\n\nHow adorable...");
                            orc.SetDescription("The Orc is happily munching away at the food you gave it.\nBut oh boy it sure is ugly...");
                            inventory.Remove(food);
                            monsterEaten = true;
                            inCombat = false;
                            break;
                        }
                        Console.WriteLine("Invalid input, try again.");
                        return;
                    }
                    break;

                //While monster state is attacking
                case NPCState.Attacking:
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("\nYou have ran out of strength and are defeated, better luck next time...\n");
                        defeatEnding = true;
                        gameOver = true;
                        break;
                    }
                    Console.WriteLine("The Orc moves in for the attack!\n");
                    Console.WriteLine("Will you attempt to [s]trike, [p]arry or [d]odge?\n");
                    var choiceThir = Console.ReadLine().ToLower();

                    if (Char.TryParse(choiceThir, out char choice3))
                    {
                        if (choice3 == 's')
                        {
                            if (eaten)
                            {
                                Console.WriteLine("\nJust as you are about to strike at the Orc, you start feeling super sick and dizzy.\n" +
                                    "It was definitely the food.\n" +
                                    "You should have listened to me.\n" +
                                    "You collapse onto the floor and just before you pass out, you see the Orc approach you.\n");
                                Console.WriteLine("This is your end.\n");
                                eatingEnding = true;
                                gameOver = true;
                                break;
                            }
                            Console.WriteLine("As the Orc readies its weapon you attempt to strike it!\n");
                            pState = PlayerState.Attack;
                            Orc.TakeTurn(pState);
                        }
                        else if (choice3 == 'p')
                        {
                            if (eaten)
                            {
                                Console.WriteLine("\nJust as you are about to parry the Orc's attack, you start feeling super sick and dizzy.\n" +
                                    "It was definitely the food.\n" +
                                    "You should have listened to me.\n" +
                                    "You collapse onto the floor and just before you pass out, you see the Orc approach you.\n");
                                Console.WriteLine("This is your end.\n");
                                gameOver = true;
                                break;
                            }
                            Console.WriteLine("You attempt to parry the Orc's attack, while at the same time aiming for his weak spots\n");
                            pState = PlayerState.Parry;
                            Orc.TakeTurn(pState);
                        }
                        else if (choice3 == 'd')
                        {
                            Console.WriteLine("You attempt to get out of the way of the Orc's incoming attack!\n");
                            pState = PlayerState.Dodge;
                            Orc.TakeTurn(pState);
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice, please try again!\n");
                            ChooseOptionCombat();
                        }
                    }
                    else
                    {
                        if (choiceThir == "give food" && inventory.Exists(x => x.Name == "food") || choiceThir == "throw food" && inventory.Exists(x => x.Name == "food"))
                        {
                            Console.WriteLine("\nYou throw the rotten food at the Orc\n" +
                                "The Orc quickly grabs the food and starts to scoff it down\n" +
                                "The Orc seems very pleased and reaches into its pockets.\n" +
                                "After searching in its pockets for a bit, it throws something to you.\n" +
                                "You quickly catch the item and look at it.\n" +
                                "A key?\n" +
                                "You look up to ask the Orc more but the Orc is too busy eating its delicious meal.\n" +
                                "Oh wel...");

                            cellar.SetDescription("Its pretty dark in here, you can't see a whole lot other than some barrels and boxes\n...\n" +
                                 "And of course a happy Orc munching away\n\nHow adorable...");
                            orc.SetDescription("The Orc is happily munching away at the food you gave it.\nBut oh boy it sure is ugly...");
                            inventory.Remove(food);
                            monsterEaten = true;
                            inCombat = false;
                            break;
                        }
                        Console.WriteLine("Invalid input, try again.");
                        return;
                    }
                    break;
            }
        }
        #endregion

        public static void TakeDamage(int d)
        {
            playerHealth -= d;
        }

        public void Update()
        {
            string currentCommand = Console.ReadLine().ToLower();

            if (currentCommand == "quit" || currentCommand == "q")
            {
                isQuiting = true;
            }
            if (!gameOver && !isQuiting)
            {
                DoAction(currentCommand);
            }
            //Done this way so quitting manually results in this message, otherwise application quits.
            else if (isQuiting)
            {
                Console.Clear();
                Console.WriteLine("Oh you wanna quit huh?\n");
                Console.Write(@"──────────────────▄▄───▄▄▄▄▄▄▀▀▀▄──▄
────────────────▄▀──▀▀█▄▄──▄────█▄█▄▀▀▄▄▄▄
─────────────────▀█▀────▀▀▀▀█▄▄▄▄───▄▄────▀▀▀▀
─────────────▄▀▀▀▀▀──▀█▄▄▄▄▄─▀▀▀▀▀█▄███▀
──────────────▀█▄▄▄──▀▀─▄▄▄▄──────────▀▀▀▀█▀▀▀
───────▄▀▀▀▄▄▀▀████▀█▄▄▄▄▄▄▄▄▄▄▄───▄▄▄▄──▄█░▄█
────────▀▄▄▄▀▀██▀▀▀▄█─███▄──██─────▀██▀▀─█░░██
────────────▀█─▀▀█▄█▄─▀▀▀───█────────────▀█░▀█
─────────▄▄▀▀─▀▀▀▀░░▀█────▄█▄▀────────────█░░░
───▄▀▀▀▀▀░░░░░░░░░░░░░▀██▀▀▄▄▀▀──────────██░░░
▄▀▀▄████░░███████░░▄▄▄▄░░▀█▄─▀▀──────────▀█▄▄░
█░░█████▄▄███████▄██████▄▄░▀█──███▄▄────────█▄
█░░░▀▀▀▀▀▀▀▀▀▀▀░░░░░░░░░▀▀▀░░█─▀███▀───────▄█▀
─▀▀▄▄▄▄▄░░░░░░░░░░░░░░░░░░░░▄▀─────────────▀█░
───▄▀▄▄▀░░░░░░░░░░░░░░░░░░░░█────────────────█
▀▀▀─▀▄▀█░░░░░░░░░░░░░░░░░░░░█───────────────▄▀
─▄▄▀▀──▀▄░░░░░░░░░░░░░░░░░░█────────────────█░
▀────────▀▄░░░░░░░░░░░░░░▄▀──────────▄█▄▄────█
───────────▀▄▄▄▄░░░░░▄▄▄▀────────────▀██▀────█
────────────█░░░▀▀▀▀██████████▀▀▀▀▀▀▄▄▄▄▄▄▄▄▄█
───────────▄▀░░░░░░░█▒▒▒▒▒▒▒▒█░░░░░░░░░▄▄░░░░█
───────────▀▄▄▄░░░░░█▒▒▒▒▒▒▒▒█░░░░░░░░░▀█▀░░░█");

                Console.WriteLine("\n\nFine, be like that T_T");

                gameOver = true;
            }
        }
    }
}