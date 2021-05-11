using System;
using System.Collections.Generic;
using System.Threading;

namespace Tic_Tac_Toe_console
{
    class GameManager
    {
        public void runGame(string p1name, string p2name)
        {
            Console.WriteLine(aGame(p1name, p2name));
        }
        public void runAIGame(string p1name)
        {
            Console.WriteLine(anAIGame(p1name));
        }
        public static string aGame(string p1name, string p2name)
        {
            Game game = new Game(p1name, p2name);
            return aGameLoop(game);
        }
        public static string anAIGame(string p1name)
        {
            Game game = new Game(p1name, "AI");
            return anAIGameLoop(game);
        }
        private static string aGameLoop(Game currentGame)
        {
            do
            {
                Console.WriteLine(currentGame.showGame());
                if (playerMakeAMove(currentGame))
                {
                    return victoryTitleStringBuilder(currentGame);
                }
                currentGame.switchActivePlayer();
            } while (!currentGame.getGridGame().checkIfGameIsADraw());
            return MainMenu.COMMON_MESSAGES["GAME_DRAW"];
        }
        private static string anAIGameLoop(Game currentGame)
        {
            while(!currentGame.getGridGame().checkIfGameIsADraw())
            {
                Console.WriteLine(currentGame.showGame());
                if (playerMakeAMove(currentGame))
                {
                    return victoryTitleStringBuilder(currentGame);
                }
                Console.WriteLine(currentGame.showGame());
                currentGame.switchActivePlayer(); 
                if (AIMakeAMove(currentGame))
                {
                    return AIWin(currentGame);
                }
                currentGame.switchActivePlayer();
            } 
            return MainMenu.COMMON_MESSAGES["GAME_DRAW"];
        }

        private static bool AIMakeAMove(Game currentGame)
        {
            AIMoveMessaging();
            AIPlay(currentGame); 
            return gameVictoryStatus(currentGame);
        }

        private static void AIPlay(Game currentGame)
        {
            List<Cell> availableCells = selectAllAvailableCellsForMarking(currentGame);
            Cell cellToBeMarked = selectARandomCellFromList(availableCells);
            writeToCell(cellToBeMarked.Position, currentGame);
        }

        private static Cell selectARandomCellFromList(List<Cell> availableCells)
        {
            var random = new Random();
            int index = random.Next(availableCells.Count);
            return availableCells[index];
        }

        private static List<Cell> selectAllAvailableCellsForMarking(Game currentGame)
        {
            return currentGame.getGridGame().getAvailableCells();
        }

        private static void AIMoveMessaging()
        {
            Console.WriteLine(MainMenu.COMMON_MESSAGES["GAME_AI_PLAY"]);
            Console.Write(".");
            Thread.Sleep(750);
            Console.Write(".");
            Thread.Sleep(750);
            Console.Write(".");
            Thread.Sleep(750);
            Console.Write(".");
            Thread.Sleep(750);
            Console.Write(".");
        }

        private static bool gameVictoryStatus(Game currentGame)
        {
            if (currentGame.getGridGame().checkVictory())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string AIWin(Game currentGame)
        {
            string victoryTitle = "";
            victoryTitle += currentGame.showGame();
            victoryTitle += "\n\n";
            victoryTitle += MainMenu.COMMON_MESSAGES["GAME_AI_WIN"];
            return victoryTitle;
        }

        private static string victoryTitleStringBuilder(Game currentGame)
        {
            string victoryTitle = "";
            victoryTitle += currentGame.showGame();
            victoryTitle += MainMenu.COMMON_MESSAGES["GAME_WIN_PT1"];
            victoryTitle += currentGame.getActivePlayer().getName();
            victoryTitle += MainMenu.COMMON_MESSAGES["GAME_WIN_PT2"];
            return victoryTitle;
        }

        private static bool playerMakeAMove(Game currentGame)
        {
            Console.WriteLine($"\n\n\t {currentGame.getActivePlayer().getName()} " + MainMenu.COMMON_MESSAGES["GAME_CELL_PROMPT"]);
            markTheCell(currentGame);
            return gameVictoryStatus(currentGame); 
        }
        public static void markTheCell(Game currentGame)
        {
            int cellPosition = getValidCellPositionFromUser(currentGame);
            writeToCell(cellPosition, currentGame);
        }

        private static int getValidCellPositionFromUser(Game currentGame)
        {
            int cellPosition = 0;
            bool cellIsValidTarget = false; 
            while (!cellIsValidTarget)
            {
                cellPosition = getValidIntAsPosition();
                cellIsValidTarget = isIndividualCellAvailable(currentGame, cellPosition);
            } 
            return cellPosition;
        }

        private static int getValidIntAsPosition()
        {
            int cellPosition = -1;
            bool isChoiceFromUserValid = false;
            while (!isChoiceFromUserValid)
            {
                var choiceStringInput = Console.ReadLine();
                cellPosition = convertInputToInt(choiceStringInput);
                isChoiceFromUserValid = verifyInputWithinGrid(cellPosition);
            }
            return cellPosition;
        }
        private static int convertInputToInt(string choiceStringInput)
        {
            int choiceInputAsInt = 0;
            choiceStringInput = choiceStringInput.Trim();
            char choiceCharInput = choiceStringInput[0];
            try
            {
                choiceInputAsInt = Convert.ToInt32(new string(choiceCharInput, 1));
            }
            catch
            {
                Console.WriteLine(MainMenu.COMMON_MESSAGES["ERR001_INVALID_ENTRY"]);
            }
            return choiceInputAsInt;
        }
        private static bool verifyInputWithinGrid(int numericalChoice)
        {
            if(numericalChoice < 0 || numericalChoice > 8)
            {
                return false; 
            }
            return true;
        }
        private static bool writeToCell(int cellToBeMarked, Game currentGame)
        {
            try
            {
                currentGame.getGridGame().inputMark(cellToBeMarked, currentGame.getActivePlayer());
                return true;
            }
            catch
            {
                Console.WriteLine(MainMenu.COMMON_MESSAGES["ERR000_UNKNOWN"]);
            }
            return false;
        }
        private static bool isIndividualCellAvailable(Game currentGame, int cellInputPosition)
        {
            if (currentGame.getGridGame().checkIfCellIsAvailable(cellInputPosition))
            {
                return true;
            }
            Console.WriteLine(MainMenu.COMMON_MESSAGES["GAME_CELL_NOT_AVAILABLE"]);
            return false; 
        }
    }
}
