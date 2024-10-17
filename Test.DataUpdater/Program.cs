using Newtonsoft.Json;
using System.Data.SqlClient;
using Test.Entity.Entities;

var path = "S:\\ZIMA 2024\\TECHNOLOGIE_INTERNETOWE\\questions.json";
var text = File.ReadAllText(path);
var questions = JsonConvert.DeserializeObject<List<Question>>(text);


var count = 0;
var connectionString = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=True";
var connection = new SqlConnection(connectionString);
connection.Open();
foreach (var question in questions!)
{
	var properContent = question.Content!.Replace("'", "");
	var query = $"INSERT INTO Questions VALUES ({question.Category}, '{properContent}')";
	var commmand = new SqlCommand(query, connection);
	var id = commmand.ExecuteNonQuery();
	var qId = 0;
	if (id != 0)
	{
		query = "SELECT QuestionId FROM Questions ORDER BY QuestionId DESC";
		var command = new SqlCommand(query, connection);
		var reader = command.ExecuteReader();
		while(reader.Read())
		{
			qId = reader.GetInt32(0);
			break;
		}

		foreach (var answer in question.Answers)
		{
			properContent = answer.Content!.Replace("'", "");
			var correct = answer.IsCorrect ? 1 : 0;
			query = $"INSERT INTO Answers VALUES ({properContent}, {correct}, {qId})";
		}

		count++;
		Console.WriteLine($"Przetworzono {count} z {questions.Count}");
	}
	
}

connection.Close();
if (count == questions.Count)
{
    Console.WriteLine("Udało się dodać wszystkie pytania");
}
Console.ReadLine();