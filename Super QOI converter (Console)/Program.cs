namespace Super_QOI_converter__Console_
{
    internal class Program
    {
        private static bool? _copyFileInfo, _deleteSources, _ignoreColors;
        private static List<string> _paths = new();

        static void Main(string[] args)
        {
            //TODO: use a Resource manager to select the lang, currently only English
            _copyFileInfo = _deleteSources = _ignoreColors = null;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            switch (args.Length)
            {
                case 0: // Will start the program and receive paths and options internally
                    ReceivePaths();
                    break;
                case 1:
                    switch (args[0]) // printing help or error commands
                    {
                        case "-h":
                        case "--help":
                            Console.Write(Messages.Welcome_to);
                            ChangeConsoleColor(ConsoleColor.Cyan);
                            Console.Write($@" {Messages.Program_name}");
                            ChangeConsoleColor(ConsoleColor.White);
                            Console.WriteLine($@" {Messages.Specify_console_version}");
                            ChangeConsoleColor(ConsoleColor.Cyan);
                            Console.WriteLine(Messages.Made_by);
                            ChangeConsoleColor(ConsoleColor.White);
                            Console.WriteLine($@"{Messages.Repo_official_link_message}: https://github.com/LuisAlfredo92/Super-QOI-converter"
                                              + Environment.NewLine);

                            ChangeConsoleColor(ConsoleColor.Cyan);
                            Console.WriteLine(Messages.Not_console_neccesary + Environment.NewLine);

                            ChangeConsoleColor(ConsoleColor.White);
                            Console.WriteLine(Messages.Commands_title + Environment.NewLine);
                            Console.WriteLine(Messages.Copy_attributes_and_dates_option + Environment.NewLine);
                            Console.WriteLine(Messages.Not_copy_attributes_and_dates_option + Environment.NewLine);
                            Console.WriteLine(Messages.Delete_source_option + Environment.NewLine);
                            Console.WriteLine(Messages.Not_delete_source_option + Environment.NewLine);
                            Console.WriteLine(Messages.Help_option + Environment.NewLine);
                            Console.WriteLine(Messages.Ignore_colors_option + Environment.NewLine + Environment.NewLine);

                            Console.WriteLine(Messages.Here_are_some_examples + Environment.NewLine);
                            Console.WriteLine(Messages.Examples);

                            Environment.Exit(0);
                            break;

                        case "-c":
                        case "-d":
                        case "-nc":
                        case "-nd":
                            ReceivePaths();
                            break;
                        case "-i":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Messages.You_must_add_paths + Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($@"{Messages.Type} ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write($@"""{Messages.Program_name}"" -h ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($@"{Messages.More_info_how_to_execute}{Environment.NewLine}");

                            Environment.Exit(0);
                            break;

                        default:
                            if (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Messages.Invalid_option_or_path + Environment.NewLine);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($@"{Messages.Type} ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($@"""{Messages.Program_name}"" -h ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine($@"{Messages.More_info_how_to_execute}{Environment.NewLine}");

                                Environment.Exit(0);
                            }
                            break;
                    }
                    break;

                default: // Will start the program with the paths received
                    // If the user writes contradictory options, the program will close
                    if ((args.Contains("-c") && args.Contains("-nc")) || (args.Contains("-d") && args.Contains("-nd")))
                    {
                        ChangeConsoleColor(ConsoleColor.Red);
                        Console.WriteLine(Messages.Contradictory_options_error + Environment.NewLine);
                        ChangeConsoleColor(ConsoleColor.White);
                        Environment.Exit(0);
                    }
                    
                    // Reading options
                    if(args.Contains("-c"))
                        _copyFileInfo = true;
                    else if(args.Contains("-nc"))
                        _copyFileInfo = false;

                    if(args.Contains("-d"))
                        _deleteSources = true;
                    else if (args.Contains("-nd"))
                        _deleteSources = false;

                    // If the user writes only options without paths, the program will ask for paths
                    if (args.All(element => new List<string> { "-c", "-nc", "-d", "-nd" }.Contains(element)))
                        ReceivePaths();

                    break;
            }
            // TODO: Start process
        }

        private static bool SelectOption(string message, ref bool? configuration)
        {
            var keyPressed = ConsoleKey.Enter;
            byte selectedOption = 1;

            do
            {
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 1)
                            selectedOption--;
                        else
                            selectedOption = 4;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedOption < 4)
                            selectedOption++;
                        else
                            selectedOption = 1;
                        break;

                    case ConsoleKey.NumPad1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.NumPad4:
                        selectedOption = (byte)((int)keyPressed - 96);
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                        selectedOption = (byte)((int)keyPressed - 48);
                        break;
                }

                Console.Clear();
                ChangeConsoleColor(ConsoleColor.Yellow);
                Console.WriteLine(message);
                ChangeConsoleColor(ConsoleColor.White);
                Console.WriteLine(Messages.Use_arrows_or_numbers_and_Enter);

                var options = new[]{Messages.Yes, Messages.Yes_to_all, Messages.No, Messages.No_to_all};
                for (byte i = 1; i <= 4; i++)
                {
                    options[i - 1] = (selectedOption == i ? "> " : "") + $"{i}. " + options[i - 1];
                    ChangeConsoleColor(selectedOption == i ? ConsoleColor.Cyan : ConsoleColor.Gray);
                    Console.WriteLine(options[i - 1]);
                }

                keyPressed = Console.ReadKey().Key;
            } while (keyPressed != ConsoleKey.Enter);

            configuration = selectedOption switch
            {
                2 => true,
                4 => false,
                _ => configuration
            };

            return selectedOption is 1 or 2;
        }

        private static void ChangeConsoleColor(ConsoleColor color)
        {
            if (_ignoreColors != true)
                Console.ForegroundColor = color;
        }

        private static void ReceivePaths()
        {
            string tempPath;

            ChangeConsoleColor(ConsoleColor.Red);
            Console.WriteLine(Messages.You_didn_t_add_any_path);
            ChangeConsoleColor(ConsoleColor.White);
            Console.WriteLine(Messages.Write_your_paths_now);

            do
            {
                tempPath = Console.ReadLine() ?? string.Empty;
                // If the path is invalid or different to Exit string
                bool validPath = File.Exists(tempPath) || Directory.Exists(tempPath),
                    isExitString = string.Equals(tempPath, Messages.Exit, StringComparison.OrdinalIgnoreCase);

                if (!validPath && !isExitString)
                {
                    ChangeConsoleColor(ConsoleColor.Red);
                    Console.WriteLine(Messages.Reading_invalid_path_message);
                    ChangeConsoleColor(ConsoleColor.White);
                }
                else if (_paths.Contains(tempPath))
                {
                    ChangeConsoleColor(ConsoleColor.Red);
                    Console.WriteLine(Messages.Already_entered);
                    ChangeConsoleColor(ConsoleColor.White);
                }
                else
                {
                    // If is different to Exit string it will add it to paths
                    if (!string.Equals(tempPath, Messages.Exit, StringComparison.OrdinalIgnoreCase))
                    {
                        _paths.Add(tempPath);
                        continue;
                    }

                    // If the user types Exit string but didn't add any path
                    if (_paths.Any()) continue;
                    Console.WriteLine(Messages.Add_at_least_one_path);
                    tempPath = string.Empty;
                }
            } while (!string.Equals(tempPath, Messages.Exit, StringComparison.OrdinalIgnoreCase));
        }
    }
}