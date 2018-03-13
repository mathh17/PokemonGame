using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    /// <summary>
    /// The possible elemental types
    /// </summary>
    public enum Elements
    {
        Fire,
        Water,
        Grass
    }

    public class Pokemon
    {
        //fields
        public double level;
        public double baseAttack;
        public double baseDefence;
        public double hp;
        public double maxHp;
        public Elements element;

        //properties, imagine them as private fields with a possible get/set property (accessors)
        //in this case used to allow other objects to read (get) but not write (no set) these variables
        public string Name { get; }

        //example of how to make the string Name readable AND writable  
        //  public string Name { get; set; }
        public List<Move> Moves { get; }
        //can also be used to get/set other private fields
        public double Hp { get => hp; }

        /// <summary>
        /// Constructor for a Pokemon, the arguments are fairly self-explanatory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="baseAttack"></param>
        /// <param name="baseDefence"></param>
        /// <param name="hp"></param>
        /// <param name="element"></param>
        /// <param name="moves">This needs to be a List of Move objects</param>
        public Pokemon(string name, int level, double baseAttack,
            double baseDefence, double hp, Elements element,
            List<Move> moves)
        {
            this.level = level;
            this.baseAttack = baseAttack;
            this.baseDefence = baseDefence;
            this.Name = name;
            this.hp = hp;
            this.maxHp = hp;
            this.element = element;
            this.Moves = moves;
        }

        /// <summary>
        /// performs an attack and returns total damage, check the slides for how to calculate the damage
        /// IMPORTANT: should also apply the damage to the enemy pokemon
        /// </summary>
        /// <param name="enemy">This is the enemy pokemon that we are attacking</param>
        /// <returns>The amount of damage that was applied so we can print it for the user</returns>
        public double Attack(double damage)
        {
            //this function only adds the dmg to the enemy
            /*
            if (damage < 0)
            {
                damage = 0;
            }
            */
            hp = hp - damage;
            return hp > 0 ? hp : 0;
        }

        /// <summary>
        /// calculate the current amount of defence points
        /// </summary>
        /// <returns> returns the amount of defence points considering the level as well</returns>
        public double CalculateDefence(double enemyDef, double enemyLvl)
        {

            return enemyDef * enemyLvl;
            //throw new NotImplementedException();
        }
        /// <summary>
        /// calculates the total HP the enemy loses.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public double CalculateTotalLostHP(Pokemon defender)
        {
            //damage function
            var resultingDamage = CalculateTotalDamage(defender.element) - CalculateDefence(defender.baseDefence, defender.level);

            //add damage to defender.
            defender.Attack(resultingDamage);

            //return damage to program
            return resultingDamage  > 0 ? resultingDamage : 0;

        }

        /// <summary>
        /// Calculates elemental effect, check table at https://bulbapedia.bulbagarden.net/wiki/Type#Type_chart for a reference
        /// </summary>
        /// <param name="damage">The amount of pre elemental-effect damage</param>
        /// <param name="enemyType">The elemental type of the enemy</param>
        /// <returns>The damage post elemental-effect</returns>
        public double CalculateTotalDamage( Elements enemyType)
        {
            var elementPower = 1.0;
            //if my pokemon's element is grass
            if (element == Elements.Grass)
            {
                if (enemyType == Elements.Fire)
                {
                    elementPower = 0.5;
                }
                else if (enemyType == Elements.Water)
                {
                    elementPower = 2;
                }

            }
            //if my pokemon's element is water
            else if (element == Elements.Water)
            {
                if (enemyType == Elements.Fire)
                {
                    elementPower = 2;
                }
                else if (enemyType == Elements.Grass)
                {
                    elementPower = 0.5;
                }
             
            }
            //if my pokemon's element is fire 
            else if (element == Elements.Fire)
            {
                if (enemyType == Elements.Grass)
                {
                    elementPower = 2;
                }
                else if (enemyType == Elements.Water)
                {
                    elementPower = 0.5;
                }
            }

            //if the pokemon are of the same element return damage 1 to 1
            return baseAttack * level * elementPower;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Heals the pokemon by resetting the HP to the max
        /// </summary>
        public void Restore()
        {
            hp = maxHp;
        }

        /// <summary>
        /// Rerturns null when no hit
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public Move GetMoveByInput(string userInput)
        {
            return Moves.FirstOrDefault(m => m.Name.ToLower() == userInput.ToLower() || m.Id == userInput );
        }
    }
}
