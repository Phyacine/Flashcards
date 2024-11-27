using Flashcards.Helpers;
using Flashcards.Models;
using Flashcards.NewFolder;
using Flashcards.Stores;
using Flashcards.Views;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Controllers
{
    internal class UIController
    {

        public void MainMenu()
        {
            //Menu options
            Console.Clear();

            var grid = new Grid();
            grid.AddColumn();
            grid.AddRow("Welcome! Please select an option");
            grid.AddEmptyRow();
            grid.AddRow("1. Start studying");
            grid.AddRow("2. Edit study topics");
            grid.AddRow("3. Show study history");
            grid.AddRow("0. Close program.");


            var panel = new Panel(grid);
            panel.Header = new PanelHeader("Main Menu");
            panel.Border = BoxBorder.Rounded;
            panel.Padding = new Padding(1, 1, 1, 1);
            AnsiConsole.Write(panel);
        }

        public void DisplayCategories()
        {

            var grid = new Grid();
            grid.AddColumn();
            foreach (var cat in ListStore.Stacks)
            {
                grid.AddRow($"{cat.StackId}. {cat.StackName}");
            }
            var panel = new Panel(grid);
            panel.Header = new PanelHeader("Categories");
            panel.Border = BoxBorder.Rounded;
            panel.Padding = new Padding(1);
            AnsiConsole.Write(panel);
        }

        public void DisplayInPanel(string[] strings)
        {
            var grid = new Grid();
            grid.AddColumn();
            foreach (var item in strings)
            {
                grid.AddRow(item);
            }
            var panel = new Panel(grid);
            panel.Header = new PanelHeader(SelectedCategoryStore.SelectedCategory.StackName);
            panel.Border = BoxBorder.Rounded;
            panel.Padding = new Padding(1);
            AnsiConsole.Write(panel);

        }
        public void DisplayInPanel(string[] strings, string header)
        {
            var grid = new Grid();
            grid.AddColumn();
            foreach (var item in strings)
            {
                grid.AddRow(item);
            }
            var panel = new Panel(grid);
            panel.Header = new PanelHeader(header);
            panel.Border = BoxBorder.Rounded;
            panel.Padding = new Padding(1);
            AnsiConsole.Write(panel);

        }





        public Panel ProvidePanel(string[] content, string header)
        {
            var grid = new Grid();
            grid.AddColumn();
            foreach (var item in content)
            {
                grid.AddRow(item);
            }

            var panel = new Panel(grid);
            panel.Header = new PanelHeader(header);
            panel.Border = BoxBorder.Rounded;
            panel.Padding = new Padding(1);


            return panel;
        }
        public void DisplayWithCategory(string[] content, string header)
        {
            Panel optionsPanel = ProvidePanel(content, header);

            string[] categoryInfo = new[]
            {
                $"Cards: {SelectedCategoryStore.SelectedCategory.FlashCards.Count}",
                "Accuracy: 100%"
            };

            Panel categoryPanel = ProvidePanel(categoryInfo, SelectedCategoryStore.SelectedCategory.StackName);



            Console.Clear();
            AnsiConsole.Write(categoryPanel);
            AnsiConsole.Write(optionsPanel);

        }
        public void DisplayQuestions()
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Question");
            table.AddColumn("Answer");
            table.Title(SelectedCategoryStore.SelectedCategory.StackName);

            foreach (var card in SelectedCategoryStore.SelectedCategory.FlashCards)
            {
                table.AddRow($"{card.DtoId}", $"{card.Question}", $"{card.Answer}");
            }
            AnsiConsole.Write(table);
        }

        public void DisplayHistory(DataTable history)
        {
            
            var table = new Table();
            table.AddColumn("Category");
            foreach(DataColumn column in history.Columns)
            {

                if(column.ColumnName != "StackId")
                {
                    table.AddColumn(column.ColumnName);
                }

            }

            foreach(DataRow category in history.Rows)
            {
                string item = ListStore.GetCategoryName(category["StackId"].ToString());
                var rowValues = new List<string> { item };

                foreach(DataColumn column in history.Columns)
                {
                    if(column.ColumnName != "StackId")
                    {
                        rowValues.Add(category[column].ToString());
                    }
                }
                table.AddRow(rowValues.ToArray());

            }

            AnsiConsole.Write(table);




        }
    }
}
