using ByodLauncher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ByodLauncher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotebookController
    {
        private readonly IConfiguration _configuration;

        public NotebookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<Rootobject> GetNotebookSpecs(string notebookModell)
        {
            MultipartFormDataContent content = GenerateContent(notebookModell, "get_model_info");
            var client = GetHttpClient();
            var result = await client.PostAsync("", content);

            var resultString = await result.Content.ReadAsStringAsync();
            Rootobject notebook = JsonConvert.DeserializeObject<Rootobject>(resultString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            return notebook;
        }

        private MultipartFormDataContent GenerateContent(string notebookModell, string method)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(_configuration["Endpoints:ApiKeyNoteB"]), "apikey");

            if (!string.IsNullOrEmpty(notebookModell))
                content.Add(new StringContent(notebookModell), "param[model_name]");

            content.Add(new StringContent(method), "method");
            return content;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["Endpoints:NoteB"]);
            return client;
        }
    }
}
