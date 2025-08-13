// (c) copyright UnPlaguer 2025
using System;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

class Program
{
    static Random random = new Random();

    [STAThread]
    static void Main(string[] args)
    {
        while (true)
        {
            ShowMainMenu();
            string input = Console.ReadLine().ToLower();

            if (input == "start")
            {
                StartGenerator();
            }
            else if (input == "exit")
            {
                Environment.Exit(0);
            }
            else if (input == "back")
            {
                // This command doesn't do anything here, as this is the main menu.
            }
            else
            {
                // Clear the invalid input from the console
                ClearLastInputLine();
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.Clear();
        Console.SetCursorPosition(50, 0);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("(c) copyright UnPlaguer 2025");
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
██████╗░░░██╗██╗░░░░░██╗░░░██╗██╗░░░██████╗░
██╔══██╗░██╔╝██║░░░░░██║░░░██║╚██╗░██╔════╝░
██████╔╝██╔╝░██║░░░░░██║░░░██║░╚██╗██║░░██╗░
██╔═══╝░╚██╗░██║░░░░░██║░░░██║░██╔╝██║░░╚██╗
██║░░░░░░╚██╗███████╗╚██████╔╝██╔╝░╚██████╔╝
╚═╝░░░░░░░╚═╝╚══════╝░╚═════╝░╚═╝░░░╚═════╝░
        ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Hi! This is Password (lu) generator, type start to start, type exit to exit and type back to back.");
        Console.WriteLine();
    }

    static void StartGenerator()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a password type (1-7):");
            Console.WriteLine("1. with only symbols");
            Console.WriteLine("2. with only letters");
            Console.WriteLine("3. with only numbers");
            Console.WriteLine("4. numbers and letters");
            Console.WriteLine("5. numbers and symbols");
            Console.WriteLine("6. letters and symbols");
            Console.WriteLine("7. numbers, letters and symbols");
            Console.WriteLine();
            Console.WriteLine("Type 'back' to go to the main menu.");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            if (choice == "back")
            {
                return;
            }

            if (int.TryParse(choice, out int passwordType) && passwordType >= 1 && passwordType <= 7)
            {
                // Only ask about capital letters if the password type includes letters
                if (passwordType == 2 || passwordType == 4 || passwordType == 6 || passwordType == 7)
                {
                    AskForPasswordLength(passwordType);
                }
                else
                {
                    // If no letters are included, skip the question
                    GenerateAndShowPassword(passwordType, 0, false, false);
                }
                return;
            }
            else
            {
                ClearLastInputLine();
            }
        }
    }

    static void AskForPasswordLength(int passwordType)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("How many characters will your password be?");
            Console.WriteLine("Type 'back' to go to the previous menu.");
            Console.WriteLine();

            string lengthInput = Console.ReadLine().ToLower();

            if (lengthInput == "back")
            {
                StartGenerator();
                return;
            }

            if (int.TryParse(lengthInput, out int passwordLength) && passwordLength > 0)
            {
                // Only ask about capital letters if the password type includes letters
                if (passwordType == 2 || passwordType == 4 || passwordType == 6 || passwordType == 7)
                {
                    AskForCapitalLetters(passwordType, passwordLength);
                }
                else
                {
                    // If no letters are included, skip the question
                    GenerateAndShowPassword(passwordType, passwordLength, false, false);
                }
                return;
            }
            else
            {
                ClearLastInputLine();
            }
        }
    }

    static void AskForCapitalLetters(int passwordType, int passwordLength)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Will you have capital letters? (y/n)");
            Console.WriteLine("Type 'back' to go to the previous menu.");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            if (choice == "back")
            {
                AskForPasswordLength(passwordType);
                return;
            }

            if (choice == "y")
            {
                AskIfAllCapitalLetters(passwordType, passwordLength);
                return;
            }
            else if (choice == "n")
            {
                GenerateAndShowPassword(passwordType, passwordLength, false, false);
                return;
            }
            else
            {
                ClearLastInputLine();
            }
        }
    }

    static void AskIfAllCapitalLetters(int passwordType, int passwordLength)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Will all letters be capital? (y/n)");
            Console.WriteLine("Type 'back' to go to the previous menu.");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            if (choice == "back")
            {
                AskForCapitalLetters(passwordType, passwordLength);
                return;
            }

            if (choice == "y")
            {
                GenerateAndShowPassword(passwordType, passwordLength, true, true);
                return;
            }
            else if (choice == "n")
            {
                GenerateAndShowPassword(passwordType, passwordLength, true, false);
                return;
            }
            else
            {
                ClearLastInputLine();
            }
        }
    }

    static void GenerateAndShowPassword(int passwordType, int passwordLength, bool includeCapitalLetters, bool allLettersCapital)
    {
        string characterSet = "";
        string symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "0123456789";

        switch (passwordType)
        {
            case 1:
                characterSet = symbols;
                break;
            case 2:
                characterSet = lowercaseLetters;
                if (includeCapitalLetters && !allLettersCapital)
                {
                    characterSet += uppercaseLetters;
                }
                else if (includeCapitalLetters && allLettersCapital)
                {
                    characterSet = uppercaseLetters;
                }
                break;
            case 3:
                characterSet = numbers;
                break;
            case 4:
                characterSet = numbers + lowercaseLetters;
                if (includeCapitalLetters && !allLettersCapital)
                {
                    characterSet += uppercaseLetters;
                }
                else if (includeCapitalLetters && allLettersCapital)
                {
                    characterSet = numbers + uppercaseLetters;
                }
                break;
            case 5:
                characterSet = numbers + symbols;
                break;
            case 6:
                characterSet = lowercaseLetters + symbols;
                if (includeCapitalLetters && !allLettersCapital)
                {
                    characterSet += uppercaseLetters;
                }
                else if (includeCapitalLetters && allLettersCapital)
                {
                    characterSet = uppercaseLetters + symbols;
                }
                break;
            case 7:
                characterSet = numbers + lowercaseLetters + symbols;
                if (includeCapitalLetters && !allLettersCapital)
                {
                    characterSet += uppercaseLetters;
                }
                else if (includeCapitalLetters && allLettersCapital)
                {
                    characterSet = numbers + uppercaseLetters + symbols;
                }
                break;
        }

        if (string.IsNullOrEmpty(characterSet))
        {
            Console.WriteLine("Error: Character set is empty. Press any key to try again.");
            Console.ReadKey();
            return;
        }

        StringBuilder passwordBuilder = new StringBuilder();
        for (int i = 0; i < passwordLength; i++)
        {
            int index = random.Next(characterSet.Length);
            passwordBuilder.Append(characterSet[index]);
        }

        string generatedPassword = passwordBuilder.ToString();

        Console.Clear();
        Console.WriteLine("Generated Password:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(generatedPassword);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  'copy' - Copy the password to your clipboard.");
        Console.WriteLine("  'generate again' - Create another password with the same settings.");
        Console.WriteLine("  'back' - Return to the main menu.");
        Console.WriteLine();

        while (true)
        {
            string action = Console.ReadLine().ToLower();
            if (action == "copy")
            {
                try
                {
                    // Use a separate thread to handle clipboard operations
                    Thread thread = new Thread(() => Clipboard.SetText(generatedPassword));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    Console.WriteLine("Password copied to clipboard successfully!");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Unable to copy to clipboard. Please copy manually. Details: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else if (action == "generate again")
            {
                GenerateAndShowPassword(passwordType, passwordLength, includeCapitalLetters, allLettersCapital);
                return;
            }
            else if (action == "back")
            {
                return;
            }
            else
            {
                ClearLastInputLine();
            }
        }
    }

    static void ClearLastInputLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, currentLineCursor);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
}