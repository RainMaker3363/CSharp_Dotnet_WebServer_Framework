using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Study
{
    public enum ClassType
    {
        Knight,
        Archer,
        Mage
    }

    public class Player
    {
        public ClassType classtype { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public List<int> items { get; set; } = new List<int>();
    }

    class Program
    {
        static Task Test()
        {
            Console.WriteLine("Start Test");
            Task t = Task.Delay(3000);

            return t;
        }

        static async Task<int> TestAsync()
        {
            Console.WriteLine("Start TestAsync");

            await Task.Delay(3000);

            Console.WriteLine("End TestAsync");

            return 1;
        }

        /*
        static async Task Main(string[] args)
        {
            Task<int> taskint = TestAsync();

            int result = await taskint;


            Console.WriteLine("while start");
            Console.WriteLine($"{result}");

            while(true)
            {

            }
        }
        */

        static List<Player> _players = new List<Player>();
        static void Main(string[] args)
        {
            
            Random rand = new Random();

            
            for(int i = 0; i< 100; ++i)
            {
                ClassType type = ClassType.Knight;
                switch(rand.Next(0, 3))
                {
                    case 0:
                        type = ClassType.Knight;
                        break;

                    case 1:
                        type = ClassType.Archer;
                        break;

                    case 2:
                        type = ClassType.Mage;
                        break;
                }

                Player player = new Player()
                {
                    classtype = type,
                    Level = rand.Next(1, 100),
                    Hp = rand.Next(100, 300),
                    Attack = rand.Next(5, 50)
                };

                for(int j = 0; j < 5; ++j)
                {
                    player.items.Add(rand.Next(1, 101));
                }

                _players.Add(player);
            }

            // 일반 버전
            {
                var players = GetHighLevelKngiths();
                foreach(var p in players)
                {
                    Console.WriteLine($"{p.Level} {p.Hp}");
                }
            }

            Console.WriteLine($"=======================================================================");
            Console.WriteLine($"=======================================================================");

            // LINQ 버전
            {
                // from (foreach)
                // where (필터 역할 = 조건에 부합하는 데이터만 걸러낸다
                // order by (정렬을 수행. 기본적으로는 오름차순)
                // select (최종 결과물 추출 -> 가공해서 추출)

                var players = from p in _players
                where p.classtype == ClassType.Knight && p.Level >= 50
                orderby p.Level
                select p;

                foreach (var p in players)
                {
                    Console.WriteLine($"{p.Level} {p.Hp}");
                }
            }

            // 중첩 from
            // Ex) 모든 아이템 목록을 추출
            {
                var playerItems = from p in _players
                from i in p.items
                where i < 30
                select new { p, i };

                var li = playerItems.ToList();

            }

            // grouping
            {
                var playerByLevel = from p in _players
                                    group p by p.Level into g
                                    orderby g.Key
                                    select new { g.Key, Players = g };
            }

            // join (내부 조인)
            {
                List<int> levels = new List<int>() { 1, 5, 10 };

                var playerLevels = 
                    from p in _players
                    join l in levels
                    on p.Level equals l
                    select p;
            }

            {
                var players =
                    from p in _players
                    where p.classtype == ClassType.Knight && p.Level >= 50
                    orderby p.Level ascending
                    select p;

                var players2 = _players
                    .Where(p => p.classtype == ClassType.Knight && p.Level >= 50)
                    .OrderBy(p => p.Level)
                    .Select(p => p);
            }
        }

        static public List<Player> GetHighLevelKngiths()
        {
            List<Player> players = new List<Player>();

            foreach(var player in _players)
            {
                if (player.classtype != ClassType.Knight)
                    continue;
                if (player.Level < 50)
                    continue;

                players.Add(player);
            }

            players.Sort((lhs, rhs) =>
            {
                return lhs.Level - rhs.Level;
            });

            return players;
        }
    }
}
