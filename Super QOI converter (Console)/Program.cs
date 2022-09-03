namespace Super_QOI_converter__Console_
{
    internal class Program
    {
        static bool? copyFileInfo, deleteSources, ignoreColors;

        static void Main(string[] args)
        {
            //TODO: use a Resource manager to select the lang, currently only English
            copyFileInfo = deleteSources = ignoreColors = null;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            switch (args.Length)
            {
                case 1:
                    switch (args[0])
                    {
                        case "-h":
                        case "--help":
                            Console.Write(Messages.Welcome_to);
                            ChangeConsoleColor(ConsoleColor.Cyan);
                            Console.Write($@" {Messages.Program_name}");
                            ChangeConsoleColor(ConsoleColor.White);
                            Console.WriteLine($@" {Messages.Specify_console_version}{Environment.NewLine}");

                            ChangeConsoleColor(ConsoleColor.Cyan);
                            Console.WriteLine(Messages.Not_console_neccesary + Environment.NewLine);

                            ChangeConsoleColor(ConsoleColor.White);
                            Console.WriteLine(Messages.Commands_title + Environment.NewLine);
                            Console.WriteLine(Messages.Copy_attributes_and_dates_option + Environment.NewLine);
                            Console.WriteLine(Messages.Delete_source_option + Environment.NewLine);
                            Console.WriteLine(Messages.Help_option + Environment.NewLine);
                            Console.WriteLine(Messages.Ignore_colors_option + Environment.NewLine);
                            break;

                        case "-c":
                        case "-d":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Messages.You_must_add_paths + Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($@"{Messages.Type} ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write($@"""{Messages.Program_name}"" -h ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($@"{Messages.More_info_how_to_execute}{Environment.NewLine}");
                            break;

                        default:
                            if (Uri.IsWellFormedUriString(args[0], UriKind.RelativeOrAbsolute))
                            {
                                //TODO: Convert
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Messages.Invalid_option_ot_path);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($@"{Messages.Type} ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($@"""{Messages.Program_name}"" -h ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine($@"{Messages.More_info_how_to_execute}{Environment.NewLine}");
                            }
                            break;
                    }
                    break;
            }
//            SelectBooleanOption(Messages.Test.ToString());
        }

        private static bool SelectBooleanOption(string message)
        {
            var keyPressed = ConsoleKey.Enter;
            var option = true;

            do
            {
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        option = !option;
                    break;

                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        option = true;
                    break;

                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        option = false;
                    break;
                }

                Console.Clear();
                ChangeConsoleColor(ConsoleColor.Yellow);
                Console.WriteLine(message);

                string op1 = (option ? "> " : "") + "1. Yes",
                    op2 = (!option ? "> " : "") + "2. No";

                ChangeConsoleColor(option? ConsoleColor.Cyan : ConsoleColor.Gray);
                Console.WriteLine(op1);
                ChangeConsoleColor(!option ? ConsoleColor.Cyan : ConsoleColor.Gray);
                Console.WriteLine(op2);

                keyPressed = Console.ReadKey().Key;
            } while (keyPressed != ConsoleKey.Enter);

            return option;
        }

        private static void ChangeConsoleColor(ConsoleColor color)
        {
            if (ignoreColors != true)
                Console.ForegroundColor = color;
        }
    }
}