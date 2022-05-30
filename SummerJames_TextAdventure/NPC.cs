using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure_SJ_SDG20
{
    //Totally didn't just yeet the enums exercise and add to it a bit
    public enum NPCState { Idle, Patrolling, Attacking, Dead };
    public enum PlayerState { Idle, Attack, Parry, Dodge };

    class NPC
    {
        public NPCState State { get; set; }
        public int HP { get; set; }

        public NPC()
        {
            State = NPCState.Idle;
            HP = 30;
        }
        public NPCState GetState()
        {
            return State;
        }

        public void StartPatrolling()
        {
            if (State != NPCState.Dead)
                State = NPCState.Patrolling;
        }
        public void StartAttacking()
        {
            if (State != NPCState.Dead)
                State = NPCState.Attacking;
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                State = NPCState.Dead;
            }
        }
        public void TakeTurn(PlayerState pState)
        {
            switch (pState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Attack:
                    Console.WriteLine($"You manage to hit the Orc for {Game.playerDamage} damage!\n");
                    this.TakeDamage(Game.playerDamage);
                    break;
                case PlayerState.Parry:
                    Console.WriteLine($"You succesfully parry its attack and hit him back with a critical hit for {Game.playerDamage * 2} damage!\n");
                    this.TakeDamage(Game.playerDamage * 2);
                    break;
                case PlayerState.Dodge:
                    Console.WriteLine("You manage to avoid getting hit!\n");
                    break;
                default:
                    break;
            }

            Random random = new Random();
            int res = random.Next(1, 4);

            switch (State)
            {
                case NPCState.Idle:
                    if (res == 1)
                    {
                        Console.WriteLine("The Orc doesn't seem to have noticed you.\n");
                    }
                    else if (res == 2)
                    {
                        Console.WriteLine("Upon hearing the shuffling of stones, the monster's head snaps in your direction." +
                            "\nYou better prepare for a brutal fight!\n");
                        State = NPCState.Attacking;
                    }
                    else if (res == 3)
                    {
                        Console.WriteLine("The Orc's head snaps up as soon as it hears all the shuffling.\n" +
                            "Luckily it doesn't seem to have spotted you.\n" +
                            "The Orc is weary and starts to patrol the area.\n");
                        State = NPCState.Patrolling;
                    }
                    break;
                case NPCState.Patrolling:
                    if (res == 1)
                    {
                        Console.WriteLine("The Orc keeps patrolling wearily but still doesn't seem to have noticed you.\n");
                    }
                    else if (res == 2)
                    {
                        Console.WriteLine("From the corner of its eye, the Orc sees you and lets out a mighty roar!" +
                            "\nYou're in trouble now!\n");
                        State = NPCState.Attacking;
                    }
                    else if (res == 3)
                    {
                        Console.WriteLine("The Orc seems to let its guard down and sits back down to rest.\n");
                        State = NPCState.Idle;
                    }
                    break;
                case NPCState.Dead:
                    res = random.Next(1, 3);
                    Game.monsterDead = true;
                    if (res == 1)
                    {
                        Console.WriteLine("The Orc runs out of strength and falls to its knees.\n\nWow! You actually did it!\n");
                    }
                    else if (res == 2)
                    {
                        Console.WriteLine("The Orc seems to be...scared?\nIt curls up into a ball, a trembling mess.\n\n" +
                            "So uhh, congrats I guess.\nYou managed to defeat the Orc!\n\nOr something like that anyway...\n");
                    }
                    break;
                case NPCState.Attacking:
                    res = random.Next(1, 5);
                    if (res == 1 && pState == PlayerState.Attack)
                    {
                        Console.WriteLine("The Orc swings at you!\nYou close your eyes knowing this is the end.\n" +
                            "Left regretting every life choice that brought you to this moment until...\n" +
                            "You hear the Orc's weapon crash into the floor!\nSeriously...it missed a still target? What a noob.\n");
                    }
                    else if (res == 2 && pState == PlayerState.Attack)
                    {
                        Console.WriteLine("The Orc swings at you but you dodge just in time!\nThat was kinda close chief.\n");
                    }
                    else if (res == 3 && pState == PlayerState.Attack)
                    {
                        Console.WriteLine("The Orc swings at you and his weapon clashes into your body, sending you flying!\n" +
                            "Ouch, that's gotta hurt...\n");
                        Game.TakeDamage(5);
                    }
                    else if (res == 4 && pState == PlayerState.Attack)
                    {
                        Console.WriteLine("The Orc swings at you but misses you by an inch!\n" +
                            "Phew that was close...\n" +
                            "You drop your guard and the monster takes this opportunity to strike you for a critical hit!\n");
                        Game.TakeDamage(10);
                    }
                    else if (res == 1 && pState == PlayerState.Parry)
                    {
                        Console.WriteLine("The counter attack seems to temporarily stun the Orc.\n" +
                            "It falls back before quickly getting back on its feet.\nThe Orc seems furious, you better be careful...\n");
                    }
                    else if (res == 2 && pState == PlayerState.Parry)
                    {
                        Console.WriteLine("This poor thing stands no chance...\n");
                    }
                    else if (res == 3 && pState == PlayerState.Parry)
                    {
                        Console.WriteLine("You hit your counter attack but the monster was one step ahead, reading your move.\n" +
                            "You're caught off guard and the monster strikes you with his weapon!\n");
                        Game.TakeDamage(7);
                    }
                    else if (res == 4 && pState == PlayerState.Parry)
                    {
                        Console.WriteLine("You succesfully counter attack, but dropping your guard lets the monster headbutt you!\n" +
                            "Ouch!\n");
                        Game.TakeDamage(3);
                    }
                    else if (res == 1 && pState == PlayerState.Dodge)
                    {
                        Console.WriteLine("You quickly dodge behind one of the boxes.\nThe Orc seems to lose sight of you.\n" +
                            "Don't let your guard down however, the monster is weary.\n");
                        State = NPCState.Patrolling;
                    }
                    else if (res == 2 && pState == PlayerState.Dodge)
                    {
                        Console.WriteLine("You quickly dodge behind one of the boxes, hurting yourself in the process.\nThe Orc seems to lose sight of you.\n" +
                            "Don't let your guard down however, the monster is weary.\n");
                        Game.TakeDamage(1);
                        State = NPCState.Patrolling;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
