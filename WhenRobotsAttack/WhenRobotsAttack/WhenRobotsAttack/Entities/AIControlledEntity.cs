using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhenRobotsAttack.Entities.AI;

namespace WhenRobotsAttack.Entities
{
    class AIControlledEntity : KillableCreature
    {

        protected AIBlackBoard bb;
        protected AIMemory mem;
        protected AISensorModule senses;

        public AIControlledEntity(GameManager game)
            : base(game)
        {
            //Reference to this entity's blackboard.
            

        }
    }
}
