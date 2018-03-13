using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Program
    {
        private static void PrintMovesAvailable(Pokemon playerPokemon)
        {
            for (int i = 0; i < playerPokemon.Moves.Count; i++)
            {
                Console.WriteLine(playerPokemon.Moves[i].Name + "(" + playerPokemon.Moves[i].Id + ")");

            }
        }

        private static void PrintPokemons(List<Pokemon> rosterPokemons)
        {
            for (int j = 0; j < rosterPokemons.Count; j++)
            {
                Console.WriteLine(rosterPokemons[j].Name );

            }
        }

        static void Main(string[] args)
        {
            
            // INITIALIZE YOUR THREE POKEMONS HERE

            List<Move> charmanderMoves = new List<Move>();
            charmanderMoves.Add(new Move("Ember", "1"));
            charmanderMoves.Add(new Move("Fire Blast", "2"));

            List<Move> bulbasaurMoves = new List<Move>();
            bulbasaurMoves.Add(new Move("Cut", "1"));
            bulbasaurMoves.Add(new Move("Mega Drain", "2"));
            bulbasaurMoves.Add(new Move("Razor Leaf", "3"));

            List<Move> squirtleMoves = new List<Move>();
            squirtleMoves.Add(new Move("Bubble", "1"));
            squirtleMoves.Add(new Move("Bite", "2"));

            Pokemon charmander = new Pokemon("Charmander", 3, 52, 43, 39, Elements.Fire, charmanderMoves);
            Pokemon bulbasaur = new Pokemon("Bulbasaur", 3, 49, 49, 45, Elements.Grass, bulbasaurMoves);
            Pokemon squirtle = new Pokemon("Squirtle", 2, 48, 65, 44, Elements.Water, squirtleMoves);

            List<Pokemon> roster = new List<Pokemon>();
            roster.Add(charmander);
            roster.Add(bulbasaur);
            roster.Add(squirtle);

            


            // INITIALIZE YOUR THREE POKEMONS HERE

            Console.WriteLine("Welcome to the world of Pokemon!");
            Console.WriteLine("These are the commmands you can write:");
            Console.WriteLine("list");
            Console.WriteLine("fight");
            Console.WriteLine("heal");
            Console.WriteLine("quit");
            Console.WriteLine("examine");

            while (true)
            {
                Console.WriteLine("\nPlease enter a command");
                switch (Console.ReadLine())
                {
                    case "list":
                        // PRINT THE POKEMONS IN THE ROSTER HERE
                        PrintPokemons(roster);
                        break;

                    case "commands":
                        Console.WriteLine("These are the commmands you can write:");
                        Console.WriteLine("list");
                        Console.WriteLine("fight");
                        Console.WriteLine("heal");
                        Console.WriteLine("quit");
                        Console.WriteLine("examine");

                        break;

                    case "examine":
                        Console.WriteLine("Choose a pokemon to examine");
                        switch (Console.ReadLine())
                        {
                            case "charmander":
                                Console.WriteLine("These are the stats for charmander");
                                Console.WriteLine("Level: " + roster[0].level);
                                Console.WriteLine("HP: " + roster[0].hp);
                                Console.WriteLine("Attack: " + roster[0].baseAttack);
                                Console.WriteLine("Defence: " + roster[0].baseDefence);
                                Console.WriteLine("Element: Fire");
                                break;
                            case "bulbasaur":
                                Console.WriteLine("These are the stats for bulbasaur");
                                Console.WriteLine("Level: " + roster[1].level);
                                Console.WriteLine("HP: " + roster[1].hp);
                                Console.WriteLine("Attack: " + roster[1].baseAttack);
                                Console.WriteLine("Defence: " + roster[1].baseDefence);
                                Console.WriteLine("Element: grass");
                                break;
                            case "squirtle":
                                Console.WriteLine("These are the stats for squirtle");
                                Console.WriteLine("Level: " + roster[2].level);
                                Console.WriteLine("HP: " + roster[2].hp);
                                Console.WriteLine("Attack: " + roster[2].baseAttack);
                                Console.WriteLine("Defence: " + roster[2].baseDefence);
                                Console.WriteLine("Element: water");
                                break;

                            default:
                                break;
                        }
                        break;
                       

                    case "fight":
                        //PRINT INSTRUCTIONS AND POSSIBLE POKEMONS (SEE SLIDES FOR EXAMPLE OF EXECUTION)
                        Console.Write("Choose which pokemon should fight:" + "\n");

                        //READ INPUT, REMEMBER IT SHOULD BE TWO POKEMON NAMES
                        string command = Console.ReadLine();
                        string[] input = command.Split(' ');
                        var numberOfInputs = input.Count();
                        if (numberOfInputs != 2)
                        {
                            Console.WriteLine("Invalid amount of pokemons chosen");
                            Console.WriteLine("You can only choose two pokemons");
                            break;
                        }
                        //BE SURE TO CHECK THE POKEMON NAMES THE USER WROTE ARE VALID (IN THE ROSTER) AND IF THEY ARE IN FACT 2!
                        Pokemon player = null;
                        Pokemon enemy = null;
                        Random random = new Random();
                        int randPok = random.Next(0,roster.Count + 1);
                        //function for player choosing pokemon
                        player = roster.SingleOrDefault(p => p.Name.ToLower().StartsWith(input[0].ToLower()));

                       // enemy = roster.Single(p => p.)

                        //function for enemy getting a pokemon assigned
                        enemy = roster.SingleOrDefault(p => p.Name.ToLower().StartsWith(input[1].ToLower()));
                        if (player == null || enemy == null)
                        {
                            Console.WriteLine("The player and the enemy was not assigned correctly");
                            Console.WriteLine("These are the Pokemon you can choose from:");
                            PrintPokemons(roster);

                        }
                        

                        //if everything is fine and we have 2 pokemons let's make them fight
                        if (player != null && enemy != null && player != enemy)
                        {
                            Console.WriteLine("A wild " + enemy.Name + " appears!");
                            Console.Write(player.Name + " I choose you! ");

                            //BEGIN FIGHT LOOP
                            while (player.Hp > 0 && enemy.Hp > 0)
                            {
                                //PRINT POSSIBLE MOVES
                                Console.Write("What move should we use? (" + "\n");
                                PrintMovesAvailable(player);
                                

                                //GET USER ANSWER, BE SURE TO CHECK IF IT'S A VALID MOVE, OTHERWISE ASK AGAIN
                                string chosenMoveInput = Console.ReadLine();
                                var myChosenMove = player.GetMoveByInput(chosenMoveInput);
                                if (myChosenMove == null)
                                {
                                    //Userinput was invalid, print the available moves
                                    Console.WriteLine(chosenMoveInput + " was not a valid move to choose");
                                    continue;
                                }


                                //CALCULATE AND APPLY DAMAGE

                                //calculate elemental damage

                                //// - defence
                                //double damage = player.CalculateTotalDamage(enemy.element);
                                // calculate resulting damage 
                                double resultingDamage = player.CalculateTotalLostHP(enemy);
                                

                                //print the move and damage
                                Console.WriteLine(player.Name + " uses " + myChosenMove.Name + ". " + enemy.Name + " loses " + resultingDamage + " HP " + enemy.Name + " has " + enemy.hp + " HP left");

                                //if the enemy is not dead yet, it attacks
                                if (enemy.Hp > 0)
                                {
                                    //CHOOSE A RANDOM MOVE BETWEEN THE ENEMY MOVES AND USE IT TO ATTACK THE PLAYER
                                    Random rand = new Random();
                                    int randMove = rand.Next(1, enemy.Moves.Count + 1);
                                    /*the C# random is a bit different than the Unity random
                                     * you can ask for a number between [0,X) (X not included) by writing
                                     * rand.Next(X) 
                                     * where X is a number 
                                     */
                                     //var enemyMove = -1;

                                    var enemymove = enemy.Moves.Single(m => m.Id == randMove.ToString());
                                    double enemyDamage = enemy.CalculateTotalLostHP(player);
                                    

                                    //print the move and damage
                                    Console.WriteLine(enemy.Name + " uses " + enemymove.Name + ". " + player.Name + " loses " + enemyDamage + " HP, " + player.Name + " has " + player.hp + " HP left!" );
                                }
                            }
                            //The loop is over, so either we won or lost
                            if (enemy.Hp <= 0)
                            {
                                Console.WriteLine(enemy.Name + " faints, you won!");
                            }
                            else
                            {
                                Console.WriteLine(player.Name + " faints, you lost...");
                            }
                        }
                        //otherwise let's print an error message
                        else
                        {
                            Console.WriteLine("Invalid pokemons");
                        }
                        break;

                    case "heal":
                        //RESTORE ALL POKEMONS IN THE ROSTER
                        roster.ForEach(p => p.Restore());
                        Console.WriteLine("All pokemons have been healed");
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
