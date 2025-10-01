namespace ProjetoHospitalWebAssembly.Services.Http
{
    using System.Text.Json.Serialization;

    public class HttpError
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}