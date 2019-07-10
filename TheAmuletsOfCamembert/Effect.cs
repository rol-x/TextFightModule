using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    enum EffectType
    {
        Health,
        Strength,
        Defense,
        Gold,
        XP
    }

    class Effect
    {
        public EffectType Type { get; }
        public double Power { get; }
        public bool IsUsed { get; private set; }

        public Effect(EffectType type, double power)
        {
            Type = type;
            Power = power;
            Reset();
        }

        public void ShowEffect()
        {
            switch (Type)
            {
                case EffectType.Health:
                    Console.Write("Health");
                    break;
                case EffectType.Strength:
                    Console.Write("Strength");
                    break;
                case EffectType.Defense:
                    Console.Write("Defense");
                    break;
                case EffectType.Gold:
                    Console.Write("Gold");
                    break;
                case EffectType.XP:
                    Console.Write("XP");
                    break;
            }
            Console.Write(" buff: " + Power);
        }

        public void Use()
        {
            IsUsed = true;
        }

        public void Reset()
        {
            IsUsed = false;
        }
    }
}
