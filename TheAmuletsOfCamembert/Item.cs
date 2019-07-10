using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    enum ItemType
    {
        Consumable,
        Weapon,
        Armor,
        Quest
    }

    class Item
    {
        public ItemType Type;
        public string Name { get; }
        public int Power { get; }
        public int Value { get; }
        public List<Effect> Effects { get; }
        public bool IsActive { get; private set; }

        public Item()
        {

        }

        public Item(ItemType type, string name, int power, int value)
        {
            Type = type;
            Name = name;
            Value = value;
            Power = power;
            Effects = new List<Effect>();
        }

        public Item(ItemType type, string name, int power, int value, List<Effect> effects)
        {
            Type = type;
            Name = name;
            Power = power;
            Value = value;
            Effects = new List<Effect>(effects);
        }

        public void AddEffect(Effect effect)
        {
            if (Effects.Count < 3 && Effects.Contains(effect) == false)
            {
                bool hasEffectType = false;
                foreach (var itemEffect in Effects)
                {
                    if (effect.Type == itemEffect.Type)
                    {
                        hasEffectType = true;
                        break;
                    }
                }
                if (!hasEffectType)
                    Effects.Add(effect);
            }
        }

        public bool HasEffects()
        {
            return Effects.Count > 0 ? true : false;
        }

        public void Equip()
        {
            IsActive = true;
            foreach (Effect effect in Effects)
                effect.Reset();
        }

        public void Unequip()
        {
            IsActive = false;
        }

        public void ShowItem()
        {
            Console.Write(Name + " ");
            switch (Type)
            {
                case ItemType.Consumable:
                    Console.Write("[Consumable]");
                    break;
                case ItemType.Weapon:
                    Console.Write("[Weapon]");
                    break;
                case ItemType.Armor:
                    Console.Write("[Armor]");
                    break;
                case ItemType.Quest:
                    Console.Write("[Quest item]");
                    break;
            }
            Console.Write(" (Power: " + Power + ") ");
            ShowEffects();
            Console.WriteLine();
        }

        public void ShowEffects()
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].ShowEffect();
                if (i != Effects.Count - 1)
                    Console.Write(", ");
            }
        }
    }
}
