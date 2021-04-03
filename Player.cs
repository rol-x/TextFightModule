using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    class Player                // redundant: default constructor
    {
        private TextColor _textColor;

        private double _bonusEffectStrength;
        private double _bonusEffectDefense;
        private double _bonusItemStrength;
        private double _bonusItemDefense;
        private double _bonusItemMaxHealth;
        private double _goldBuff;
        private double _xpBuff;

        public string Name { get; }
        public int Level { get; private set; }
        public double Strength { get; private set; }
        public double Defense { get; private set; }
        public int MaxHealth { get; private set; }
        public int Gold { get; private set; }
        public int XP { get; private set; }
        public List<Item> Equipment { get; }
        public List<Item> Backpack { get; }
        public int Health { get; set; }

        /*public Player()
        {
            _equipment = new List<Item>();
            _backpack = new List<Item>();
            _textColor = new TextColor();
        }*/

        public Player(string name)
        {
            Equipment = new List<Item>();
            Backpack = new List<Item>();
            _textColor = new TextColor();
            Name = name;
            Gold = 0;
            XP = 1;
            UpdateStats(true);
        }

        public bool Alive()
        {
            return Health > 0 ? true : false;
        }

        private void UpdateLevel()
        {
            int requiredXP = 1;
            int currentXP = XP;
            int increment = 2;
            Level = 0;

            while (currentXP >= requiredXP)
            {
                currentXP -= requiredXP;
                Level++;

                requiredXP += increment;
                increment++;
            }
        }

        private int ReturnBaseStrength()
        {
            int baseStrength = 2 + 3 * Level;
            foreach (Item item in Equipment)
            {
                if (!item.IsActive)
                    continue;
                if (item.Type == ItemType.Weapon)
                {
                    baseStrength += item.Power;
                }
            }
            baseStrength += (int)_bonusEffectStrength;
            return baseStrength;
        }

        private int ReturnBaseDefense()
        {
            int baseDefense = 2 * Level;
            foreach (Item item in Equipment)
            {
                if (!item.IsActive)
                    continue;
                if (item.Type == ItemType.Armor)
                {
                    baseDefense += item.Power;
                }
            }
            baseDefense += (int)_bonusEffectDefense;
            return baseDefense;
        }

        private void UpdateEffects()
        {
            _bonusItemStrength = 0;
            _bonusItemDefense = 0;
            _bonusItemMaxHealth = 0;

            foreach (Item item in Equipment)
            {
                if (!item.IsActive)
                    continue;
                if (item.HasEffects())
                {
                    if (item.Type == ItemType.Consumable)
                        continue;
                    foreach (Effect effect in item.Effects)
                    {
                        if (!effect.IsUsed)
                        {
                            switch (effect.Type)
                            {
                                case EffectType.Health:
                                    _bonusItemMaxHealth += effect.Power;
                                    break;
                                case EffectType.Strength:
                                    _bonusItemStrength += effect.Power;
                                    effect.Use();
                                    break;
                                case EffectType.Defense:
                                    _bonusItemDefense += effect.Power;
                                    effect.Use();
                                    break;
                                case EffectType.Gold:
                                    _goldBuff *= effect.Power;
                                    effect.Use();
                                    break;
                                case EffectType.XP:
                                    _xpBuff *= effect.Power;
                                    effect.Use();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            Strength = _bonusItemStrength + ReturnBaseStrength();
            Defense = _bonusItemDefense + ReturnBaseDefense();
            MaxHealth = 30 + Level * 5 + (int)_bonusItemMaxHealth;
        }

        public void UpdateStats(bool init)
        {
            if (init)
            {
                _bonusEffectDefense = 0;
                _bonusEffectStrength = 0;
                _goldBuff = 1.0;
                _xpBuff = 1.0;
                Health = MaxHealth;
            }

            UpdateLevel();
            UpdateEffects();
        }

        public void AddGold(int addedGold)
        {
            Gold += addedGold;
        }

        public void SubtractGold(int subtractedGold)
        {
            Gold -= subtractedGold;
        }

        public int GetCurrentLevelXP()
        {
            int requiredXP = 1;
            int currentXP = XP;
            int increment = 2;

            while (currentXP >= requiredXP)
            {
                currentXP -= requiredXP;
                requiredXP += increment;
                increment++;
            }
            return currentXP;
        }

        public void GainXP(int gainedXP)
        {
            XP += gainedXP;
        }

        public void ShowBackpack()
        {
            _textColor.SetColor("backpack");
            Console.WriteLine("[BACKPACK]");
            if (Backpack.Count == 0)
                Console.WriteLine("Backpack empty!\n");
            int index = 1;
            foreach (Item item in Backpack)
            {
                Console.Write(index + ". ");
                item.ShowItem();
                index++;
            }
            Console.WriteLine();
        }

        public bool AddToBackpack(Item item)
        {
            if (Backpack.Count < 10)
            {
                Backpack.Add(item);
                return true;
            }
            else
                return false;
        }

        public void SelectFromBackpack(int selection)
        {
            _textColor.SetColor("backpack");
            Console.WriteLine("[BACKPACK]");
            if (Backpack.Count == 0)
                Console.WriteLine("Backpack empty!\n");
            int index = 1;
            foreach (Item item in Backpack)
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

        public bool DropFromBackpack(int backpackItemIndex)
        {
            if (backpackItemIndex >= Backpack.Count)
                return false;
            Backpack.RemoveAt(backpackItemIndex);
            return true;
        }

        public void ShowEquipment()
        {
            _textColor.SetColor("equipment");
            Console.WriteLine("[EQUIPMENT]");
            if (Backpack.Count == 0)
                Console.WriteLine("Equipment empty!\n");
            int index = 1;
            foreach (Item item in Equipment)
            {
                Console.Write(index + ". ");
                item.ShowItem();
                index++;
            }
            Console.WriteLine();
        }

        public bool AddToEquipment(int backpackItemIndex)
        {
            if (Backpack.Count == 0)
                return false;
            if (Equipment.Count < 6)
            {
                Equipment.Add(Backpack[backpackItemIndex]);
                DropFromBackpack(backpackItemIndex);
                Equipment[Equipment.Count - 1].Equip();
                return true;
            }
            else
                return false;
        }

        public void SelectFromEquipment(int selection)
        {
            _textColor.SetColor("equipment");
            Console.WriteLine("[EQUIPMENT]");
            if (Equipment.Count == 0)
                Console.WriteLine("Equipment empty!\n");
            int index = 1;
            foreach (Item item in Equipment)
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

        public bool DropFromEquipment(int equipmentItemIndex)
        {
            if (equipmentItemIndex >= Equipment.Count)
                return false;
            Equipment.RemoveAt(equipmentItemIndex);
            return true;
        }

        public bool RemoveFromEquipment(int equipmentItemIndex)
        {
            if (Equipment.Count == 0)
                return false;
            if (Backpack.Count < 10)
            {
                Backpack.Add(Equipment[equipmentItemIndex]);
                Equipment.RemoveAt(equipmentItemIndex);
                Backpack[Backpack.Count - 1].Unequip();
                return true;
            }
            else
                return false;
        }

        public bool IsInInventory(string itemName)
        {
            foreach (Item item in Equipment)
            {
                if (item.Name == itemName)
                    return true;
            }
            foreach (Item item in Backpack)
            {
                if (item.Name == itemName)
                    return true;
            }
            return false;
        }

        public bool IsInEquipment(string itemName)
        {
            foreach (Item item in Equipment)
            {
                if (item.Name == itemName)
                    return true;
            }
            return false;
        }

        public void UseItem(Item item)
        {
            if (item.Type == ItemType.Consumable)
            {
                foreach (Effect effect in item.Effects)
                {
                    switch (effect.Type)
                    {
                        case EffectType.Health:
                            Health = (int) Math.Min(MaxHealth, Health + effect.Power);
                            break;
                        case EffectType.Defense:
                            _bonusEffectDefense += effect.Power;
                            break;
                        case EffectType.Strength:
                            _bonusEffectStrength += effect.Power;
                            break;
                        case EffectType.Gold:
                            _goldBuff *= effect.Power;
                            break;
                        case EffectType.XP:
                            _xpBuff *= effect.Power;
                            break;
                    }
                }
            }
        }
    }
}
