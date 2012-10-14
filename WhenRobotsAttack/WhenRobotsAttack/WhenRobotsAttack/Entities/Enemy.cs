using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WhenRobotsAttack
{
    public class Enemy : KillableCreature
    {

        public Enemy(GameManager game, string enemyname)
            : base(game)
        {
            loadEnemy(enemyname);
        }

        private void loadEnemy(string enemyname)
        {
            List<Tuple<string, string>> enemyInfo = ReadFromFile.read("Data\\enemies.txt");
            string[] enemyVars = null;

            for (int i = 0; i < enemyInfo.Count; i++)
            {
                if (enemyInfo[i].Item1 == enemyname)
                {
                    enemyVars = enemyInfo[i].Item2.Split('/');

                    // Loads the stats
                    this.attackPower = Convert.ToInt32(enemyVars[0]);
                    this.startingHP = Convert.ToInt32(enemyVars[1]);
                }
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
