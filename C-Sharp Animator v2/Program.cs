
using System.Drawing;


string version = "A0.0.2.0";


//Wstępne ustawienie
Console.SetWindowPosition(0, 0); // ustaw pozycję okna  
Console.SetWindowSize(120, 40); // ustaw rozmiar okna   
Console.CursorVisible = false; // ukryj kursor konsoli  

//zmienne
int[,,] pix = new int[62, 31, 2000]; // dane o pikselach
int controlx = 0; // x kontrolera
int controly = 0; // y kontrolera
int highlightcolor = 1; // kolor podświetlenia
int currentcolor = 8; // kolor rysowania
int colormodeback = 0; // kolor tła
int colormodefore = 15; // domyślny kolor tekstu
int frame = 0; // klatka obrazu
string[] menuarrow = new string[8];
string[] menuarrowy = new string[8];
bool inmenu = false;
int menux = 0;
int menuy = 0;
int maxmenuy = 0;
int selectedtool = 1;
bool[] options = new bool[3] { false, true, false};

Console.WriteLine("Click any key to run program"); 
ConsoleKeyInfo lastinput = Console.ReadKey(true); // ostatnio wciśnięty klawisz

string[] programmenuoptions = new string[3] { "▒▒▒▒▒▒▒▒▒▒▒▒", "Exit F2     ", "Reload UI F3"};
string[] animmenuoptions = new string[4] { "▒▒▒▒▒▒", "New   ", "Open  ", "Save  " };
string[] playermenuoptions = new string[3] { "▒▒▒▒▒▒", "Play  ", "W.I.P." };
string[] toolsmenuoptions = new string[2] { "▒▒▒▒▒▒▒▒▒▒", "Pencil  " };
string[] settingmenuoptions = new string[3] { "▒▒▒▒▒▒▒▒▒▒▒▒║▒"/*, "Dark mode  "*/, "Debug text  ", "W.I.P.      " };
string[] aboutmenuoptions = new string[4] { "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒", "C-Sharp Animator v2", "By Drenewoo        ", "Version: " + version + "  ", };

//wywołania
setup(true); // początkowe przygotowanie programu

while (true)
{
    if (!inmenu)
    {
        render(); // narysowanie każdego pixela
        debugtext(lastinput); // tekst debugowania
    }
    
    controller(); // sprawdzenie wejścia z klawiatury i wykonanie czynności jeśli klawisz został wciśnięty
    
    drawmenubar();
    
}

void setup(bool pix)
{
    Console.BackgroundColor = (ConsoleColor)colormodeback;
    Console.ForegroundColor = (ConsoleColor)colormodefore;
    Console.Clear(); // wyczyszczenie konsoli
    if (pix)
    {
        setpix(); // ustawienie wartości 15 dla każdego pixela
    }
    setworkbench(); // ustawienie ramki wokół obszaru rysowania
    for(int i = 0; i < menuarrow.Length; i++)
    {
        menuarrow[i] = "  ";
    }
    menuarrow[0] = "->";
}

void setworkbench()
{
    for (int x = 0; x != 64; x++)
    {
        for (int y = 0; y != 33; y++)
        {
            Console.SetCursorPosition(x + 2, y + 4);
            Console.BackgroundColor = (ConsoleColor)colormodeback;
            Console.ForegroundColor = (ConsoleColor)colormodefore;
            Console.Write("▒");
        }
    }
    
    

}



void drawmenubar()
{
    Console.SetCursorPosition(2, 2);
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    if (menuy == 0)
    {
        for (int i = 0; i < menuarrow.Length; i++)
        {
            menuarrow[i] = "  ";
        }
        menuarrow[menux] = " »";
        for (int j = 0; j < maxmenuy + 1; j++)
        {
            menuarrowy[j] = "  ";
        }
    }
    else
    {
        for (int i = 0; i < menuarrow.Length; i++)
        {
            menuarrow[i] = "  ";
        }
        menuarrow[menux] = " /";
        for (int j = 0; j < maxmenuy + 1; j++) 
        {
            menuarrowy[j] = "  ";
        }
        menuarrowy[menuy] = " »";


    }
    Console.Write(menuarrow[1] + " Program  " + menuarrow[2] + " Animation  " + menuarrow[3] + " Player  " + menuarrow[4] + " Tools  " + menuarrow[5] + " Settings  " + menuarrow[6] + " About CSAv2 " + "                                           ");
    if(menux == 1)
    {
        maxmenuy = programmenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < programmenuoptions.Length; j++)
        {
            Console.SetCursorPosition(2, j+3);
            Console.Write(menuarrowy[j] + programmenuoptions[j]);
        }
    }
    if (menux == 2)
    {
        maxmenuy = animmenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < animmenuoptions.Length; j++)
        {
            Console.SetCursorPosition(14, j + 3);
            Console.Write(menuarrowy[j] + animmenuoptions[j]);
        }
    }
    if (menux == 3)
    {
        maxmenuy = playermenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < playermenuoptions.Length; j++)
        {
            Console.SetCursorPosition(28, j + 3);
            Console.Write(menuarrowy[j] + playermenuoptions[j]);
        }
    }
    if (menux == 4)
    {
        maxmenuy = toolsmenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < toolsmenuoptions.Length; j++)
        {
            Console.SetCursorPosition(39, j + 3);
            Console.Write(menuarrowy[j] + toolsmenuoptions[j]);
            if (selectedtool == j) { Console.Write("<-"); }
            else { if (j != 0) { Console.Write("  "); } }
        }
    }
    if (menux == 5)
    {
        maxmenuy = settingmenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < settingmenuoptions.Length; j++)
        {
            Console.SetCursorPosition(49, j + 3);
            Console.Write(menuarrowy[j] + settingmenuoptions[j]);
            if (options[j] == true) { Console.Write("~ "); }
            else { if (j != 0) { Console.Write("  "); } }
        }
    }
    if (menux == 6) 
    {
        maxmenuy = aboutmenuoptions.Length - 1;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int j = 0; j < aboutmenuoptions.Length; j++)
        {
            Console.SetCursorPosition(64, j + 3);
            Console.Write(aboutmenuoptions[j]);
            
        }
    }

}

void debugtext(ConsoleKeyInfo Key) // tekst debugowania
{
    Console.SetCursorPosition(0, 0);
    
    Console.BackgroundColor = (ConsoleColor)colormodeback;
    Console.ForegroundColor = (ConsoleColor)colormodefore;
    Console.Write("                                                                                                                   ");
    Console.SetCursorPosition(0, 0);
    Console.Write("Version: " + version + " Debug text ( temporary ) : ");
    Console.ForegroundColor = (ConsoleColor)highlightcolor;
    Console.Write("Last input: " + Key.Key.ToString() + "             " + " Current color: ");
    Console.BackgroundColor = (ConsoleColor)currentcolor;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write(Int32.Parse(currentcolor.ToString()) - 7);
    Console.BackgroundColor = (ConsoleColor)colormodeback;
    Console.ForegroundColor = (ConsoleColor)highlightcolor;
    Console.Write(" Frame: " + frame);
}

void setpix() // ustawienie pikseli do wartości domyślnej
{
    for (int x = 0; x != 62; x++)
    {
        for (int y = 0; y != 31; y++)
        {
            pix[x, y, frame] = colormodeback;
        }
    }
}

void menu()
{
    if (menux == 1)
    {
        if (menuy == 1)
        {
            System.Environment.Exit(0);
        }
        else if(menuy == 2)
        {
            Console.Clear();
            Console.WriteLine("Press any key to load UI");
            Console.ReadLine();
            setup(false);
            inmenu = false;
            menux = 0;
            menuy = 0;
            maxmenuy = 0;
            selectedtool = 1;
        }
    }
    else if (menux == 2)
    {
       if (menuy == 1) 
       {
            setup(true);
            inmenu = false;
            menux = 0;
            menuy = 0;
            maxmenuy = 0;
            selectedtool = 1;
       }
       else if(menuy == 2)
        {
            Console.SetCursorPosition(6, 10);
            Console.WriteLine("Type in the name of the file: ( file needs to be in program directory )");

            string name = Console.ReadLine() + ".csa";

            Console.WriteLine("Wait 5 seconds and click escape");
            if (File.Exists(name))
            {
                TextReader tr = new StreamReader(name);
                for (int f = 0; f < 1999; f++)
                {
                    for (int y = 0; y < 31; y++)
                    {
                        for (int x = 0; x < 62; x++)
                        {
                            
                           pix[x,y,f] = Int32.Parse(tr.ReadLine());

                        }
                    }
                }
               
            }
           
        






    }
       else if(menuy == 3)
        {
            Console.SetCursorPosition(6, 10);
            Console.WriteLine("Type in the name of the file: ( file will be saved in program directory)");
           
                string name = Console.ReadLine() + ".csa";

            if(File.Exists(name))
            {
                File.Delete(name);
                File.Create(name).Close();
            }
            else
            {
                File.Create(name).Close();
            }

            TextWriter tw = new StreamWriter(name);
            
            
            for(int f = 0; f < 2000; f++)
            {
                for(int y = 0; y < 31; y++)
                {
                    for (int x = 0; x < 62; x++)
                    {
                       
                        tw.WriteLine(pix[x,y,f]);
                      
                    }
                }
            }
            
            setup(false);
            inmenu = false;
            menux = 0;
            menuy = 0;
            maxmenuy = 0;
        }
    }
}

void render() // renderowanie obaszaru rysowania
{
    for(int x = 0; x != 62; x++)
    {
        for(int y = 0; y != 31; y++)
        {
            Console.SetCursorPosition(x + 3, y + 5); // ustaw pozycję kursora
            Console.BackgroundColor = (ConsoleColor)pix[x, y, frame]; // ustaw kolor tekstu na wartość pixela
            if(controlx == x && controly == y)                          // \
            {                                                           //  \
                Console.ForegroundColor = (ConsoleColor)highlightcolor; //   |
                if(highlightcolor < 7)                                  //   |
                {                                                       //   |
                    highlightcolor++;                                   //   }  renderowanie customowego kursora
                }                                                       //   |
                else                                                    //   |
                {                                                       //   |
                    highlightcolor = 1;                                 //   |
                }                                                       //   /
            }                                                           //  /
            else
            {
                Console.ForegroundColor = (ConsoleColor)pix[x, y, frame];
            }
            Console.Write("╬");
        }
    }
}

void controller() // sterowanie
{
    if(Console.KeyAvailable == true)
    {
       
        ConsoleKeyInfo input;
        input = Console.ReadKey(true);
        try
        {
            if (inmenu == false)
            {
                if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) // Kursor w górę
                {
                    controly--;
                    if (controly < 0) { controly++; }

                }
                else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) // Kursor w dół
                {
                    controly++;
                    if (controly > 30) { controly--; }

                }
                else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow) // Kursor w lewo
                {
                    controlx--;
                    if (controlx < 0) { controlx++; }

                }
                else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow) // Kursor w prawo
                {
                    controlx++;
                    if (controlx > 61) { controlx--; }

                }
                else if (input.Key == ConsoleKey.E) // rysuj
                {
                    pix[controlx, controly, frame] = currentcolor;
                }
                else if (input.Key == ConsoleKey.F) // wymaż
                {
                    pix[controlx, controly, frame] = colormodeback;
                }
                else if (input.Key == ConsoleKey.C) // klatka-
                {
                    if (frame > 0)
                    {
                        frame--;
                    }
                }
                else if (input.Key == ConsoleKey.V) // klatka+
                {
                    if (frame < 1999)
                    {
                        frame++;
                    }
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    inmenu = true;
                    menux = 1;
                }
                else if (Int32.Parse(input.KeyChar.ToString()) >= 1 && Int32.Parse(input.KeyChar.ToString()) <= 7) // wybór koloru
                {
                    currentcolor = Int32.Parse(input.KeyChar.ToString()) + 7;
                }
            }
            else
            {
                if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow) // Kursor w górę
                {
                    if (menuy > 0)
                    {
                        menuy--;
                    }

                }
                else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow) // Kursor w dół
                {
                    if (menuy < maxmenuy && menux != 6)
                    {
                        menuy++;
                    }

                }
                else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow) // Kursor w lewo
                {
                    if (menux > 1 && menuy == 0)
                    {
                        menux--;
                        setup(false);
                        render();
                        debugtext(lastinput);
                    }
                }
                else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow) // Kursor w prawo
                {
                    if (menux < 6 && menuy == 0)
                    {
                        menux++;
                        setup(false);
                        render();
                        debugtext(lastinput);
                    }
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    inmenu = false;
                    menux = 0;
                    setup(false);
                }
                else if (input.Key == ConsoleKey.E)
                {
                    menu();
                }
            }    
           
        }

        catch
        {

        }
        lastinput = input;
    }
}


