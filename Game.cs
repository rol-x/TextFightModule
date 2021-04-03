using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TheAmuletsOfCamembert
{
    class Game
    {
        public Player Player;
        public TextColor TextColor;
        public EpicWriter Writer;
        public Setup Setup;

        internal void Pause()
        {
            TextColor.WriteColor("Press any key to continue...\n", "sub");
            Console.ReadKey(true);
        }

        public Game()
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                             .IsOSPlatform(OSPlatform.Windows);
            if (isWindows == true)
            {
              Console.WindowWidth = 94;
              Console.BufferWidth = 94;
              Console.WindowHeight = 25;
              Console.BufferHeight = 25;
            }
            Player = new Player("Macrofaculty Student");
            TextColor = new TextColor();
            Writer = new EpicWriter();
            Setup = new Setup();
            Player.UpdateStats(true);
            Console.Clear();
        }

        public void PlayerDead()
        {
            bool loop = false;
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t    Y  O  U     D  I  E  D ");
            do
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case (char)27:
                        Environment.Exit(0);
                        break;
                    default:
                        loop = true;
                        break;
                }
            } while (loop);
        }

        public void ShowHelp()
        {
            Console.Clear();
            TextColor.SetColor("neutral");
            TextColor.WriteColor("The Amulets of Camembert - Fight Module\n\nItems\n", "info");
            Console.Write("Each item belongs to one of three categories: Armor, Weapon or Consumable. Weapon items boost your strength, armor corresponds to defense ");
            Console.Write("while consumables are one-time-use items to grant\nyou some recovery effects (healing) or permanent boosts. Items can be found in the remains of ");
            Console.Write("defeated monsters and can be stored either in Backpack (10 slots) or Equipment (6 slots).\nWhile one can posses many copies of the same item in Backpack, ");
            Console.Write("only one can be Equipped, whichmeans its effects will be granted to the Player.\n\n");
            TextColor.WriteColor("The Encounter\n", "info");
            Console.Write("The Player can meet different foes on their way, each with stats and belongings random.Nextomized\naccordingly to its level. Out of three options, the Player can Attack, ");
            Console.Write("exchanging hits with\ntheir enemy (the Player goes first); manipulate their Inventory (at the cost of a lost turn)\nand try to Run away, with probability tending to ");
            Console.Write("50% as the monster is more damaged.\n\n");
            TextColor.WriteColor("Effects", "info");
            Console.Write("\nSome items are enchanted with spells empowering their effects on the Player. Those Effects canbe corelated to: Health, Strength, Defense, Gold and XP. Work with ");
            Console.Write("the best possible\ncombination of items in your Equipment to make it as far as possible in this satire-ridden\njourney through academic life.");
            Console.WriteLine("\n");
            TextColor.SetColor("sub");
            Pause();  //
        }

        public void ShowPlayerStats()
        {
            TextColor.SetColor("info");
            Console.Write(Player.Name + "\t\tHealth: " + Player.Health + "/" + Player.MaxHealth + " \tStrength: " + Player.Strength + "\tDefense: " + Player.Defense + " (" + (int)(2000.0 * Math.Sqrt(Player.Defense) / 3.0 - 500.0) / 100.0 + "% reduction)");
            Console.SetCursorPosition(0, 1);
            TextColor.SetColor("sub");
            Console.WriteLine("Level: " + Player.Level + "\tXP: " + Player.GetCurrentLevelXP() + "/" + (Player.Level + 1) * (Player.Level + 2) / 2 + " (" + (Player.Level * (Player.Level + 1) * (Player.Level + 2) / 6 + Player.GetCurrentLevelXP() - 1) + ")\tGold: " + Player.Gold);
            Console.WriteLine("---------------------------------------------------------------------------------------------\n");
        }

        public void ShowMonsterStats(Monster monster)
        {
            TextColor.SetColor("neutral");
            TextColor.WriteColor(String.Format("{0}\n", monster.Name), "danger");
            Console.WriteLine("Health: " + monster.Health + "\tStrength: " + monster.Strength + "\tDefense:" + monster.Defense + "\n");
            TextColor.SetColor("fight");
        }

        public void ShowInventoryMenu()
        {
            int selectedPosition = 0;
            bool showMenu = true;
            ConsoleKeyInfo keyCode = new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false);
            while (showMenu)
            {
                Console.Clear();
                ShowPlayerStats();

                switch (keyCode.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedPosition > 0)
                            selectedPosition--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedPosition < Player.Backpack.Count + Player.Equipment.Count - 1)
                            selectedPosition++;
                        break;
                    case ConsoleKey.E:
                        if (selectedPosition >= 0 && selectedPosition < Player.Backpack.Count)
                        {
                            Player.UseItem(Player.Backpack[selectedPosition]);
                            if (Player.Backpack[selectedPosition].Type == ItemType.Consumable)
                                Player.DropFromBackpack(selectedPosition);
                        }
                        else if (selectedPosition >= Player.Backpack.Count && selectedPosition < (Player.Backpack.Count + Player.Equipment.Count))
                        {
                            Player.UseItem(Player.Equipment[selectedPosition - Player.Backpack.Count]);
                            if (Player.Equipment[selectedPosition - Player.Backpack.Count].Type == ItemType.Consumable)
                                Player.DropFromEquipment(selectedPosition - Player.Backpack.Count);
                        }
                        break;
                    case ConsoleKey.R:
                        if (selectedPosition >= 0 && selectedPosition < Player.Backpack.Count)
                        {
                            if (!(Player.IsInEquipment(Player.Backpack[selectedPosition].Name)))
                                Player.AddToEquipment(selectedPosition);
                        }
                        else if (selectedPosition >= Player.Backpack.Count && selectedPosition < (Player.Backpack.Count + Player.Equipment.Count))
                            Player.RemoveFromEquipment(selectedPosition - Player.Backpack.Count);
                        break;
                    case ConsoleKey.D:
                        if (selectedPosition >= 0 && selectedPosition < Player.Backpack.Count)
                            Player.DropFromBackpack(selectedPosition);
                        else if (selectedPosition >= Player.Backpack.Count && selectedPosition < (Player.Backpack.Count + Player.Equipment.Count))
                            Player.DropFromEquipment(selectedPosition - Player.Backpack.Count);
                        break;
                    case ConsoleKey.Escape:
                        showMenu = false;
                        break;
                    default:
                        break;
                }

                if (selectedPosition >= 0 && selectedPosition < Player.Backpack.Count)
                {
                    if (Player.IsInEquipment(Player.Backpack[selectedPosition].Name))
                    {
                        Console.CursorTop--;
                        Console.WriteLine("Item already in Equipment!");
                    }
                }

                if (showMenu)
                {
                    Player.UpdateStats(false);
                    if (selectedPosition < Player.Backpack.Count)
                    {
                        Player.SelectFromBackpack(selectedPosition);
                        Player.ShowEquipment();
                    }
                    else if (selectedPosition >= Player.Backpack.Count)
                    {
                        Player.ShowBackpack();
                        Player.SelectFromEquipment(selectedPosition - Player.Backpack.Count);
                    }
                    TextColor.SetColor("neutral");
                    Console.WriteLine("\n[E] Use    [R] Equip / Unequip    [D] Drop    [ESC] Go back");
                }

                keyCode = Console.ReadKey(true);
            }
        }

        public void Attack(Player Player, Monster monster, bool isPlayerAttacking)
        {
            Random random = new Random();

            if (isPlayerAttacking)
            {
                double armorRatio = (1.05 - Math.Sqrt((monster.Defense)) / 15.0);           //calculating what % of damage will be taken due to armor
                int damage = (int)(Player.Strength * Math.Max(armorRatio, 0.3));                          //maximal value of 70% of damage can be blocked by armor
                damage = Math.Max(1, random.Next() % (2 * damage / 5 + 1) + 7 * damage / 10);           //each hit is about 70%-110% of potential hit, and never less than 1
                Writer.Write(Player.Name + " attacked " + monster.Name + " for " + damage + " dmg!\n");
                monster.Health -= damage;
            }
            else if (monster.Health > 0)
            {
                double armorRatio = (1.05 - Math.Sqrt((float)(Player.Defense)) / 15.0);
                int damage = (int)(monster.Strength * Math.Max(armorRatio, 0.3));
                damage = Math.Max(1, (random.Next() % (2 * damage / 5 + 1)) + 3 * damage / 5);          //monster's hit ranges from 60% to 100%
                Writer.Write(monster.Name + " attacked " + Player.Name + " for " + damage + " dmg!\n\n");
                Player.Health -= damage;
            }
        }   //move attack to Player class? (Attack(ICreature creature)

        public void LootMonster(Monster monster, Player Player)
        {
            int selection = 0;
            bool showMenu = true;
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false);
            while (showMenu)
            {
                Console.Clear();
                ShowPlayerStats();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selection > 0)
                            selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selection < Player.Backpack.Count + monster.Loot.Count - 1)
                            selection++;
                        break;
                    case ConsoleKey.E:
                        if (selection >= 0 && selection < monster.Loot.Count)
                        {
                            Player.UseItem(monster.Loot[selection]);
                            if (monster.Loot[selection].Type == ItemType.Consumable)
                            {
                                monster.DropItem(selection);

                            }
                        }
                        else if (selection >= monster.Loot.Count && selection < (monster.Loot.Count + Player.Backpack.Count))
                        {
                            Player.UseItem(Player.Backpack[selection - monster.Loot.Count]);
                            if (Player.Backpack[selection - monster.Loot.Count].Type == ItemType.Consumable)
                                Player.DropFromBackpack(selection - monster.Loot.Count);
                        }
                        break;
                    case ConsoleKey.R:
                        if (selection >= 0 && selection < monster.Loot.Count)
                        {
                            if (Player.AddToBackpack(monster.Loot[selection]))
                                monster.DropItem(selection);
                        }
                        else if (selection >= monster.Loot.Count && selection < (monster.Loot.Count + Player.Backpack.Count))
                        {
                            Item storedItem = Player.Backpack[selection - monster.Loot.Count];
                            Player.DropFromBackpack(selection - monster.Loot.Count);
                            monster.StoreItem(storedItem);
                        }
                        break;
                    case ConsoleKey.D:
                        if (selection >= monster.Loot.Count && selection < (monster.Loot.Count + Player.Backpack.Count))
                            Player.DropFromBackpack(selection - monster.Loot.Count);
                        break;
                    case ConsoleKey.Escape:
                        showMenu = false;
                        break;
                    default:
                        break;
                }


                if (showMenu)
                {
                    Player.UpdateStats(false);
                    if (selection < monster.Loot.Count)
                    {
                        monster.SelectFromLoot(selection);
                        Player.ShowBackpack();
                    }
                    else if (selection >= monster.Loot.Count)
                    {
                        monster.ShowLoot();
                        Player.SelectFromBackpack(selection - monster.Loot.Count);
                    }
                    TextColor.SetColor("neutral");
                    Console.WriteLine("\n[E] Use    [R] Loot / Store    [D] Drop    [ESC] Go back");
                }

                keyInfo = Console.ReadKey(true);
            }
        }

        private void PlaceHelpButton()
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(84, 4);
            TextColor.WriteColor("[H] Help", "sub");
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        public void Fight(Monster monster)
        {
            bool fight = true;
            bool firstScreen = true;
            bool doPause = true;
            double runTry;
            Random random = new Random();

            ShowPlayerStats();
            PlaceHelpButton();
            TextColor.SetColor("fight");
            Writer.Write("You encountered " + monster.Name + "!");
            Writer.Write("\nIt looks like it has " + monster.MaxHealth + " health and " + monster.Strength + " strength!\n\n");
            do
            {
                doPause = true;
                if (!firstScreen)
                {
                    ShowPlayerStats();
                    PlaceHelpButton();
                    TextColor.SetColor("fight");
                    Console.Write("You encountered " + monster.Name + "!");
                    Console.WriteLine("\nIt looks like it has " + monster.MaxHealth + " health and " + monster.Strength + " strength!\n");
                }
                ShowMonsterStats(monster);
                TextColor.SetColor("neutral");
                Console.WriteLine("\n\t\t[A] Attack!\n\t\t[I] Inventory\n\t\t[R] Run (" + (int)(50.0 * (monster.MaxHealth - monster.Health) / (monster.MaxHealth)) + "% chance)");
                //run probability is equal to damage done to monster in % over 2
                //if monster is left with 3HP from 12HP it's 9/12 * 50% = 37.5%

                TextColor.SetColor("fight");
                var menuChoice = Console.ReadKey(true);
                switch (menuChoice.Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine();
                        Attack(Player, monster, true);
                        Attack(Player, monster, false);
                        break;
                    case ConsoleKey.I:
                        ShowInventoryMenu();
                        PlaceHelpButton();
                        TextColor.SetColor("fight");
                        Console.Write("You encountered " + monster.Name + "!");
                        Console.WriteLine("\nIt looks like it has " + monster.MaxHealth + " health and " + monster.Strength + " strength!\n");
                        ShowMonsterStats(monster);
                        TextColor.SetColor("neutral");
                        Console.Write("\n\t\t[A] Attack!\n\t\t[I] Inventory\n\t\t[R] Run (" + (int)50.0 * (monster.MaxHealth - monster.Health) / monster.MaxHealth + "% chance)\n");
                        TextColor.SetColor("fight");
                        Console.WriteLine();
                        Attack(Player, monster, false);
                        if (Player.Health <= 0)
                            fight = false;
                        break;
                    case ConsoleKey.R:
                        runTry = random.NextDouble() * 100;
                        if (runTry < 50.0 * (monster.MaxHealth - monster.Health) / monster.MaxHealth)
                        {
                            Console.ReadKey(true);
                            Console.WriteLine();
                            Writer.Write("Escape successful!\n");
                            fight = false;
                        }
                        else
                        {
                            Console.ReadKey(true); ;
                            Console.WriteLine();
                            Writer.Write("Escape unsuccessful!\n");
                            Attack(Player, monster, false);
                        }
                        break;
                    case ConsoleKey.H:
                        ShowHelp();
                        doPause = false;
                        break;
                    default:
                        Console.ReadKey(true);
                        doPause = false;
                        break;
                }

                if (Player.Health <= 0 || monster.Health <= 0)
                {
                    doPause = false;
                    fight = false;
                    if (Player.Health <= 0)
                    {
                        TextColor.SetColor("danger");
                        Writer.Write(Player.Name + "'s HP is " + Player.Health);
                        Console.WriteLine();
                        System.Threading.Thread.Sleep(2137);
                        PlayerDead();
                    }
                    else
                    {
                        Console.Clear();
                        ShowPlayerStats();
                        Console.Write("You encountered " + monster.Name + "!");
                        Console.SetCursorPosition(84, 4);
                        TextColor.WriteColor("[H] Help", "sub");
                        Console.WriteLine("\nIt looks like it has " + monster.MaxHealth + " health and " + monster.Strength + " strength!\n");
                        TextColor.SetColor("consumable");
                        System.Threading.Thread.Sleep(200);
                        Writer.Write("\nYou defeated " + monster.Name + "!\nXP gained: " + monster.XPReward + "\nGold gained: " + monster.GoldReward + "\n\n");
                        Player.GainXP(monster.XPReward);
                        Player.AddGold(monster.GoldReward);
                        TextColor.SetColor("default");
                        Pause();
                        Console.Clear();
                        LootMonster(monster, Player);
                    }
                }

                if (doPause)
                    Pause();
                Console.Clear();
                if (firstScreen)
                    firstScreen = false;
            } while (fight == true);
        }
    }
}
