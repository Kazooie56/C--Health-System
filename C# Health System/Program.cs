using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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

        static int xp = 0;
        static int level = 1;

        static void Main(string[] args)
        {
            UnitTestHealthSystem();
            UnitTestXPSystem();

            Console.Clear();
            ResetGame();                // Showcasing ResetGame() working
            ShowHUD();                  // working

            TakeDamage(50);
            ShowHUD();
            TakeDamage(150);
            ShowHUD();
            Revive();
            ShowHUD();
            TakeDamage(150);
            ShowHUD();
            Heal(50);
            ShowHUD();
            RegenerateShield(50);
            ShowHUD();
            IncreaseXP(100);
            ShowHUD();
            IncreaseXP(200);
            ShowHUD();

            // show taking damage, taking a ton of damage, show revive, show regenhealth, shields, and xp.



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

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            IncreaseXP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            IncreaseXP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            IncreaseXP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            IncreaseXP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            IncreaseXP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }

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

            if (lives > 99) { lives = 99; }            // just so lives have the 99 range technical specification even though it goes unused.
            Console.WriteLine($"Lives: {lives}");

            Console.WriteLine($"\nExperience: {xp}");
            Console.WriteLine($"Level: {level}");

            Console.ReadKey();

            Console.Clear();
        }
        static void IncreaseXP(int exp)
        {
            if (exp < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error detected: cant gain less than 1 experience.\n");
                Console.ResetColor();
                return;
            }

            xp += exp;

            while (xp >= level * 100) // check if enough XP to level up
            {
                xp -= level * 100; // subtract XP needed for this level
                level++;            // increase level
            }
        }

        static void TakeDamage(int damage)
        {



            if (damage < 0)
            {
                //Console.ForegroundColor = ConsoleColor.Red;                         //I fought for three hours trying to make this work so the console would show up below. I don't know why I did that.
                //Console.WriteLine("Error detected: Damage can't be negative.\n");
                //Console.ResetColor();
                return;
            }

            int damagedealt = damage;
            shield -= damagedealt;
            if (shield < 0)
            {
                int piercingDamage = -shield;
                shield = 0;
                health -= piercingDamage;
            }
            if (health < 0) { health = 0; }     // I wrote this back when I called ShowHUD() in all my methods. // NEEDS TO BE CALLED LAST OTHERWISE YOU START DEALING 90 DAMAGE AT THE BEGINNING OF UNITTESTHEALTHSYSTEM() FOR NO REASON - 12:05 AM            
        }
        static void Heal(int hp)
        {
            if (hp < 0)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("Error detected: can't heal less than 1 health.\n");      
                //Console.ResetColor();
                return;
            }

            health += hp;

            if (health > 100) health = 100;
        }
        static void RegenerateShield(int hp)
        {
            if (hp < 0)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("Error detected: can't regenerate less than 1 shield.\n");
                //Console.ResetColor();
                return;
            }

            shield += hp;

            if (shield > 100) { shield = 100; }
        }
        static void Revive()
        {
            if(health >= 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error detected: you cant revive a alive guy.\n");
                Console.ResetColor();
                return;
            }
            else if (lives < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error detected: You have no more lives left.\n");
                Console.ResetColor();
                return;
            }
            health = 100;
            shield = 100;
            --lives;

            ShowHUD();
        }

        static void ResetGame()
        {
            Console.WriteLine("New Game...");

            health = 100;
            shield = 100;
            healthStatus = (" (Perfect Health)");
            lives = 3;

            xp = 0;
            level = 1;

            ShowHUD();
        }
    }
}

// https://nscconline.brightspace.com/d2l/lms/dropbox/user/folder_submit_files.d2l?db=583995&grpid=0&isprv=0&bp=0&ou=356685

// Old Code----------------------------------------------------------------------------

// ////Showcasing Heal()
//ShowHUD();
//Console.WriteLine("health is being set to 1 for demonstration.");
//Console.WriteLine("error message as intended for values less than 1.");
//health = 1;
//Heal(-10);
//ShowHUD();
//Console.WriteLine("health goes up by 50");
//Heal(50);                   // 
//ShowHUD();
//Heal(150);                  // healing over 100 without going over maximum
//ShowHUD();

//////Showcasing RegenerateShield()
//ResetGame();
//shield = 1;                 // shield is being set to 1 for demonstration.
//RegenerateShield(-10);      // error message as intended for values less than 1.
//ShowHUD();
//RegenerateShield(50);       // shield goes up by 50
//ShowHUD();
//RegenerateShield(150);      // shield increased by over 100 without going over maximum
//ShowHUD();

//////Showcasing TakeDamage() with -10 damage not working, then taking 50 damage to shields first, then 100 damage to show the damage transfers over correctly from shield to health, then showing that death doesn't automatically respawn you.
//ResetGame();
//TakeDamage(-10);            // error message as intended for values less than 1.
//ShowHUD();
//TakeDamage(50);             // deals 50 damage to shields first
//ShowHUD();
//TakeDamage(100);            // deals 50 damage to shields first then the remaining damage is added to health.
//ShowHUD();
//TakeDamage(300);            // On brightspace you say revive() should be called when you die but in class you said you should not have it call when you have 0 health left because it breaks the code we needed to run it through. That's why my code doesn't do that.
//ShowHUD();

//////Showcasing Revive()
//ResetGame();
//shield = 0; health = 1; lives = 1;  // This is to make him have try to revive with health remaining, which will fail
//Revive();                           // showcasing a failed revive while alive
//ShowHUD();
//health = 0;                         // This is to make him dead so we can use revive
//Revive();                           // a successful revive
//ShowHUD();
//health = 0;                         // this is to show what happens if you try and revive with no lives left.
//Revive();                           // a failed revive while dead
//ShowHUD();

////Showcasing IncreaseXP()   
//ResetGame();
//IncreaseXP(-10);                // error message as intended for values less than 1
//ShowHUD();
//IncreaseXP(100);                // shows 100 being the first level up breakpoint
//ShowHUD();
//IncreaseXP(100);                // shows how it increased by 100 since last level
//ShowHUD();
//IncreaseXP(100);                // shows that 200 is the correct amount of how much it needed to be increased
//ShowHUD();
//IncreaseXP(700);                // adding 700 and getting exactly level 5 proves the pattern will continue
//ShowHUD();

//switch (health)
//{
//    case 4: if (health == 100) healthStatus = " (Perfect Health)"; break;     // Here is the switch I tried to use but couldn't get to work.
//    case 3: if (health <= 99) healthStatus = " (Healthy)"; break;             // The case needed to be the numbers, not a generic 0, 1, 2, 3, 4
//    case 2: if (health <= 75) healthStatus = " (Hurt)"; break;                // using something like case <= is a newer feature so i gave up
//    case 1: if (health <= 50) healthStatus = " (Badly Hurt)"; break;  
//    case 0: if (health <= 10) healthStatus = " (Imminent Danger)"; break;
//}