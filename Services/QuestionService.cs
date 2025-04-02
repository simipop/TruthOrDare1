using TruthOrDare1.Models;

namespace TruthOrDare1.Services
{
    public class QuestionService(QuestionReaderService questionReaderService)
    {
        private readonly QuestionReaderService _questionReaderService = questionReaderService;
        private List<Question>? _questions;
        private Random _random = new();
        private HashSet<int> _questionIndexes = new();

        public async Task InitializeAsync()
        {
            _questions = await _questionReaderService.ReadQuestionsAsync("input.csv");
        }


        public Question? PickQuestion(QuestionType questionType)
        {
            if (_questions == null || !_questions.Any())
            {
                return null;
            }

            // Reset usage flags when all questions were used
            if (_questionIndexes.Count >= _questions?.Count)
                ResetQuestionUsage(questionType);

            //get random question
            int index;
            do
            {
                index = _random.Next(0, _questions.Count);
            }
            while (_questionIndexes.Contains(index));

            //used index
            _questionIndexes.Add(index);

            return _questions[index]; 
        }

        private void ResetQuestionUsage(QuestionType questionType)
        {
            if (_questions != null)
            {
                foreach (var item in _questions.Where(q => q.QuestionType == questionType))
                {
                    item.IsUsed = false;
                }
            }
            _questionIndexes.Clear();
        }
    }
}
