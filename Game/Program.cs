using System;
using System.Collections.Generic;

namespace Game
{
    class Program
    {
        private static string formatError = "输入格式错误,请重输：";
        private static string lineFormatError = "输入行数格式错误,请重输：";
        private static string countFormatError = "输入挑选数量格式错误,请重输：";
        private static string lineLimitError = "输入行数不在范围内,请重输：";
        private static string countLimitError = "输入挑选数量不在范围内,请重输：";

        static void Main(string[] args)
        {
            Game();
        }

        private static void Game()
        {
            Console.WriteLine("开始游戏");
            List<int> lines = new List<int> { 3, 5, 7 };
            int count = GetCount(lines);

            int step = 1;
            bool gameStop = false;
            while (!gameStop)
            {
                int user = step % 2 == 1 ? 1 : 2;
                Console.WriteLine($"user{user}的环节,请选择某行某些根牙签,以,分隔：");
                Console.WriteLine($"目前游戏状态:{GetStatus(lines)}");

                while (true)
                {
                    string line = Console.ReadLine();
                    var lineAttr = line.Split(',');
                    if (lineAttr.Length != 2)
                    {
                        Console.WriteLine(formatError);
                        continue;
                    }
                    else
                    {
                        var curLine = 0;
                        try
                        {
                            curLine = Convert.ToInt32(lineAttr[0]);
                        }
                        catch
                        {
                            Console.WriteLine(lineFormatError);
                            continue;
                        }

                        if (curLine <= 0 || curLine > 3)
                        {
                            Console.WriteLine(lineLimitError);
                            continue;
                        }
                        else
                        {
                            var curCount = 0;
                            try
                            {
                                curCount = Convert.ToInt32(lineAttr[1]);
                            }
                            catch
                            {
                                Console.WriteLine(countFormatError);
                                continue;
                            }

                            if (curCount <= 0 || curCount > count || curCount > lines[curLine - 1])
                            {
                                Console.WriteLine(countLimitError);
                                continue;
                            }
                            else
                            {
                                lines[curLine - 1] -= curCount;
                            }

                            count = GetCount(lines);
                            if (count == 0 || (count == 2 && lines.FindAll(p => p == 0).Count == 1))
                            {
                                Console.WriteLine($"user{user}输了");
                                gameStop = true;
                            }
                            else if (count == 1)
                            {
                                Console.WriteLine($"user{2 / user}输了");
                                gameStop = true;
                            }
                        }
                    }

                    step++;
                    break;
                }
            }
            
            while (true)
            {
                Console.WriteLine($"游戏结束,重新开始游戏?Y/N");
                string again = Console.ReadLine().Trim().ToLower();
                if (again != "y" && again != "n")
                {
                    continue;
                }
                else if (again == "n")
                {
                    Console.WriteLine($"游戏结束");
                    break;
                }
                else
                {
                    Game();
                }
            }
        }

        private static int GetCount(List<int> lines)
        {
            int count = 0;
            if (lines != null && lines.Count > 0)
            {
                lines.ForEach(p => count += p);
            }

            return count;
        }

        private static string GetStatus(List<int> lines)
        {
            var statusList = new List<string>();
            if (lines != null && lines.Count > 0)
            {
                for (int i = 1; i<= lines.Count; i ++)
                {
                    statusList.Add($"第{i}行还有{lines[i-1]}根牙签");
                }
            }

            return string.Join(",", statusList);
        }
    }
}
