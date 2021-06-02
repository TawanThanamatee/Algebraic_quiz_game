using System;

namespace Algebraic_quiz_game
{
    class Program
    {
        static void Main(string[] args)
        {
            double score = 0;
            double score_sum = 0;
            int numberlevel = 0;
            double correct_answer = 0;
            int many_answer;
            bool check_input = false;



            while (check_input == false)
            {
                
                Difficulty level;
                level = (Difficulty)numberlevel;

                //SetNumberLevel
                if (numberlevel == 0)
                {
                    numberlevel += 3;
                }
                else if (numberlevel == 1)
                {
                    numberlevel += 4;
                }
                else if (numberlevel == 2)
                {
                    numberlevel += 5;
                }
             
                //StartProgram
                Console.WriteLine("\nScore: {0}, Difficulty: {1}", score_sum, level);
                Console.WriteLine("Please input. \n Number 0 = Start \n Number 1 = Setting \n Number 2 = End");
                int numbersetting = int.Parse(Console.ReadLine());
                if (numbersetting == 0 || numbersetting == 1 || numbersetting == 2)
                {
                    //PlayGame
                    if (numbersetting == 0)
                    {
                        //StartDateTime
                        DateTimeOffset.Now.ToUnixTimeSeconds();
                        DateTime start_time = DateTime.UtcNow;
                        long start_unix_time = ((DateTimeOffset)start_time).ToUnixTimeSeconds();

                        //CallGenerateRandomProblems&ReturnProblemArray
                        Problem[] problem = GenerateRandomProblems(numberlevel);
                        foreach (Problem Text in problem)
                        {
                            Console.WriteLine("\nQuestion : " + Text.Message);
                            Console.Write("Answer : ");
                            int Answer = int.Parse(Console.ReadLine());
                            if (Answer == Text.Answer)
                            {
                                correct_answer++;
                            }
                            else
                            {
                                Console.WriteLine("Wrong answer.");
                            }
                        }

                        many_answer = numberlevel;

                        //ResetNumberLevel
                        if (numberlevel == 3)
                        {
                            numberlevel -= 3;
                        }
                        else if (numberlevel == 5)
                        {
                            numberlevel -= 4;
                        }
                        else if (numberlevel == 7)
                        {
                            numberlevel -= 5;
                        }
                        //StopDateTime
                        DateTime stop_time = DateTime.UtcNow;
                        long stop_unix_time = ((DateTimeOffset)stop_time).ToUnixTimeSeconds();

                        //SummationDateTime
                        double spend_time;
                        spend_time = stop_unix_time - start_unix_time;

                        //SummationScore
                        score = (correct_answer / many_answer) * (25 - Math.Pow(numberlevel, 2)) / (Math.Max(spend_time, 25 - Math.Pow(numberlevel, 2))) * Math.Pow(((2 * numberlevel) + 1), 2); ;
                        score_sum += score;

                        //ResetCorrectAnswer
                        correct_answer = 0;
                    }
                    //Setting
                    else if (numbersetting == 1)
                    {
                        bool check_level = false;
                        while (check_level == false)
                        {
                            Console.WriteLine("Please input level. \n Number 0 = Easy \n Number 1 = Normal \n Number 2 = Hard");
                            numberlevel = int.Parse(Console.ReadLine());
                            if (numberlevel != 0 && numberlevel != 1 && numberlevel != 2)
                            {
                                Console.WriteLine("Please input level tryagain. \n" + numberlevel);
                            }
                            else
                                check_level = true;
                        }
                    }
                    //End
                    else if (numbersetting == 2)
                    {
                        check_input = true;
                    }
                    else
                        Console.WriteLine("Please input 0 - 2. \n"); 
                }
                else
                    Console.WriteLine("Please input 0 - 2. \n");
            }
        }
        enum Difficulty
        {
            Easy,
            Normal,
            Hard 
        }
        struct Problem
        {
            public string Message;
            public int Answer;

            public Problem(string message, int answer)
            {
                Message = message;
                Answer = answer;
            }
        }
        static Problem[] GenerateRandomProblems(int numProblem)
        {
            Problem[] randomProblems = new Problem[numProblem];

            Random rnd = new Random();
            int x, y;
            for (int i = 0; i < numProblem; i++)
            {
                x = rnd.Next(50);
                y = rnd.Next(50);
                if (rnd.NextDouble() >= 0.5)
                {
                    randomProblems[i] = new Problem(String.Format("{0} + {1} = ?", x, y), x + y);
                }
                else
                    randomProblems[i] = new Problem(String.Format("{0} - {1} = ?", x, y), x - y);
            }
            
            return randomProblems;
        }

    }
}
