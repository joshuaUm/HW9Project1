/// Homework No. 9 Project No. 1
/// File Name : Program.cs
/// @author : Joshua Um
/// Date : November 1 2021
/// 
/// Problem Statement : Write a program to simulate the duel using two strategies : the first is for each man to shoot at the most accurate shooter still alive, and for Aaron to miss his first shot. 
/// 
/// Plan:
/// 1. Create duelist objects.
/// 2. Simulate 10,000 duels with each duelist targetting the highest accuracy duelist.
/// 3. Simulate 10,000 duels with Aaron missing his first shot.
/// 
using System;

namespace HW9Project1
{
    class Program
    {
        static void Main(string[] args)
        {

            Duelist[] duelists = { new Duelist("Aaron", 33), new Duelist("Bob", 50), new Duelist("Charlie", 99.95) };

            simulateNDuels(10000, duelists);

            simulateNDuels(10000, duelists, true);
        }

        public static void simulateNDuels( int n , Duelist[] duelists, bool altStrategy = false)
        {



            Console.WriteLine("Simulating " + n + " duel" + (n == 1 ? "" : "s") + (altStrategy ? " with alternate strategy" : ""));

            int aaronWins = 0;
            int bobWins = 0;
            int charlieWins = 0;
            for (int i = 0, count = n; i < count; i++)
            {
                int winnerIndex = simulateDuel(duelists, altStrategy);

                switch (winnerIndex)
                {
                    case 0:
                        aaronWins++;
                        break;
                    case 1:
                        bobWins++;
                        break;
                    case 2:
                        charlieWins++;
                        break;
                }

            }


            Console.WriteLine("Aaron won " + aaronWins + "/" + n + " duels or " + Math.Round((aaronWins /(double)n) * 100, 2) + "%");
            Console.WriteLine("Bob won " + bobWins + "/" + n + " duels or " + Math.Round((bobWins / (double)n) * 100, 2) + "%");
            Console.WriteLine("Charlie won " + charlieWins + "/" + n + " duels or " + Math.Round((charlieWins / (double)n) * 100, 2) + "%\n");
        }



        public static int simulateDuel(Duelist[] duelists, bool altStrategy)
        {
            int duelistsAlive = duelists.Length;
            int selectedIndex = -1;

            for (int i = 0, count = duelists.Length; i < count; i++)
            {
                duelists[i].IsAlive = true;
            }




                while (duelistsAlive > 1)
            {
                for (int i = 0, count = duelists.Length; i < count; i++)
                {
                    Duelist selectedDuelist = duelists[i];
                    selectedIndex = i;
                    if(altStrategy && selectedIndex == 0)
                    {
                  
                        altStrategy = false;
                        continue;
                    }



                    if (!selectedDuelist.IsAlive)
                    {
                        continue;
                    }


                    Duelist target = findHighestAcc(duelists,selectedDuelist);

                    if (!selectedDuelist.shootAtTarget(target))
                    {

                        if (--duelistsAlive == 1) break;
                    
                    }
                }
            }
            return selectedIndex;
        }

        public static Duelist findHighestAcc(Duelist[] duelists, Duelist self)
        {
            Duelist mostAccurate = duelists[0];
            for (int i = 0, count = duelists.Length; i < count; i++)
            {
                Duelist selectedDuelist = duelists[i];
                if (!selectedDuelist.Equals(self) && selectedDuelist.IsAlive && mostAccurate.ShootingAccuracy < selectedDuelist.ShootingAccuracy)
                    mostAccurate = selectedDuelist;
            }

            return mostAccurate;

        }


    }




    class Duelist
    {

        private static Random rng = new Random();

        public string Name { get; private set; }
        public double ShootingAccuracy { get;  private set; }

        public bool IsAlive { get; set; }



        public Duelist(string name, double shootingAccuracy)
        {
            Name = name;
            ShootingAccuracy = shootingAccuracy;
            IsAlive = true;
        }


        public  bool shootAtTarget(Duelist target)
        {
            int rand = rng.Next(101);

            if(rand <= ShootingAccuracy)
            {
                target.IsAlive = false;
            }
                

            return target.IsAlive;
        }




        public override string ToString()
        {
            return Name + " Acc%: " + ShootingAccuracy + " isAlive: " + IsAlive;
        }




        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Duelist))
            {
                return false;
            }

            Duelist other = (Duelist)obj;

            return Name.Equals(other.Name) &&
                   ShootingAccuracy.Equals(other.ShootingAccuracy) &&
                   IsAlive.Equals(other.IsAlive);
        }



    }
}
