namespace ContactManagerApp.Api.Responses
{
    public class ContactResponse
    {
        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();
    }
}
