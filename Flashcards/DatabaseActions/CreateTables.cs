﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Flashcards.DatabaseActions
{
    internal class CreateTables
    {

        public CreateTables()
        {
            string stacksQuery = "IF NOT EXISTS (SELECT * FROM sys.Tables " +
                    "WHERE Name = 'Stacks')" +
                    "BEGIN CREATE TABLE Stacks(" +
                    "Category nvarchar(50) NOT NULL," +
                    "StackId int PRIMARY KEY IDENTITY); " +
                    "END";
            Execute(stacksQuery);

            string flashCardsQuery = "IF NOT EXISTS (SELECT * FROM sys.Tables " +
                    "WHERE Name = 'Cards')" +
                    "BEGIN CREATE TABLE Cards(" +
                    "CardId INT IDENTITY(1, 1) PRIMARY KEY," +
                    "Question NVARCHAR(MAX)," +
                    "Answer NVARCHAR(MAX)," +
                    "StackId INT NOT NULL FOREIGN KEY REFERENCES Stacks(StackId) ON DELETE CASCADE); " +
                    "END ";
            Execute(flashCardsQuery);
        }
        public void Execute(string query)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }

        }
    }
}
