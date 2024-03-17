
internal class Program
{
    private static void Main(string[] args)
    {
        /*Juego 21*/
        uint coins = 0;
        uint opt;
        string namePlayer = "Annonymous Player 🔏";
        string? input;

        Console.WriteLine("\t-- Juego 21 --");
        Console.Write($"Type your name, {ColorizeString("(Enter for Anonymous)", "magenta")}: ");
        input = Console.ReadLine();
        namePlayer = string.IsNullOrEmpty(input) ? namePlayer : input;
        Console.Clear();

        /* Console.Write($"¿{ColorizeString(namePlayer, "magenta")}, how many coins do you want to buy?, {ColorizeString("(Enter if you don't want to buy)", "blue")}: ");
        input = Console.ReadLine(); */
        coins = BuyCoins(namePlayer);
        
        Console.Clear();

        do
        {
            string menu = $"Player: {ColorizeString(namePlayer, "magenta")} - Coins: {ColorizeString(coins.ToString() + " 🪙", "yellow")}\n"
            + "What do you want to do?\n"
            + $"1. {ColorizeString("Play 🎰", "green")} \n"
            + $"2. {ColorizeString("Buy more coins 💲🪙", "green")}\n"
            + $"3. {ColorizeString("Prove your looky 🎲", "green")}\n"
            + $"0. {ColorizeString("Exit 📤️", "green")}\n"
            + "> ";
            Console.Write(menu);
            opt = Convert.ToUInt32(Console.ReadLine());
            Console.Clear();

            switch (opt)
            {
                case 0: break;
                case 1:
                    uint stakedCoins;
                    bool win;

                    if (coins <= 0)
                    {
                        Console.WriteLine($"{ColorizeString("At least, you don't have more", "red")} {ColorizeString("Coins 🪙 ", "yellow")}, please {ColorizeString("buy", "green")} more {ColorizeString("Coins 💲🪙", "yellow")}  to play");
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine($"Player: {ColorizeString(namePlayer, "magenta")} - Coins: {ColorizeString(coins.ToString() + " 🪙", "yellow")}\n");
                            Console.Write($"What do you want to {ColorizeString("bet", "green")}?\n> ");
                            stakedCoins = Convert.ToUInt32(Console.ReadLine());
                            Console.Clear();
                            if (stakedCoins > coins)
                            {
                                Console.WriteLine($"{ColorizeString("You don't have enough coins", "red")}\n{ColorizeString("Please try again", "yellow")}");
                            }
                        } while (stakedCoins > coins);

                        
                        win = BlackJack(namePlayer, stakedCoins);

                        if (win)
                        {
                            coins += stakedCoins;
                        }
                        else
                        {
                            coins -= stakedCoins;
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("buy more coins");
                    coins += BuyCoins(namePlayer);
                    break;
                case 3:
                    coins = ProveLucky(coins);
                    break;
                default:
                    Console.WriteLine($"{ColorizeString("Invalid option", "red")}");
                    break;
            }

            if (opt != 0)
            {
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
                Console.Clear();
            }
        } while (opt != 0);
        Console.WriteLine($"{ColorizeString("Thanks for playing 🌟🎊", "green")}");
    }

    static bool BlackJack(string namePlayer, uint stakedCoins)
    {
        uint playerPoints = 0, crupierPoints = 0, optMoreCards, number;
        string card;

        Console.WriteLine("The first two cards of croupier");
        for (int i = 0; i < 2; i++)
        {
            (card, number) = NewCard();
            crupierPoints += number;
            Console.WriteLine($"{card}");
        }
        Console.WriteLine($"Points of {ColorizeString("croupier " + crupierPoints.ToString(), "green")}");
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
        Console.Clear();

        do
        {
            (card, number) = NewCard();
            playerPoints += number;
            Console.WriteLine($"{ColorizeString(namePlayer, "magenta")} {playerPoints}pts - Bet: {ColorizeString(stakedCoins.ToString() + "🪙", "yellow")}  - {ColorizeString("Crupier", "blue")} {crupierPoints}pts");
            Console.WriteLine($"Your card is {card}\nDo you have {playerPoints}");
            Console.Write("Do you want more cards? (1 = Yes, 0 = Stay)\n>");
            optMoreCards = Convert.ToUInt32(Console.ReadLine());
            Console.Clear();
        } while (optMoreCards != 0);

        Console.WriteLine("The last card of croupier");
        (card, number) = NewCard();
        crupierPoints += number;
        Console.WriteLine($"{card}");
        Console.WriteLine($"Points of {ColorizeString("croupier " + crupierPoints.ToString(), "green")}");
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
        Console.Clear();

        if (playerPoints > crupierPoints && playerPoints <= 21)
        {
            Console.WriteLine($"{ColorizeString("You win", "green")} {ColorizeString(stakedCoins.ToString() + " 🪙", "yellow")}");
            return true;
        }
        else
        {
            Console.WriteLine($"{ColorizeString("You lost", "red")} {ColorizeString(stakedCoins.ToString() + " 🪙", "yellow")}");
            return false;
        }
    }

    static uint BuyCoins(string namePlayer){
        string? input;

        Console.Write($"¿{ColorizeString(namePlayer, "magenta")}, how many coins do you want to buy?, {ColorizeString("(Press enter if you don't want to buy)", "blue")}: ");
        input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? 0 : Convert.ToUInt32(input);
    }

    static uint ProveLucky(uint coins){
        Random random = new();
        int type = random.Next(0, 4);

        if(coins>0){
            switch (type)
            {
                case 0: 
                    Console.WriteLine($"{ColorizeString("You don't have lucky 🐈‍⬛","red")}");
                    return coins*0;
                case 1: 
                    Console.WriteLine($"{ColorizeString("You are ok 👍🏽","yellow")}");
                    return coins*1;
                case 2: 
                    Console.WriteLine($"{ColorizeString("Nice shot 🤞🏽","blue")}");
                    return coins*2;
                case 3:
                    Console.WriteLine($"{ColorizeString("You are very lucky 🍀","green")}");
                    return coins*3;
            }
        }
        Console.WriteLine($"{ColorizeString("Shhh 🤫, its a secret 😉","magenta")}");
        return 5;
    }

    static (string, uint) NewCard()
    {
        string[] typeOfCards = { "\u001b[30m♠️", "\u001b[31m♥️", "\u001b[31m♦️", "\u001b[30m♣️" };

        Random random = new();
        int type = random.Next(0, 4);
        uint number = (uint)random.Next(1, 11);

        return ($"{number}{typeOfCards[type]}\u001b[0m", number);
    }

    static string ColorizeString(string text, string color)
    {
        color = color.ToLower();
        var defaultColor = "\u001b[0m";
        string? colorSelected = color switch
        {
            "red" => "\u001b[31m",
            "green" => "\u001b[32m",
            "yellow" => "\u001b[33m",
            "blue" => "\u001b[34m",
            "magenta" => "\u001b[35m",
            "cyan" => "\u001b[36m",
            "white" => "\u001b[37m",
            _ => defaultColor,
        };
        return $"{colorSelected}{text}{defaultColor}";
    }
}