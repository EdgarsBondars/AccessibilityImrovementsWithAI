/*using Microsoft.Extensions.Configuration;
using Rystem.OpenAi;
using Rystem.OpenAi.Chat;

namespace AccessibilityImprovementsWithAI.ChatGPTInteractions
{
    public class ChatLogic
    {
        public void TestOne()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var ApiKey = config["ApiKey"];
            OpenAiService.Instance.AddOpenAi(settings =>
            {
                settings.ApiKey = ApiKey;
            }, "NoDI");

            IOpenAi openAiApi = OpenAiService.Create();
            var results = openAiApi.Chat
                    .Request(new ChatMessage { Role = ChatRole.User, Content = "Hello!! How are you?" })
                    .WithModel(ChatModelType.Gpt4)
                    .WithTemperature(1)
                    .ExecuteAsync();
        }
    }
}
*/