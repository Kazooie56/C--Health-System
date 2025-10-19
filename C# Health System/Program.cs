using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace C__Health_System
{
    //MUST USE ALL THE SPECIFIC NAMES FOR THE METHODS, INTS, EVERYTHING AS IT DOES ON THE ASSIGNMENT OUTLINE!
    // if you have a take damage and revive method, don't put logic in your take damage that revives you cause it breaks something maybe?
    internal class Program
    {
        static int health = 100;
        static string healthStatus = " (Perfect Health)";
        static int shield = 100;
        static int lives = 3;
        static void Main(string[] args)
        {
            //Showcasing ShowHud() working
            //ResetGame();
            //ShowHUD();

            //Showcasing Healing -10 not working, healing 50, and healing over 100, 
            //ResetGame();
            //health = 1;         // health is being set to 1 for demonstration.
            //Heal(-10);
            //Heal(50);
            //Heal(150);

            //Showcasing Shield
            //ResetGame();
            //shield = 1;
            //RegenerateShield(-10);
            //RegenerateShield(50);
            //RegenerateShield(150);


















            UnitTestHealthSystem();
            //UnitTestXPSystem();
        }
        // v NO CHANGING ANYTHING IN HERE v
        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives                     // I spent 40 minutes stuck because it stayed the same for one button press when I was assuming it would change
            shield = 5;                                                     // but it was actually just running properly
            health = 100;   
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input                                
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;                        
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        //static void UnitTestXPSystem()
        //{
        //    Debug.WriteLine("Unit testing XP / Level Up System started...");

        //    // IncreaseXP()

        //    // IncreaseXP() - no level up; remain at level 1
        //    xp = 0;
        //    level = 1;
        //    IncreaseXP(10);
        //    Debug.Assert(xp == 10);
        //    Debug.Assert(level == 1);

        //    // IncreaseXP() - level up to level 2 (costs 100 xp)
        //    xp = 0;
        //    level = 1;
        //    IncreaseXP(105);
        //    Debug.Assert(xp == 5);
        //    Debug.Assert(level == 2);

        //    // IncreaseXP() - level up to level 3 (costs 200 xp)
        //    xp = 0;
        //    level = 2;
        //    IncreaseXP(210);
        //    Debug.Assert(xp == 10);
        //    Debug.Assert(level == 3);

        //    // IncreaseXP() - level up to level 4 (costs 300 xp)
        //    xp = 0;
        //    level = 3;
        //    IncreaseXP(315);
        //    Debug.Assert(xp == 15);
        //    Debug.Assert(level == 4);

        //    // IncreaseXP() - level up to level 5 (costs 400 xp)
        //    xp = 0;
        //    level = 4;
        //    IncreaseXP(499);
        //    Debug.Assert(xp == 99);
        //    Debug.Assert(level == 5);

        //    Debug.WriteLine("Unit testing XP / Level Up System completed.");
        //    Console.Clear();
        //}

        // ^ NO CHANGING ANYTHING IN HERE ^

        static void ShowHUD()
        {
            Console.WriteLine($"Shield: {shield}");

            Console.Write($"Health: {health}%"); //may remove the % depending on how it looks, brightspace said "(0..100%)"
            if (health <= 10)
            {
                healthStatus = " (Imminent Danger)";
            }
            else if (health <= 50)
            {
                healthStatus = " (Badly Hurt)";
            }
            else if (health <= 75)
            {
                healthStatus = (" (Hurt)");
            }
            else if (health <= 99)
            {
                healthStatus = (" (Healthy)");
            }
            else
            {
                healthStatus = (" (Perfect Health)");
            }
            Console.WriteLine(healthStatus);
            Console.WriteLine($"Lives: {lives}");

            Console.ReadKey();

            Console.Clear();
        }
        static void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                ShowHUD();
                Console.ForegroundColor = ConsoleColor.Red;                         //I fought for three hours trying to make this work
                Console.WriteLine("Error detected: Damage can't be negative.\n");
                Console.ResetColor();
                return;
            }

            ShowHUD();

            int damagedealt = damage;
            shield -= damagedealt;
            if (shield < 0)
            {
                int piercingDamage = -shield;
                shield = 0;
                health -= piercingDamage;
            }
            if (health < 0) { health = 0; }
        }
        static void Heal(int hp)
        {
            if (hp < 0)
            {
                ShowHUD();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error detected: can't heal 0 health.\n");      
                Console.ResetColor();                                             
                return;
            }

            health += hp;

            if (health > 100) health = 100;

            ShowHUD();
        }
        static void RegenerateShield(int hp)
        {
            if (hp < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error detected: can't regenerate 0 shield.\n");
                Console.ResetColor();
                ShowHUD(); return;
            }

            shield += hp;

            if (shield > 100) { shield = 100; }

            ShowHUD();
        }
        static void Revive()
        {
            if(health <= 0 && lives >= 1)
            {
                health = 100;
                shield = 100;
                --lives;
            }

            ShowHUD();
        }

        static void ResetGame()
        {
            Console.WriteLine("New Game...");

            ShowHUD();
            health = 100;
            shield = 100;
            healthStatus = (" (Perfect Health)");
            lives = 3;

        }
    }
}

//C#: Health System:

//Code a health system (and an xp system) and showcase that it works.

//In addition to showcasing your systems work, you must also incorporate provided unit test code -- copy the unit test methods I created for you into your own project. These unit test will assess your code -- this will determine your grade.

//range checking = clamp variables to their ranges
//error checking = handles (incorrectly) passing in negative numbers, such as TakeDamage(-10)
//display error message that describes what happened

//do not modify values
//no actual gameplay required
//no hard coding


//Extra Mile - XP/Level Up System - Technical Specifications:

//variables:
//xp integer; 0..(new!)
//level integer; 1..(new!)
//methods:
//IncreaseXP(int exp) method
// modifies xp and level
//leveling up uses up xp -- this make it easier to code (new!)
//ShowHUD() method extended
//shows previous stats (see health system for reference)
//shows xp
//shows level
//xp/level design:
//same design as before, only now costs xp to level up -- easier to code (new!)
//start at 0 xp
//start at level 1
//at level 1, costs 100 xp to level up to level 2
//at level 2, costs 200 xp to level up to level 3
//at level 3, costs 300 xp to level up to level 4
//etc.
//no hard coding
//Feel free to add more extra mile, including more specs that do not interfere with given tech specs. In addition, not instead of.
//Tips:

//Overwhelmed? Divide and conquer.
//ResetGame() method
//resets all variables to default values
//useful for testing
//evolve methods such as ShowHUD() showing changes such as +5 and -10 after the values
//Purpose:

//To learn fundamentals of C# programming, outside of Unity.
//To code a program that demonstrates your knowledge of variables, methods, and conditionals.
//To being to familiarize yourself with the idea of systems and APIs.
//Minimal Requirements:

//All minimal requirements must be met, else work is unaccepted and evaluates to zero (0).
//Submission Requirements:

//Submit the following:

//build
//GitHub link
//Testing Code Example:

//Test and showcase your code works:




// https://nscconline.brightspace.com/d2l/lms/dropbox/user/folder_submit_files.d2l?db=583995&grpid=0&isprv=0&bp=0&ou=356685

// Old Code----------------------------------------------------------------------------

//switch (health)
//{
//    case 4: if (health == 100) healthStatus = " (Perfect Health)"; break;     // Here is the switch I tried to use but couldn't get to work.
//    case 3: if (health <= 99) healthStatus = " (Healthy)"; break;             // The case needed to be the numbers, not a generic 0, 1, 2, 3, 4
//    case 2: if (health <= 75) healthStatus = " (Hurt)"; break;                // using something like case <= is a newer feature so i gave up
//    case 1: if (health <= 50) healthStatus = " (Badly Hurt)"; break;  
//    case 0: if (health <= 10) healthStatus = " (Imminent Danger)"; break;
//}