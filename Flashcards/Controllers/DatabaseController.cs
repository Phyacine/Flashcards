using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.NewFolder;
using Flashcards.Models;
using System.Collections;
using System.Data;

namespace Flashcards.Controllers
{
    internal class DatabaseController
    {
        public void UpdateStackItem(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Stacks SET Category = @Category " +
                        "WHERE StackId = @StackId;";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Category", stack.StackName);
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    };

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteStack(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Stacks WHERE StackId = @StackId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }
        public void AddNewStackItem(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Stacks (Category) VALUES (@Category)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Category", stack.StackName);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public StackItem GetStackByCategory(string category)
        {
            StackItem stack = new StackItem();
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Stacks WHERE Category = @Category";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            stack.StackId = (int)reader["StackId"];
                            stack.StackName = (string)reader["Category"];

                        }
                    }
                }
                catch (Exception ex) { }
            }
            return stack;
        }
        public void AddFlashCard(string question, string answer, StackItem stack)
        {

            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Cards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Question", question);
                        command.Parameters.AddWithValue("@Answer", answer);
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }
        public void RemoveFlashCard(FlashCardDto flashcard)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Cards WHERE CardId = @FlashCardId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FlashCardId", flashcard.FlashCardId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }

        public void AddStudySession(StudySession session)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO StudySessions (TotalQuestions, CorrectAnswers, Percentage, StudyDate, StackId) VALUES (@TotalQuestions, @CorrectAnswers, @Percentage, @StudyDate, @StackId)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TotalQuestions", session.QuestionsAnswered);
                        command.Parameters.AddWithValue("@CorrectAnswers", session.QuestionsAnsweredCorrectly);
                        command.Parameters.AddWithValue("@Percentage", session.Percentage);
                        command.Parameters.AddWithValue("@StudyDate", session.StudyDate);
                        command.Parameters.AddWithValue("@StackId", session.CategoryId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public DataTable GetStudyHistory(string year)
        {
            string sql = @"DECLARE @columns NVARCHAR(MAX), @sql NVARCHAR(MAX);
    SELECT @columns = STRING_AGG(QUOTENAME(StudyMonth), ',')
    FROM (
       SELECT DISTINCT FORMAT(StudyDate, 'MMM yyyy') AS StudyMonth
       FROM StudySessions
       WHERE FORMAT(StudyDate, 'yyyy') = @year
    ) AS UniqueMonths;
    SET @sql = '
    SELECT * 
    FROM (
       SELECT
           FORMAT(StudyDate, ''MMM yyyy'') AS StudyMonth,
           SessionId,
           StackId
       FROM
           StudySessions 
    WHERE FORMAT(StudyDate, ''yyyy'') = @year
    ) AS SourceTable
    PIVOT (
       COUNT(SessionId)
       FOR StudyMonth IN (' + @columns + ')
    ) AS PivotTable;';
    EXEC sp_executesql @sql, N'@year NVARCHAR(4)', @year;";

            DataTable dataTable = new DataTable();
            var history = new StudyHistory();
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@year", year);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }

                catch { }
            }

            return dataTable;
        }
        public DataTable GetStudyAverages(string year)
        {
            string sql = @"DECLARE @columns NVARCHAR(MAX), @sql NVARCHAR(MAX);
    SELECT @columns = STRING_AGG(QUOTENAME(StudyMonth), ',')
    FROM (
       SELECT DISTINCT FORMAT(StudyDate, 'MMM yyyy') AS StudyMonth
       FROM StudySessions
       WHERE FORMAT(StudyDate, 'yyyy') = @year
    ) AS UniqueMonths;
    SET @sql = '
    SELECT * 
    FROM (
       SELECT
           FORMAT(StudyDate, ''MMM yyyy'') AS StudyMonth,
           Percentage,
           StackId
       FROM
           StudySessions 
    WHERE FORMAT(StudyDate, ''yyyy'') = @year
    ) AS SourceTable
    PIVOT (
       AVG(Percentage)
       FOR StudyMonth IN (' + @columns + ')
    ) AS PivotTable;';
    EXEC sp_executesql @sql, N'@year NVARCHAR(4)', @year;";

            DataTable dataTable = new DataTable();
            var history = new StudyHistory();
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@year", year);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }

                catch { }
            }

            return dataTable;
        }
    }
}
