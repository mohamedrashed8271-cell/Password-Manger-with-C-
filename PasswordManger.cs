using System.Text;

namespace ConsoleApp.TrainingApps.PasswordApp;

internal static class PasswordManger
{
    private const string Path = @"E:\Coding\C#\Project\Solution1\ConsoleApp\TrainingApps\PasswordApp\passwords.txt";
    private static readonly Dictionary<string, string> _Password = new();

    private static void Menu()
    {
        Console.WriteLine("[ Password Manger ]");
        Console.WriteLine("== Select an Option ==");
        Console.WriteLine("1. List All Passwords.");
        Console.WriteLine("2. Add || Change Password.");
        Console.WriteLine("3. Get Password.");
        Console.WriteLine("4. Delete Password.");
        Console.WriteLine("5. Exit.");
    }

    internal static void Run()
    {
        ReadPasswords();
        while (true)
        {
            Menu();
            int user = int.Parse(Console.ReadLine());
            if (user == 5)
                break;
            switch (user)
            {
                case 1:
                    ListAllPasswords();
                    break;
                case 2:
                    AddOrChangePassword();
                    break;
                case 3:
                    GetPassword();
                    break;
                case 4:
                    DeletePassword();
                    break;
                default:
                    Console.WriteLine("Inviald");
                    break;
            }
            Console.WriteLine("---------------------------------------");
        }
    }
    private static void ListAllPasswords()
    {
        foreach (var i in _Password)
            Console.WriteLine($"- {i.Key} = {i.Value}");
    }
    private static void AddOrChangePassword()
    {
        Console.WriteLine("Write Like This:\nWebsite=Password");
        string user = Console.ReadLine();
        string[] arr = new string[2];
        arr = user.Split("=");
        if (_Password.ContainsKey(arr[0]))
            _Password[arr[0]] = arr[1];
        else
            _Password.Add(arr[0], arr[1]);
        SavePasswords();
    } 
    private static void GetPassword()
    {
        Console.Write("Enter Website/App Name : ");
        var user = Console.ReadLine();
            if (_Password.ContainsKey(user))
            Console.WriteLine($"Password: {_Password[user]}"); 
            else
            Console.WriteLine("Not Found!");
    }
    private static void DeletePassword()
    {
        Console.Write("Enter Website/App Name : ");
        var user = Console.ReadLine();
        if (_Password.ContainsKey(user))
        {
            _Password.Remove(user);
            SavePasswords();
        }
        else
            Console.WriteLine("Not Found!");
    }

    private static void ReadPasswords()
    {
        if (File.Exists(Path))
        {
            string passwordLine = File.ReadAllText(Path);
            foreach (var line in passwordLine.Split(Environment.NewLine))
            {
                if(!string.IsNullOrEmpty(line))
                {
                    var Index = line.IndexOf('=');
                    var appName = line.Substring(0, Index);
                    var pass = line.Substring(Index + 1);
                    _Password.Add(appName, pass);
                }
        }
        }
    }
    private static void SavePasswords() 
    {
        var sb = new StringBuilder();
        foreach (var entry in _Password)
            sb.AppendLine($"{entry.Key}={entry.Value}");
        File.WriteAllText(Path,sb.ToString());
    }
}

