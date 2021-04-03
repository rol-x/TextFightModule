using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    class Monster       //redundant: default constructor, OverrideLevel method
    {
        private TextColor _textColor;

        public string Name { get; }
        public double Strength { get; }
        public double Defense { get; }
        public int MaxHealth { get; }
        public List<Item> Loot { get; }
        public int GoldReward { get; }
        public int XPReward { get; }
        public int Level { get; private set; }
        public int Health { get; set; }

        public Monster()
        {
            Loot = new List<Item>();
            _textColor = new TextColor();
        }

        public Monster(string name, int level)
        {
            Loot = new List<Item>();
            _textColor = new TextColor();

            Name = name;
            if (level > 0)
                Level = level;
            else
                Level = 1;                                                          //do it again:

            Random random = new Random();
            MaxHealth = random.Next() % (level * 6 + 1) + (level * 6);     //1: 6-12	2: 12-24	3: 18-36	4: 24-48	5: 30-60	6: 36-72
            Strength = 1;
            //Strength = random.Next() % (level * 3 + 1) + (level * 3);      //1: 3-6	    2: 4-8		3: 6-12		4: 8-16		5: 10-20	6: 12-24
            Defense = random.Next() % (level * 2 + 1) + (level);           //1: 1-3	    2: 2-6		3: 3-9		4: 4-12		5: 5-15		6: 6-18 
            Health = MaxHealth;

            GoldReward = (int)((MaxHealth * 0.3) + (Defense * 0.6) + Strength + Level);
            XPReward = Level;
            if (random.Next() % 10 == 0)
                XPReward *= 2;
        }

        /*public void OverrideLevel(int newLevel)
        {
            Level = newLevel;
        }*/

        public bool IsDead()
        {
            return (Health <= 0) ? true : false;
        }

        public void ShowLoot()
        {
            _textColor.SetColor("consumable");
            Console.WriteLine("[LOOT]");
            if (Loot.Count == 0)
                Console.WriteLine("Loot empty!\n");
            int index = 1;
            foreach (Item item in Loot)
            {
                Console.Write(index + ". ");
                item.ShowItem();
                index++;
            }
            Console.WriteLine();
        }

        public void DropItem(int itemID)
        {
            Loot.RemoveAt(itemID);
        }

        public void StoreItem(Item item)
        {
            Loot.Add(item);
        }

        public void SelectFromLoot(int selection)
        {
            _textColor.SetColor("consumable");
            Console.WriteLine("[LOOT]");
            if (Loot.Count == 0)
                Console.WriteLine("Loot empty!\n");
            int index = 1;
            foreach (Item item in Loot)
            {
                if (selection + 1 == index)
                    Console.Write("> ");
                Console.Write(index + ". ");
                item.ShowItem();
                index++;
            }
            Console.WriteLine();
            _textColor.SetColor("neutral");
        }

        public void StoreLoot(List<Item> monsterLoot)
        {
            Loot.AddRange(monsterLoot);
        }
    }
}
