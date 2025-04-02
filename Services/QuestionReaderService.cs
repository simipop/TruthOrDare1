using System.Text.Json;
using TruthOrDare1.Models;

namespace TruthOrDare1.Services
{
    public class QuestionReaderService
    {
        private readonly IWebHostEnvironment _environment;
        public QuestionReaderService(IWebHostEnvironment environment) 
        { 
        
            _environment = environment;
        }

        public async Task<List<Question>> ReadQuestionsAsync(string fileName)
        {
            var path = Path.Combine(_environment.WebRootPath, fileName);
            var lines = await File.ReadAllLinesAsync(path);

            var questions = new List<Question>();

            foreach (var line in lines)
            {
                var props = line.Split(',');
                if (int.TryParse(props[1], out int seconds))
                {
                    questions.Add(new Question(props[0], seconds, props[2]));
                }
            }
            return questions ?? [];
        }
    }
}
