using Microsoft.AspNetCore.Components;
using TruthOrDare1.Models;
using TruthOrDare1.Services;


namespace TruthOrDare1.Components.Pages
{
    public partial class Home : ComponentBase
    {
        private Question? _question;
        private int? _seconds;
        private CancellationTokenSource? _cancellationToken;

        [Inject]
        private QuestionService QuestionService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await QuestionService.InitializeAsync();
        }

        private async Task ResetTimer(int seconds)
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();

            _seconds = seconds;
            try
            {
                while (_seconds > 0)
                {
                    await Task.Delay(1000, _cancellationToken.Token);
                    _seconds--;
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (TaskCanceledException) { }
        }

        protected async Task OnButtonClick(QuestionType questionType)
        {
            _question = QuestionService.PickQuestion(questionType);
            if (_question != null)
            {
                await ResetTimer(_question.Time);
            }
        }

        public void Dispose()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}
