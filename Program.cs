using System;
using System.Collections.Generic;

namespace TheAmuletsOfCamembert
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();

            while (Game.Player.Alive())
            {
                Monster monster = Game.Setup.RandomMonster(Game.Player.Level);
                List<Item> monsterLoot = Game.Setup.RandomItems(Game.Player.Level);
                Game.Setup.EnchantItems(monsterLoot);
                monster.StoreLoot(monsterLoot);
                Game.Fight(monster);
            }
        }
    }
}
