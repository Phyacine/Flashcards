﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Flashcards.Helpers
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

            string studySessionsQuery = "IF NOT EXISTS (SELECT * FROM sys.Tables " +
                "WHERE Name = 'StudySessions')" +
                "BEGIN CREATE TABLE StudySessions(" +
                "SessionId INT IDENTITY(1,1) PRIMARY KEY, " +
                "TotalQuestions INT," +
                "CorrectAnswers INT," +
                "StudyDate DATETIME," +
                "StackId INT NOT NULL FOREIGN KEY REFERENCES" +
                " Stacks(StackId) ON DELETE CASCADE);" +
                "END";
            Execute(studySessionsQuery);
        }

        public void PivotTable()
        {
            string sql = "DECLARE @columns NVARCHAR(max), @sql NVARCHAR(MAX);" +
                "SELECT @columns = STRING_AGG(QUOTENAME(StudyMonth), ',')" +
                "FROM (" +
                "   SELECT DISTINCT FORMAT(StudyDate, 'MMM yyyy') AS StudyMonth " +
                "   FROM StudySessions" +
                ") AS UniqueMonths;" +
                "SET @sql = '" +
                "SELECT * " +
                "FROM (" +
                "   SELECT" +
                "       FORMAT(StudyDate, ''MMM yyyy'') AS StudyMonth," +
                "       SessionId" +
                "   FROM" +
                "       StudySessions" +
                ") AS SourceTable" +
                "PIVOT (" +
                "   COUNT(SessionId)" +
                "   FOR StudyMonth IN (' + @columns + ')" +
                ") AS PivotTable;';" +
                "EXEC sp_executesql @sql";
        }

        public void ExecuteReader(string query)
        {

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }

            }

        }
    }
}