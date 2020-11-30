using ByodLauncher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public async Task<string> GetNotebookSpecs(string notebookModell)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(_configuration["Endpoints:ApiKeyNoteB"]), "apikey");
            content.Add(new StringContent(notebookModell), "param[model_name]");
            content.Add(new StringContent("list_models"), "method");
            var client = GetHttpClient();
            var result = await client.PostAsync("/", content);

            return await result.Content.ReadAsStringAsync();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["Endpoints:NoteB"]);
            return client;
        }
    }
}
