namespace ProjetoHospitalWebAssembly.Services.Http
{
    using Microsoft.AspNetCore.Components.Authorization;
    using ProjetoHospitalShared.Converters;
    using System.Globalization;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class HttpService : IHttpService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly AppSettings appSettings;

        public HttpService(
            AuthenticationStateProvider authenticationStateProvider,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            appSettings = configuration.Get<AppSettings>();
            this.authenticationStateProvider = authenticationStateProvider;
            this.httpClient = httpClient;
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            jsonSerializerOptions
                .Converters
                .Add(new JsonTimeSpanConverter());

            this.httpClient.DefaultRequestHeaders
                .Remove("Accept-Language");
            this.httpClient.DefaultRequestHeaders
                .Add("Accept-Language", CultureInfo.CurrentCulture.ToString());
        }

        public async Task<TResponse> GetJsonAsync<TResponse>(string uri)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .GetAsync(fullUrl)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                await HandleHttpStatusError(response)
                    .ConfigureAwait(false);
            }

            var responseContent = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(
                responseContent,
                jsonSerializerOptions);
        }

        public async Task<TResponse> PostJsonAsync<TData, TResponse>(
            string uri,
            TData data)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .PostAsJsonAsync(
                    fullUrl,
                    data,
                    jsonSerializerOptions)
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                throw new HttpRequestException(
                    $"{response.StatusCode}: {response.ReasonPhrase} - {erro}");
            }

            var responseContent = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(
                responseContent,
                jsonSerializerOptions);
        }

        public void SetBearerToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("bearer", token);
        }

        public void RemoveBearerToken()
        {
            httpClient.DefaultRequestHeaders.Authorization = default;
        }

        public async Task<TResponse> PostJsonAsync<TResponse>(string uri)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .PostAsJsonAsync(
                    fullUrl,
                    jsonSerializerOptions)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"{response.StatusCode}: {response.ReasonPhrase}");
            }

            var responseContent = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(
                responseContent,
                jsonSerializerOptions);
        }

        public async Task PostJsonAsync<TData>(
            string uri,
            TData data)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .PostAsJsonAsync(
                    fullUrl,
                    data,
                    jsonSerializerOptions)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"{response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task PostJsonAsync(string uri)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .PostAsJsonAsync(
                    fullUrl,
                    jsonSerializerOptions)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task PutJsonAsync<TData>(string uri, TData data)
        {
            var fullUrl = DynamicAddress(uri);

            var response = await httpClient
                .PutAsJsonAsync(
                    fullUrl,
                    data,
                    jsonSerializerOptions)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"{response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task<TResponse> PutJsonAsync<TData, TResponse>(
            string uri,
            TData data)
        {
            var fullUrl = DynamicAddress(uri);
            var response = await httpClient
                .PutAsJsonAsync(
                    fullUrl,
                    data,
                    jsonSerializerOptions)
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"{response.StatusCode}: {response.ReasonPhrase}");
            }

            var responseContent = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(
                responseContent,
                jsonSerializerOptions);
        }

        public Uri GetApiUrl()
        {
            return appSettings.ApiBaseUrl;
        }

        public async Task<byte[]> GetBytesAsync(string uri)
        {
            var fullUrl = DynamicAddress(uri);

            var response = await httpClient
                .GetAsync(fullUrl)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                await HandleHttpStatusError(response)
                    .ConfigureAwait(false);
            }

            var responseContentBytes = await response.Content
                .ReadAsByteArrayAsync()
                .ConfigureAwait(false);

            return responseContentBytes;
        }

        private async Task HandleHttpStatusError(HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await ((ApiAuthenticationStateProvider)authenticationStateProvider)
                        .MarkUserAsLoggedOut()
                        .ConfigureAwait(false);

                    throw new UnauthorizedAccessException();
                }

                var errorContent = await response
                    .Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);
                var errorResponse = JsonSerializer.Deserialize<HttpError>(
                    errorContent,
                    jsonSerializerOptions);

                throw new HttpRequestException(errorResponse.Message);
            }
            catch (JsonException)
            {
                throw new HttpRequestException(
                    $"{(int)response.StatusCode}: Erro inesperado");
            }
        }

        private string DynamicAddress(string url)
        {
            var baseAddressSettings = appSettings
                .ApiBaseUrl
                .ToString();
            if (baseAddressSettings.Last() == '/')
            {
                if (baseAddressSettings.Contains(
                    "/api/",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return $"{baseAddressSettings}{url}";
                }

                return $"{baseAddressSettings}api/{url}";
            }
            else
            {
                if (baseAddressSettings.Contains(
                    "/api",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return $"{baseAddressSettings}/{url}";
                }

                return $"{baseAddressSettings}/api/{url}";
            }
        }
    }
}