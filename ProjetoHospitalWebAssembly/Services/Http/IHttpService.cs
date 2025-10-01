namespace ProjetoHospitalWebAssembly.Services.Http
{
    public interface IHttpService
    {
        Task<TResponse> GetJsonAsync<TResponse>(string uri);

        Task<TResponse> PostJsonAsync<TData, TResponse>(
            string uri,
            TData data);

        Task<TResponse> PostJsonAsync<TResponse>(string uri);

        Task PostJsonAsync<TData>(string uri, TData data);

        Task PostJsonAsync(string uri);

        Task<TResponse> PutJsonAsync<TData, TResponse>(
            string uri,
            TData data);

        Task PutJsonAsync<TData>(
            string uri,
            TData data);

        Task<byte[]> GetBytesAsync(string url);

        void SetBearerToken(string token);

        void RemoveBearerToken();

        Uri GetApiUrl();
    }
}