using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    class Setup
    {
        private List<Effect> _effects;
        private List<Item> _items;
        private List<Monster> _monsters;
        private Random random;

        public Setup()
        {
            #region effectsDeclaration
            _effects = new List<Effect>();

            Effect restoreHPweak = new Effect(EffectType.Health, 3);
            Effect restoreHPmoderate = new Effect(EffectType.Health, 6);
            Effect restoreHPpotent = new Effect(EffectType.Health, 10);
            Effect buffDefenseWeak = new Effect(EffectType.Defense, 3);
            Effect buffDefenseModerate = new Effect(EffectType.Defense, 6);
            Effect buffDefensePotent = new Effect(EffectType.Defense, 10);
            Effect buffStrengthWeak = new Effect(EffectType.Strength, 3);
            Effect buffStrengthModerate = new Effect(EffectType.Strength, 6);
            Effect buffStrengthPotent = new Effect(EffectType.Strength, 10);
            Effect buffXPweak = new Effect(EffectType.XP, 1.1);
            Effect buffXPmoderate = new Effect(EffectType.XP, 1.2);
            Effect buffXPpotent = new Effect(EffectType.XP, 1.3);
            Effect buffGoldWeak = new Effect(EffectType.Gold, 1.1);
            Effect buffGoldModerate = new Effect(EffectType.Gold, 1.2);
            Effect buffGoldPotent = new Effect(EffectType.Gold, 1.3);

            _effects.Add(restoreHPweak);
            _effects.Add(restoreHPmoderate);
            _effects.Add(restoreHPpotent);
            _effects.Add(buffDefenseWeak);
            _effects.Add(buffDefenseModerate);
            _effects.Add(buffDefensePotent);
            _effects.Add(buffStrengthWeak);
            _effects.Add(buffStrengthModerate);
            _effects.Add(buffStrengthPotent);
            _effects.Add(buffXPweak);
            _effects.Add(buffXPmoderate);
            _effects.Add(buffXPpotent);
            _effects.Add(buffGoldWeak);
            _effects.Add(buffGoldModerate);
            _effects.Add(buffGoldPotent);

            #endregion
            #region itemsDeclaration
            _items = new List<Item>();

            Item brokenPen = new Item(ItemType.Weapon, "Broken pen", 2, 5);
            Item USB = new Item(ItemType.Weapon, "USB stick", 3, 10);
            Item chair = new Item(ItemType.Weapon, "Chair", 7, 25);
            Item skates = new Item(ItemType.Weapon, "Mr Czekalski's ice-skates", 8, 40);
            Item wire = new Item(ItemType.Weapon, "Piece of wire", 1, 1);
            Item capacitor = new Item(ItemType.Weapon, "Charged capacitor", 9, 50);
            Item katanaSword = new Item(ItemType.Weapon, "Katana sword", 12, 80);
            Item sheet = new Item(ItemType.Armor, "Sheet of paper", 1, 5);
            Item tolcNotes = new Item(ItemType.Armor, "ToLC Notes", 3, 10);
            Item winterHat = new Item(ItemType.Armor, "Winter hat", 7, 15);
            Item NOT = new Item(ItemType.Armor, "Logic gate NOT", 10, 20);
            Item leggins = new Item(ItemType.Armor, "Pair of leggins", 8, 16);
            Item calculus = new Item(ItemType.Armor, "Signature of calculus lecturer", 14, 50);
            Item furCoat = new Item(ItemType.Armor, "Fur coat", 12, 30);
            Item coffeeSmall = new Item(ItemType.Consumable, "Small coffee cup", 3, 6);
            Item coffeeLarge = new Item(ItemType.Consumable, "Large coffee cup", 9, 10);
            Item ciabatta = new Item(ItemType.Consumable, "Ciabatta sandwich", 9, 12);
            Item cig = new Item(ItemType.Consumable, "Cigarette", 6, 15);
            Item twoCigs = new Item(ItemType.Consumable, "Two cigarettes", 6, 25);
            Item cheatSheet = new Item(ItemType.Consumable, "Cheat sheet", 10, 10);
            Item donutSmall = new Item(ItemType.Consumable, "Small donut", 6, 14);
            Item donutLarge = new Item(ItemType.Consumable, "Large donut", 10, 20);
            Item water = new Item(ItemType.Consumable, "Cup of water", 0, 0);
            Item blackHole = new Item(ItemType.Consumable, "Really tiny black hole", 15, 0);
            Item rustyCoin = new Item(ItemType.Consumable, "Rusty coin", 20, 100);
            Item chewingGum = new Item(ItemType.Consumable, "Used chewing gum", 9, 5);
            Item ninjaScroll = new Item(ItemType.Consumable, "Ninja scroll", 10, 50);
            Item zenDVDtutorial = new Item(ItemType.Consumable, "'Zen buddhism' DVD tutorial", 10, 40);

            coffeeSmall.AddEffect(restoreHPweak);
            coffeeLarge.AddEffect(restoreHPmoderate);
            coffeeLarge.AddEffect(buffXPweak);
            ciabatta.AddEffect(restoreHPmoderate);
            ciabatta.AddEffect(buffDefenseWeak);
            cig.AddEffect(restoreHPweak);
            cig.AddEffect(buffStrengthWeak);
            twoCigs.AddEffect(restoreHPmoderate);
            cheatSheet.AddEffect(buffXPpotent);
            donutSmall.AddEffect(restoreHPmoderate);
            donutLarge.AddEffect(restoreHPpotent);
            blackHole.AddEffect(restoreHPweak);
            blackHole.AddEffect(buffDefenseWeak);
            blackHole.AddEffect(buffStrengthWeak);
            blackHole.AddEffect(buffGoldModerate);
            rustyCoin.AddEffect(buffGoldPotent);
            rustyCoin.AddEffect(buffXPpotent);
            chewingGum.AddEffect(buffStrengthModerate);
            chewingGum.AddEffect(buffDefenseWeak);
            ninjaScroll.AddEffect(buffStrengthPotent);
            zenDVDtutorial.AddEffect(buffDefensePotent);

            _items.Add(twoCigs);
            _items.Add(skates);
            _items.Add(wire);
            _items.Add(cheatSheet);
            _items.Add(donutSmall);
            _items.Add(donutLarge);
            _items.Add(blackHole);
            _items.Add(rustyCoin);
            _items.Add(chewingGum);
            _items.Add(coffeeLarge);
            _items.Add(brokenPen);
            _items.Add(USB);
            _items.Add(chair);
            _items.Add(katanaSword);
            _items.Add(sheet);
            _items.Add(tolcNotes);
            _items.Add(winterHat);
            _items.Add(calculus);
            _items.Add(leggins);
            _items.Add(coffeeSmall);
            _items.Add(ciabatta);
            _items.Add(cig);
            _items.Add(capacitor);
            _items.Add(NOT);
            _items.Add(water);
            _items.Add(ninjaScroll);
            _items.Add(zenDVDtutorial);

            #endregion
            #region monsterDeclaration
            _monsters = new List<Monster>();

            Monster parkingLot = new Monster("Full Parking Lot", 1);
            Monster elevator = new Monster("Broken Elevator", 1);
            Monster cigarette = new Monster("Cigarette", 1);
            Monster socs = new Monster("Sociology Class Test", 1);
            Monster coffeeCup = new Monster("Another Black Coffee Cup", 2);
            Monster ieecQuiz7 = new Monster("IEEC Quiz no 7", 2);
            Monster flatTire = new Monster("Flat Tire", 2);
            Monster lectureQuiz = new Monster("Unexpected Calculus Lecture Quiz", 2);
            Monster deansOffice = new Monster("Dean's Office", 3);
            Monster algebra = new Monster("Algebra 4th Correction Term", 3);
            Monster focpProj = new Monster("Computer Programming Project", 3);
            Monster room622a = new Monster("Room 622a", 3);
            Monster packOfCigarettes = new Monster("Another Pack of Cigarettes", 4);
            Monster calculusExam = new Monster("Calculus I Exam", 4);
            Monster ieecQuiz14 = new Monster("IEEC Quiz no 14", 4);
            Monster secondSem = new Monster("Second Semester", 4);
            Monster circuitTheory = new Monster("Circuit Theory Assessment Test", 5);
            Monster room816 = new Monster("Room 816", 5);
            Monster counter = new Monster("Mod-3 Asynchronous T-Type Flip-Flop Counter", 5);
            Monster derivative = new Monster("Missing Square Root in E8", 5);

            _monsters.Add(parkingLot);
            _monsters.Add(elevator);
            _monsters.Add(cigarette);
            _monsters.Add(socs);
            _monsters.Add(coffeeCup);
            _monsters.Add(ieecQuiz7);
            _monsters.Add(flatTire);
            _monsters.Add(lectureQuiz);
            _monsters.Add(deansOffice);
            _monsters.Add(algebra);
            _monsters.Add(focpProj);
            _monsters.Add(room622a);
            _monsters.Add(packOfCigarettes);
            _monsters.Add(calculusExam);
            _monsters.Add(ieecQuiz14);
            _monsters.Add(secondSem);
            _monsters.Add(circuitTheory);
            _monsters.Add(room816);
            _monsters.Add(counter);
            _monsters.Add(derivative);

            #endregion
            random = new Random();
        }

        public Monster RandomMonster(int playerLevel)
        {
            Monster monster = new Monster();

            // Monster level is maximally 1 level higher and minimally 1 level lower then player; first level only fights with first level.
            if (playerLevel < 6)
            {
                if (playerLevel == 1)
                    monster = _monsters[random.Next() % 4];
                else if (playerLevel < 5)
                    monster = _monsters[random.Next() % 12 + 4 * (playerLevel - 2)];
                else if (playerLevel == 5)
                    monster = _monsters[random.Next() % 8 + 12];

                monster = new Monster(monster.Name, monster.Level);
            }
            else
            {
                monster = _monsters[random.Next() % _monsters.Count];
                monster = new Monster(monster.Name, playerLevel);
            }

            return monster;
        }

        public List<Item> RandomItems(int playerLevel)
        {
            Item item = new Item();

            List<Item> itemList = new List<Item>();
            for (int i = 0; i <= playerLevel; i++)
            {
                if (random.Next() % 2 == 0)
                    break;
                int pick = random.Next() % _items.Count;
                item = _items[pick];
                var item2 = new Item(item.Type, item.Name, item.Power, item.Value, item.Effects);
                Console.WriteLine(ReferenceEquals(item, item2));
                Console.WriteLine(ReferenceEquals(item.Effects, item2.Effects));
                Console.ReadKey();
                item.AddEffect(new Effect(EffectType.Strength, 8));
                itemList.Add(item2);
            }

            return itemList;
        }

        public void EnchantItems(IEnumerable<Item> itemList)
        {
            foreach (Item item in itemList)
            {
                if (item.Type == ItemType.Consumable)
                    continue;
                if ((random.Next() % 3) != 0)
                {
                    item.AddEffect(_effects[random.Next() % _effects.Count]);

                    if ((random.Next() % 6) == 0)
                        item.AddEffect(_effects[random.Next() % _effects.Count]);
                    if ((random.Next() % 12) == 0)
                        item.AddEffect(_effects[random.Next() % _effects.Count]);
                }
            }
        }
    }
}
