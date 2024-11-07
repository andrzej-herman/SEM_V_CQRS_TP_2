using Newtonsoft.Json;
using System.Data.SqlClient;
using Test.DataUpdater;

var path = "S:\\ZIMA 2024\\TECHNOLOGIE_INTERNETOWE\\questions.json";
var text = File.ReadAllText(path);
var questions = JsonConvert.DeserializeObject<List<Question>>(text);


var count = 1;
var connectionString = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=True";
var connection = new SqlConnection(connectionString);
connection.Open();
foreach (var question in questions!)
{
	var properContent = question.Content!.Replace("'", "\"");
	var questionGuid = Guid.NewGuid();	
	var query = $"INSERT INTO Questions VALUES ('{questionGuid}', {question.Category}, '{properContent}')";
	var commmand = new SqlCommand(query, connection);
	var id = commmand.ExecuteNonQuery();
	if (id != 0)
	{
		var answerCount = 1;
		foreach (var answer in question.Answers!)
		{
            var answerGuid = Guid.NewGuid();
            properContent = answer.Content!.Replace("'", "\"");
			var correct = answer.IsCorrect ? 1 : 0;
			var ansquery = $"INSERT INTO Answers VALUES ('{answerGuid}', '{properContent}', {correct}, '{questionGuid}')";
            var answerCommand = new SqlCommand(ansquery, connection);
            answerCommand.ExecuteNonQuery();
			Console.WriteLine($" Dodano odpowiedź nr {answerCount} do pytania nr {count}");
			answerCount++;
        }

		answerCount = 1;
        Console.WriteLine($"Przetworzono {count} z {questions.Count}");
        count++;
		
	}
	
}

connection.Close();
if (count == questions.Count)
{
    Console.WriteLine();
    Console.WriteLine("Udało się dodać wszystkie pytania");
}
Console.ReadLine();