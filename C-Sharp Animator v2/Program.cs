
using System.Drawing;


string version = "A0.0.1.1";


//Wstępne ustawienie
Console.SetWindowPosition(0, 0); // ustaw pozycję okna  
Console.SetWindowSize(120, 40); // ustaw rozmiar okna   
Console.CursorVisible = false; // ukryj kursor konsoli  

//zmienne
int[,,] pix = new int[62, 31, 9999]; // dane o pikselach
int controlx = 0; // x kontrolera
int controly = 0; // y kontrolera
int highlightcolor = 1; // kolor podświetlenia
int currentcolor = 8; // kolor rysowania
int colormodeback = 0; // kolor tła
int colormodefore = 15; // domyślny kolor tekstu
int frame = 0; // klatka obrazu

Console.WriteLine("Click any key to run program"); 
ConsoleKeyInfo lastinput = Console.ReadKey(true); // ostatnio wciśnięty klawisz


//wywołania
setup(); // początkowe przygotowanie programu

while (true)
{
    
    render(); // narysowanie każdego pixela
    controller(); // sprawdzenie wejścia z klawiatury i wykonanie czynności jeśli klawisz został wciśnięty
    debugtext(lastinput); // tekst debugowania
    
}

void setup()
{
    Console.Clear(); // wyczyszczenie konsoli
    setpix(); // ustawienie wartości 15 dla każdego pixela
    setworkbench(); // ustawienie ramki wokół obszaru rysowania
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
            Console.Write("#");
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
                if (frame < 9999)
                {
                    frame++;
                }
            }
            else if (Int32.Parse(input.KeyChar.ToString()) >= 1 && Int32.Parse(input.KeyChar.ToString()) <= 7) // wybór koloru
            {
                currentcolor = Int32.Parse(input.KeyChar.ToString()) + 7;
            }
           
        }

        catch
        {

        }
        lastinput = input;
    }
}


