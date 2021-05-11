using System;
using System.Collections.Generic;

namespace Tic_Tac_Toe_console
{
    class MainMenu
    {
        public static int FIRST_PLAYER = 0;
        public static int SECOND_PLAYER = 1;
        public enum MenuActionList
        {
            Setup_game_for_players_vs_players,
            Setup_game_against_AI,
            Exit_game
        }
        public static Dictionary<string, string> COMMON_MESSAGES = generateCommonMessages();
        public static Dictionary<MenuActionList, Action> MENU_ACTION_LIST = generateMenuActionList();
        public void runMenu()
        {
            try
            {
                programMainLoop();
            } catch
            {
                Console.WriteLine(COMMON_MESSAGES["ERR000_UNKNOWN"]);
            }
            exitGame();
        }

        public static bool programMainLoop()
        {
            int userChoiceForMenuAction;
            do
            {
                userChoiceForMenuAction = menuChoice();
                dealWithMenuChoice(userChoiceForMenuAction);
            } while (userChoiceForMenuAction != (int)MenuActionList.Exit_game);
            return false;
        }

        private static void dealWithMenuChoice(int choice)
        {
            MenuActionList chosenActionByUser = (MenuActionList)choice;
            if(!choiceInMenu(choice))
            {
                Console.WriteLine(COMMON_MESSAGES["ERR001_INVALID_ENTRY"]); 
            } else
            {
                MENU_ACTION_LIST[chosenActionByUser]();
            }
        }

        private static bool choiceInMenu(int choice)
        {
            if (choice > Enum.GetValues(typeof(MenuActionList)).Length)
            {
                return false; 
            }
            if(choice < 0)
            {
                return false; 
            }
            return true; 
        }

        private static Dictionary<MenuActionList, Action> generateMenuActionList()
        {
            var actionList = new Dictionary<MenuActionList, Action>()
            {
                {MenuActionList.Setup_game_for_players_vs_players, () => setUpGamePVP() },
                {MenuActionList.Setup_game_against_AI,             () => setUpGameVsAi() },
                {MenuActionList.Exit_game,                         () => exitGame() }
            };
            return actionList;
        }
        private static Dictionary<string, string> generateCommonMessages()
        {
            Dictionary<string, string> commonMessages = new Dictionary<string, string>();
            commonMessages.Add("ERR000_UNKNOWN", "Unknown error, please restart the game."                         );
            commonMessages.Add("ERR001_INVALID_ENTRY", "The entry is invalid. Try again with the right parameters.");
            commonMessages.Add("ERR002_NOT_IMPLEMENTED", "This feature is not implemented yet."                    );
            commonMessages.Add("VARIA_PRESS_ENTER", "(Please press ENTER to continue)"                             );
            commonMessages.Add("VARIA_WELCOME", "***************************" +
                                              "\n* Welcome to Tic Tac Toe! *" +
                                              "\n***************************"                                      );
            commonMessages.Add("VARIA_THANK_YOU", "**************************" +
                                                "\n* Thank you for playing! *" +
                                                "\n**************************"                                     );
            commonMessages.Add("VARIA_CHOOSE", "Choose an option from the menu."                                   );
            commonMessages.Add("GAME_WIN_PT1", $"\n\n\n\t Congratulations "                                        );
            commonMessages.Add("GAME_WIN_PT2", "! You have won!"                                                   );
            commonMessages.Add("GAME_CELL_PROMPT", ", please enter a number to choose a cell to mark."             );
            commonMessages.Add("GAME_CELL_NOT_AVAILABLE", "This cell is not available, try again."                 );
            commonMessages.Add("VARIA_ENTER_PLAYER_NAME", "Enter the name (less than 15 characters) of player "    );
            commonMessages.Add("GAME_DRAW", "The game ends in a draw! Play again!"                                 );
            commonMessages.Add("GAME_AI_PLAY", "The AI is making its move."                                        );

            return commonMessages;
        }

        private static void setUpGameVsAi()
        {
            string p1name = registerPlayer(FIRST_PLAYER);
            runPVEGame(p1name); 
        }

        private static void runPVEGame(string p1name)
        {
            GameManager gameManager = new GameManager();
            gameManager.runAIGame(p1name);
            Console.WriteLine(COMMON_MESSAGES["VARIA_PRESS_ENTER"]);
            Console.ReadLine();
            Console.Clear();
        }

        private static void setUpGamePVP()
        {
            string p1name = registerPlayer(FIRST_PLAYER);
            string p2name = registerPlayer(SECOND_PLAYER);
            runPVPGame(p1name, p2name);
        }

        private static string registerPlayer(int playerNumber)
        {
            var pName = "";
            bool nameLegality = false;
            do
            {
                Console.WriteLine(COMMON_MESSAGES["VARIA_ENTER_PLAYER_NAME"] + playerNumber + ":");
                pName = Console.ReadLine();
                nameLegality = checkNameLegality(pName);
            } while (nameLegality == false);
            return pName.ToString();
        }

        private static bool checkNameLegality(string pName)
        {
            return checkNameLength(pName);
        }

        private static bool checkNameLength(string pName)
        {
            return (pName.Length < 15 && pName.Length > 0);
        }

        private static void runPVPGame(string p1name, string p2name)
        {
            GameManager gameManager = new GameManager();
            gameManager.runGame(p1name, p2name); 
            Console.WriteLine(COMMON_MESSAGES["VARIA_PRESS_ENTER"]);
            Console.ReadLine();
            Console.Clear();
        }

        private static int menuChoice()
        {
            showMenu();
            int choice = getMenuChoiceFromUser();
            return choice;
        }

        public static void exitGame()
        {
            Console.Clear();
            Console.WriteLine(COMMON_MESSAGES["VARIA_THANK_YOU"]);
            Console.WriteLine(COMMON_MESSAGES["VARIA_PRESS_ENTER"]);
            Console.ReadLine();
            System.Environment.Exit(1);
        }

        private static void showMenu()
        {
            Console.WriteLine(COMMON_MESSAGES["VARIA_WELCOME"]);
            string menuAsString = menuStringBuilder();
            Console.WriteLine(menuAsString);
        }

        private static string menuStringBuilder()
        {
            string menu = "";

            foreach(MenuActionList actionElement in (MenuActionList[])Enum.GetValues(typeof(MenuActionList)))
            {
                menu += menuActionListToStringForMenu(actionElement);
            }
            return menu; 
        }

        private static string menuActionListToStringForMenu(MenuActionList actionElement)
        {
            string enumToString = "";

            enumToString += (int)actionElement;
            enumToString += "- ";
            enumToString += formatMenuActionListToString(actionElement);
            enumToString += "\n";

            return enumToString; 
        }

        private static string formatMenuActionListToString(MenuActionList actionElement)
        {
            string formattedEnumToString = "";

            string temp = actionElement.ToString();
            formattedEnumToString = temp.Replace("_", " ");

            return formattedEnumToString;
        }

        private static int getMenuChoiceFromUser()
        {
            int numericalChoice = -1;
            bool validChoiceFromUser = false; 
            while (!validChoiceFromUser)
            {
                Console.WriteLine(COMMON_MESSAGES["VARIA_CHOOSE"]);
                var choiceStringInput = Console.ReadLine();
                numericalChoice = convertInputToInt(choiceStringInput);
                validChoiceFromUser = verifyInputMenuChoice(numericalChoice);
            } 
            return numericalChoice; 
        }

        private static bool verifyInputMenuChoice(int numericalChoice)
        {
            return true; 
        }

        private static int convertInputToInt(string choiceStringInput)
        {
            int choiceInputAsInt = 0;
            choiceStringInput = choiceStringInput.Trim();
            char choiceCharInput = '0';
            try
            {
                choiceCharInput = choiceStringInput[0];
            } catch
            {
                Console.WriteLine(COMMON_MESSAGES["ERR001_INVALID_ENTRY"]);
            }
            try
            {
                choiceInputAsInt = Convert.ToInt32(new string(choiceCharInput, 1));
            } catch
            {
                Console.WriteLine(COMMON_MESSAGES["ERR001_INVALID_ENTRY"]);
            }
            return choiceInputAsInt; 
        }
    }
}
