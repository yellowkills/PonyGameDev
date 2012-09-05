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


namespace First_Game
{
    class KillableCreature : Entity
    {
        protected bool isDead;
        protected bool isRecovering;

        // Health
        protected int startingHP;
        protected int hp;

        // Timer Starting Values (lower = faster)
        protected int healSpeed;    // This indicates the speed of health regen.
        protected int recoverSpeed; // This is how fast the creature recovers from being damaged.

        // Timers
        protected int recoverTimer;  // A very short timer that keeps this creature from being damaged for a bit. This should
                                    // be triggered right after damage has occurred to prevent multi hit and to allow the 
                                    // creature to recover.

        protected int healTimer;    // This is the time left until the creature is healed by 1 point.

        protected int poisonTimer;  // [UNUSED] this is the time remaining for the poisoned status effect. It counts down and 
                                    // occasionally triggers damage.

        // Custom Variables
        public int HP
        {
            get { return hp; }
            set
            {
                if (value <= 0)
                {
                    hp = 0;
                    isDead = true;
                }
                else if (value >= startingHP)
                    hp = startingHP;
                else
                    hp = value;
            }
        }
        protected int HealTimer
        {
            get { return healTimer; }
            set
            {
                if (value <= 0)
                {
                    heal();
                    healTimer = healSpeed;
                }
                else
                    healTimer = value;
            }
        }
        protected int DamageTimer
        {
            get { return recoverTimer; }
            set
            {
                if (value <= 0)
                {
                    recoverTimer = 0;
                    isRecovering = false;
                }
                else
                    recoverTimer = value;
            }
        }



        // Default Constructor
        public KillableCreature(GameManager game, SpriteBatch spriteBatch, Camera camera, int startingHP)
            : base(game, spriteBatch, camera)
        {
            this.startingHP = startingHP;
            HP = startingHP;
            
            
            healSpeed = 500;    // These values are just so the compiler will shut up. I highly 
            recoverSpeed = 20;  // recommend that you overwrite these when you extend this class.
            poisonTimer = 0;    //
        }

        // This allows damage and healing to work on a timed basis
        public void tickTimers()
        {
            HealTimer -= 1;
            if(isRecovering) DamageTimer -= 1;
        }


        /*      DAMAGE     */

        // Causes one point of damage, which lowers the creatures health by one.
        public void takeDamage() 
        {
            if (DamageTimer <= 0)
            {
                HP -= 1;
                isRecovering = true;
                DamageTimer = recoverSpeed;
            } 
        }

        // Causes a specified point of damage, which lowers the creatures health by that amount.
        public void takeDamage(int amountOfDamage)
        {
            if (recoverTimer <= 0)
            {
                HP -= amountOfDamage;
                isRecovering = true;
                DamageTimer = recoverSpeed;
            } 
        }

        // Reduces the creatures health to zero
        public void kill()
        {
            HP = 0;
        }




        /*      HEALING     */

        // Increases the creatures health by one
        protected void heal() 
        {
            HP += 1;
        }

        // Increases the creatures health by the specified amount
        protected void heal(int amountOfHeal)
        {
            HP += amountOfHeal;
        }

        // Restores the creatures health to its starting HP, which is also its maximum HP
        protected void healFull()
        {
            HP = startingHP;
        }
    }
}
