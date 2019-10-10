using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Game
{
    class Program
    {
        struct Vector2
        {
            public int x;
            public int y;
        }
        enum E_GameObjectType
        {
            Player=0,      
            Normal=1,       //70
            Wall=2,       
            Moster=3,       //10
            Treasure=4,     //10
            Trap=5,          //10
            Boss=6
        }
        struct Boss
        {
            public string name;
            public int ATC;
            public int Hp;
            public int Sb;
        }
        struct GAMEObject
        {
            public Vector2 pos;
            public string icon;
            public E_GameObjectType type;

            void SetIconData()
            {
                switch (type)
                {
                    case E_GameObjectType.Player:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        icon = "你";
                        break;                
                    case E_GameObjectType.Normal:
                        Console.ForegroundColor = ConsoleColor.White;
                        icon = "□";
                        break;
                    case E_GameObjectType.Boss:
                        
                        icon = "■";
                        break;
                    case E_GameObjectType.Moster:
                        Console.ForegroundColor = ConsoleColor.Red;
                        icon = "怪";
                        break;
                    case E_GameObjectType.Treasure:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        icon = "◎";
                        break;
                    case E_GameObjectType.Trap:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        icon = "井";
                        break;
                    
                }
            }

            public void Draw()
            {
                Console.SetCursorPosition(pos.x*2,pos.y);
                SetIconData();
                Console.Write(icon);
            }


        }

        struct Moster
        {
            public  string name;
            public  int Hp;
            public  int ATc;
            public int Sb;
           
        }
        struct Player
        {
            public  int Hp;
            public  string name;
            public  int ATC;
            public  int Sb;
            public  int index;
            public GAMEObject piece;
            

            /// <summary>
            /// 掷骰子
            /// </summary>
            /// <param name="map"></param>
            public void Move(Map map) {
               int num= GetShaiZhi();
                SetIndex_Player(num, map);
            }
            public void SetIndex_Player(int i,Map map)
            {
                Map.index_Player1 += i ;

                if (Map.index_Player1<0)
                {
                    Map.index_Player1 = 0;
                }
                else if (Map.index_Player1>=map.mapData.Length-1)
                {
                    Map.index_Player1 = map.mapData.Length - 1;
                }

                piece.pos = map.mapData[Map.index_Player1].pos;

            }

            public void MapEvent(Map map,Player player)
            {
                E_GameObjectType type = map.mapData[Map.index_Player1].type;
                
                switch (type)
                {
                    case E_GameObjectType.Player:
                        
                        break;
                    
                    case E_GameObjectType.Normal:
                        break;
                    case E_GameObjectType.Boss:
                        FightBoss(ref player);
                        break;
                    case E_GameObjectType.Moster:                      
                        
                        Fight(ref player);
                        break;
                    case E_GameObjectType.Treasure:
                        bool up = false;
                        GetTreasure(ref player,ref up);
                        if (up==true)
                        {
                            SetIndex_Player(+6, map);
                        }
                        Console.SetCursorPosition(90, 20);
                        Console.WriteLine("" + player.name);
                        Console.SetCursorPosition(90, 21);
                        Console.WriteLine("生命值 " + player.Hp + " ");
                        Console.SetCursorPosition(90, 22);
                        Console.WriteLine("攻击力 " + player.ATC + " ");
                        Console.SetCursorPosition(90, 23);
                        Console.WriteLine("闪避率 " + player.Sb + " ");
                        break;

                    case E_GameObjectType.Trap:
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你踩到了陷阱");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("受到了30点伤害---回车继续");
                        Console.ReadLine();
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("            ");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("                         ");
                        player.Hp =player.Hp - 30;
                        Console.SetCursorPosition(90, 20);
                        Console.WriteLine("" + player.name);
                        Console.SetCursorPosition(90, 21);
                        Console.WriteLine("生命值 " + player.Hp + " ");
                        Console.SetCursorPosition(90, 22);
                        Console.WriteLine("攻击力 " + player.ATC + " ");
                        Console.SetCursorPosition(90, 23);
                        Console.WriteLine("闪避率 " + player.Sb + " ");
                        if (player.Hp==0)
                        {
                            Console.SetCursorPosition(90, 14);
                            Console.WriteLine("你死了");
                        }
                        break;
                    
                }


            }



        }
        struct Map
        {
            public int length;
            public GAMEObject[] mapData;
            public static int index_Player1;

          
            /// <summary>
            /// 根据地图形状和数量推算GameObject的数量
            /// </summary>
            void GetMapLenth(int count)
            {
                int sum = 0;
                for (int i = 0; i <=count; i++)
                {
                    if (i%6==1)
                    {
                        sum += 32;
                    }
                    else if (i%6==2)
                    {
                        sum += 41;
                    }
                    else if (i%6==3)
                    {
                        sum += 30;
                    }
                    else if (i%6==4)
                    {
                        sum += 20;
                    }
                    else if (i%6==5)
                    {
                        sum += 20;
                    }
                    else if (i%6==0)
                    {
                        sum += 5;
                    }
                    
                }
                mapData = new GAMEObject[sum];
                length = sum;
            }

            void CreateMapGameObject()
            {
                for (int i = 0; i < length; i++)
                {
                    GAMEObject go = new GAMEObject();
                    mapData[i] = go;
                }
                
            }

            /// <summary>
            /// 赋予地图属性
            /// </summary>
            void SetMapGameObjectType()
            {
                Random r = new Random();
                for (int i = 0; i < length; i++)
                {
                    int number = r.Next(1, 101);
                    if (number<70)
                    {
                       
                        mapData[i].type = E_GameObjectType.Normal;
                    }
                    else if (number<80)
                    {
                       
                        mapData[i].type = E_GameObjectType.Moster;
                    }
                    else if (number<90)
                    {
                        
                        mapData[i].type = E_GameObjectType.Trap;
                    }
                    else if (number<=100)
                    {
                        mapData[i].type = E_GameObjectType.Treasure;
                    }
                    mapData[length - 1].type = E_GameObjectType.Boss;
                }


            }

            void SetMapPosData(Vector2 startPos,int count)
            {
                mapData[0].pos = startPos;

                int mapindex = 1;
                for (int i = 1; i <=count; i++)
                {

                    if (i%6==1)
                    {
                        for (int j = 0; j < 32; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex - 1].pos.x + 1;
                            pos.y = mapData[mapindex - 1].pos.y;

                            mapData[mapindex].pos = pos;
                            mapindex++;
                        }
                    }
                    else if (i%6==2)
                    {
                        for (int j = 0; j < 41; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex-1].pos.x;
                            pos.y=mapData[mapindex-1].pos.y+1;

                            mapData[mapindex].pos = pos;
                            mapindex++;

                        }
                    }
                    else if (i%6==3)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex - 1].pos.x - 1;
                            pos.y = mapData[mapindex - 1].pos.y;

                            mapData[mapindex].pos = pos;
                            mapindex++;
                        }
                        

                    }
                    else if (i%6==4)
                    {//向上画
                        for (int j = 0; j < 20; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex - 1].pos.x;
                            pos.y = mapData[mapindex - 1].pos.y - 1;

                            mapData[mapindex].pos = pos;
                            mapindex++;
                        }
                    }
                    else if (i%6==5)
                    {//向右画
                        for (int j = 0; j <20; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex - 1].pos.x + 1;
                            pos.y = mapData[mapindex - 1].pos.y;

                            mapData[mapindex].pos = pos;
                            mapindex++;
                        }

                    }
                    else if (i%6==0)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Vector2 pos = new Vector2();
                            pos.x = mapData[mapindex - 1].pos.x ;
                            pos.y = mapData[mapindex - 1].pos.y-1;

                            mapData[mapindex].pos = pos;
                            mapindex++;
                        }
                    }


                }

            }


            public void RandomData(Vector2 startPos,int count)
            {
                GetMapLenth(count);
                CreateMapGameObject();
                SetMapGameObjectType();
                SetMapPosData(startPos, count);

            }

            public void DrawMap()
            {
                for (int i = 0; i < mapData.Length; i++)
                {
                    mapData[i].Draw();
                }
                
            }



        }


        static void Main(string[] args)
        {
            Console.SetBufferSize(120, 80);
            
            Boss boss = new Boss();
            boss.name = "尸王";
            boss.ATC = 50;
            boss.Hp = 150;
            boss.Sb = 15;
            int numb = 1;
            int Person = 0;
            while (true)
            {
                Console.CursorVisible = false;
                switch (numb)
                {
                    case 1:
                        {
                            Console.SetCursorPosition(55, 10);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("鬼    吹     灯");
                            Console.SetCursorPosition(55, 13);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  之古墓逃脱");
                            Console.SetCursorPosition(57, 15);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("1.开始游戏");
                            Console.ReadKey(true);
                            if ((Console.ReadKey(true).Key == ConsoleKey.D1)|| (Console.ReadKey(true).Key == ConsoleKey.NumPad1))
                            {
                                numb = 2;
                                Console.Clear();
                                break;
                            }
                            

                        }
                           break;
                    case 2:
                        {
                            while (true)
                            {
                                #region 第一幕
                                Console.SetCursorPosition(10, 5);
                                Console.WriteLine("一个风和日丽的早晨");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 6);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("小胡");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("  小王");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("  小刘");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("  小牙");
                                Console.WriteLine();
                                Console.SetCursorPosition(10, 7);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("这四个中二病少年，又开始了他们的幻想");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 9);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("小胡:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("好无聊啊，好想盗个墓");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("小王:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("你看鬼吹灯看入迷了？天天想着挖别人祖坟吗？");
                                Console.ReadLine();
                                Console.Write("\t  --说实话我也想");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 12);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("小刘:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("还真别说，我听说后山有人发现一个大洞，懂行的人说是什么将军的墓");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 13);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("小牙:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("那还等什么啊！开拔！");
                                Console.ReadLine(); 
                                #endregion
                                Console.Clear();
                                Console.SetCursorPosition(10, 5);
                                Console.WriteLine("4人来到山洞前----");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 6);
                                Console.WriteLine("山洞里漆黑一片，但深处似乎有着什么.....");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 7);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("小牙:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("要不还是算了吧....怪恐怖的");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 8);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("小胡:");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("(虽然也怕，但是还是装硬)喊来的是你，喊走的也是你，能不能不拉跨！");
                                Console.ReadLine();
                                Console.SetCursorPosition(10, 9);
                                Console.WriteLine("于是4人往洞里走去....他们还不知道，等待他们的是什么");
                                Console.ReadLine();
                                Console.Clear();
                                numb = 3;
                                break;
                            }
                        }
                        break;
                    case 3:
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(55, 5);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("请选择你扮演的角色");
                            Console.SetCursorPosition(40, 10);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("1.胡巴二    2.王瘦子    3.雪莉刘    4.大银牙");
                            Console.SetCursorPosition(40, 12);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("攻击力中    攻击力高    攻击力中    攻击力低");
                            Console.SetCursorPosition(40, 13);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("闪避率中    闪避率低    闪避率高    闪避率低");
                            Console.SetCursorPosition(40, 14);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("生命值中    生命值高    生命值低    生命值低");
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.D1:
                                    Person = 1;
                                    numb = 4;
                                    
                                    break;
                                case ConsoleKey.D2:
                                    Person = 2;
                                    numb = 4;
                                    break;
                                case ConsoleKey.D3:
                                    Person = 3;
                                    numb = 4;
                                    break;
                                case ConsoleKey.D4:
                                    Person = 4;
                                    numb = 4;
                                    break;
                            }
                        }
                        Console.Clear();
                        break;
                    case 4:
                        {
                            Console.SetCursorPosition(90, 26);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("怪--遭遇怪物");
                            Console.SetCursorPosition(90, 27);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("井--触碰陷阱");
                            Console.SetCursorPosition(90, 28);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("◎--获得宝物");
                            Vector2 pos = new Vector2();
                            pos.x = 1;
                            pos.y = 1;

                            Map map = new Map();
                            map.RandomData(pos, 5);
                            map.DrawMap();

                           

                            GAMEObject picl = new GAMEObject();
                            picl.pos = map.mapData[0].pos;
                            picl.type = E_GameObjectType.Player;


                            Player p1 = new Player();
                            


                            switch (Person)
                            {
                                case 1:
                                    {
                                        p1.name = "胡巴二";
                                        p1.ATC = 15;
                                        p1.Hp = 100;
                                        p1.Sb = 20;
                                        Console.SetCursorPosition(90, 20);
                                        Console.WriteLine("" + p1.name);
                                        Console.SetCursorPosition(90, 21);
                                        Console.WriteLine("生命值 " + p1.Hp + " ");
                                        Console.SetCursorPosition(90, 22);
                                        Console.WriteLine("攻击力 " + p1.ATC + " ");
                                        Console.SetCursorPosition(90, 23);
                                        Console.WriteLine("闪避率 " + p1.Sb + " ");
                                    }
                                    break;
                                case 2:
                                    {
                                        p1.name = "王瘦子";
                                        p1.ATC = 20;
                                        p1.Hp = 120;
                                        p1.Sb = 10;
                                        Console.SetCursorPosition(90, 20);
                                        Console.WriteLine("" + p1.name);
                                        Console.SetCursorPosition(90, 21);
                                        Console.WriteLine("生命值 " + p1.Hp + " ");
                                        Console.SetCursorPosition(90, 22);
                                        Console.WriteLine("攻击力 " + p1.ATC + " ");
                                        Console.SetCursorPosition(90, 23);
                                        Console.WriteLine("闪避率 " + p1.Sb + " ");
                                    }
                                    break;
                                case 3:
                                    {
                                        p1.name = "雪莉刘";
                                        p1.ATC = 15;
                                        p1.Hp = 80;
                                        p1.Sb = 30;
                                        Console.SetCursorPosition(90, 20);
                                        Console.WriteLine("" + p1.name);
                                        Console.SetCursorPosition(90, 21);
                                        Console.WriteLine("生命值 " + p1.Hp + " ");
                                        Console.SetCursorPosition(90, 22);
                                        Console.WriteLine("攻击力 " + p1.ATC + " ");
                                        Console.SetCursorPosition(90, 23);
                                        Console.WriteLine("闪避率 " + p1.Sb + " ");
                                    }
                                    break;
                                case 4:
                                    {
                                        p1.name = "大银牙";
                                        p1.ATC = 10;
                                        p1.Hp = 80;
                                        p1.Sb = 10;
                                        Console.SetCursorPosition(90, 20);
                                        Console.WriteLine("" + p1.name);
                                        Console.SetCursorPosition(90, 21);
                                        Console.WriteLine("生命值 " + p1.Hp+" ");
                                        Console.SetCursorPosition(90, 22);
                                        Console.WriteLine("攻击力 " + p1.ATC + " ");
                                        Console.SetCursorPosition(90, 23);
                                        Console.WriteLine("闪避率 " + p1.Sb + " ");
                                    }
                                    break;
                            }

                            p1.piece = picl;

                            p1.piece.Draw();

                            while (true)
                            {
                                Console.SetCursorPosition(90, 14);
                                Console.WriteLine("按回车键投掷色子移动");
                                Console.ReadLine();
                                Console.SetCursorPosition(90, 14);
                                Console.WriteLine("                    ");

                                p1.Move(map);
                                p1.MapEvent(map, p1);
                                map.DrawMap();
                                p1.piece.Draw();
                            }

                            
                        }
                        break;
                    
                }

            }

            
        }

        static int GetShaiZhi()
        {
            #region 掷色子

            int x = 0;
            Random r = new Random();
            int dianshu = 0;
            for (int i = 0; i < 10;)
            {
                dianshu = r.Next(0, 7);
                while (x % 10000000 == 0)
                {
                    #region 色子点数


                    switch (dianshu)
                    {
                        case 1:
                            {

                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("   ");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" ● ");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("   ");
                                i++;
                            }
                            break;
                        case 2:
                            {
                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" ● ");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("   ");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" ● ");
                                i++;
                            }
                            break;
                        case 3:
                            {

                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("●  ");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" ● ");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("  ●");
                                i++;
                            }
                            break;
                        case 4:
                            {

                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("   ");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                i++;
                            }
                            break;
                        case 5:
                            {

                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" ● ");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                i++;
                            }
                            break;
                        case 6:
                            {

                                Console.SetCursorPosition(90, 8);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 9);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 10);
                                Console.WriteLine("    ");
                                Console.SetCursorPosition(90, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                Console.SetCursorPosition(90, 9);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                Console.SetCursorPosition(90, 10);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("● ●");
                                i++;
                            }
                            break;


                    }
                    #endregion

                    x++;
                }

                x++;


            }
            Console.SetCursorPosition(90, 11);
            Console.WriteLine("点数 {0}点---回车键前进",dianshu);
            Console.ReadLine();
            Console.SetCursorPosition(90, 11);
            Console.WriteLine("                       ");
            return dianshu;
            #endregion
        }

        static void Fight( ref Player player)
        {
            Moster moster = new Moster();
            Random r = new Random();
            int a = r.Next(1, 4);
            switch (a)
            {
                case 1:
                    {
                        moster.name = "黄皮子";
                        moster.Hp = 30;
                        moster.ATc = 10;
                        moster.Sb = 10;
                    }
                    break;
                case 2:
                    {
                        moster.name = "尸蟞";
                        moster.Hp = 20;
                        moster.ATc = 5;
                        moster.Sb = 10;
                    }
                    break;
                case 3:
                    {
                        moster.name = "大粽子";
                        moster.Hp = 50;
                        moster.ATc = 20;
                        moster.Sb = 10;
                    }
                    break;

            }
            Console.SetCursorPosition(90, 14);
            Console.WriteLine("你遭遇了怪物{0}", moster.name);
            Console.SetCursorPosition(90, 15);
            Console.WriteLine("    ---回车继续");
            Console.ReadLine();
            Console.SetCursorPosition(90, 14);
            Console.WriteLine("                         ");
            Console.SetCursorPosition(90, 15);
            Console.WriteLine("               ");
            while (true)
            {
               
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("{0}对你发起了攻击",moster.name);

                int x = r.Next(0, 101);
                if (x>player.Sb )
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("你受到了{0}点伤害---回车键继续",moster.ATc);
                    Console.SetCursorPosition(90, 20);
                    Console.WriteLine("" + player.name);
                    Console.SetCursorPosition(90, 21);
                    Console.WriteLine("生命值 " + player.Hp+" ");
                    Console.SetCursorPosition(90, 22);
                    Console.WriteLine("攻击力 " + player.ATC + " ");
                    Console.SetCursorPosition(90, 23);
                    Console.WriteLine("闪避率 " + player.Sb + " ");
                    Console.ReadLine();
                    player.Hp -= moster.ATc;
                    if (player.Hp==0)
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你死了---回车键继续");
                        Console.ReadLine();
                        Console.Clear();
                        Console.SetCursorPosition(55, 10);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("游  戏  结  束");
                        Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("被你躲开了---回车键继续");
                    Console.ReadLine();
                }
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("按回车键发起进攻");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                            ");
                Console.ReadLine();
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("                            ");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                            ");
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("你发起了攻击");
                Console.ReadLine();
                int y = r.Next(0, 101);
                if (y > moster.Sb)
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("怪物受到了{0}点伤害,还剩{1}", player.ATC,moster.Hp);
                    moster.Hp -= player.ATC;
                    Console.ReadLine();
                    Console.SetCursorPosition(90, 14);
                    Console.WriteLine("                               ");
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("                               ");
                    if (moster.Hp <  0)
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你赢了");
                        Console.ReadLine();
                        
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("怪物躲开了");
                }
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("                               ");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                               ");

            }


        }
     
        static void FightBoss(ref Player player)
        {
            Boss boss = new Boss();
            Console.SetCursorPosition(90, 14);
            Console.WriteLine("你遭遇了怪物{0}", boss.name);
            Console.SetCursorPosition(90, 15);
            Console.WriteLine("    ---回车继续");
            Console.ReadLine();
            Console.SetCursorPosition(90, 14);
            Console.WriteLine("                         ");
            Console.SetCursorPosition(90, 15);
            Console.WriteLine("               ");
            while (true)
            {
                Random r = new Random();
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("{0}对你发起了攻击", boss.name);

                int x = r.Next(0, 101);
                if (x > player.Sb)
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("你受到了{0}点伤害---回车键继续", boss.ATC);
                    Console.SetCursorPosition(90, 20);
                    Console.WriteLine("" + player.name);
                    Console.SetCursorPosition(90, 21);
                    Console.WriteLine("生命值 " + player.Hp + " ");
                    Console.SetCursorPosition(90, 22);
                    Console.WriteLine("攻击力 " + player.ATC + " ");
                    Console.SetCursorPosition(90, 23);
                    Console.WriteLine("闪避率 " + player.Sb + " ");
                    Console.ReadLine();
                    player.Hp -= boss.ATC;
                    if (player.Hp == 0)
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你死了---回车键继续");
                        Console.ReadLine();
                        Console.Clear();
                        Console.SetCursorPosition(55, 10);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("游  戏  结  束");
                        Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("被你躲开了---回车键继续");
                    Console.ReadLine();
                }
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("按回车键发起进攻");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                            ");
                Console.ReadLine();
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("                            ");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                            ");
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("你发起了攻击");
                Console.ReadLine();
                int y = r.Next(0, 101);
                if (y > boss.Sb)
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("怪物受到了{0}点伤害,还剩{1}", player.ATC, boss.Hp);
                    boss.Hp -= player.ATC;
                    Console.ReadLine();
                    Console.SetCursorPosition(90, 14);
                    Console.WriteLine("                               ");
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("                               ");
                    if (boss.Hp < 0)
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你赢了");
                        Console.ReadLine();
                        Console.Clear();
                        Console.SetCursorPosition(55, 10);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("游  戏  结  束");
                        Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(90, 15);
                    Console.WriteLine("怪物躲开了");
                }
                Console.SetCursorPosition(90, 14);
                Console.WriteLine("                               ");
                Console.SetCursorPosition(90, 15);
                Console.WriteLine("                               ");

            }
        }
        static void GetTreasure(ref Player player,ref bool b)
        {
            Random r = new Random();
            int x = r.Next(0, 6);
            switch (x)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你获得了古代遗物");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("攻击力+2 生命值+15----回车继续");
                        Console.ReadLine();
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("                ");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("                              ");
                        player.ATC += 3;
                        player.Hp += 15;
                        
                    }
                    break;
                case 4:
                    {
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("你获得了传送门");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("上面写着哆啦什么梦，前进6格");
                        Console.ReadLine();
                        Console.SetCursorPosition(90, 14);
                        Console.WriteLine("                ");
                        Console.SetCursorPosition(90, 15);
                        Console.WriteLine("                                        ");
                        b = true;

                    }
                    break;
            }
        }
    }
}
