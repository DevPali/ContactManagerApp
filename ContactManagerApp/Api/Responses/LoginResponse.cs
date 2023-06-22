namespace ContactManagerApp.Api.Responses
{
    public class LoginResponse
    {
        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();
    }
}
