namespace TruthOrDare1.Models
{
    public class Question
    {
        public Guid Id { get; set; } // not used
        public QuestionType QuestionType { get; set; }
        public int Time { get; set; }
        public string Text { get; set; }
        public bool IsUsed { get; set; }

        public Question(string type, int seconds, string text) {
            QuestionType = type == "dare" ? QuestionType.Dare : QuestionType.Truth;
            Time = seconds;
            Text = text;
        }

    }

    public enum QuestionType
    {
        Truth,
        Dare
    }
}