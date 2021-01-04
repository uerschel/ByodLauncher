using ByodLauncher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

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

        private async Task<Notebook> GetNotebookSpecs(string notebookModell)
        {
            MultipartFormDataContent content = GenerateContent(notebookModell, "get_model_info");
            var client = GetHttpClient();
            var result = await client.PostAsync("", content);

            var resultString = await result.Content.ReadAsStringAsync();
            return GenerateNotebookFromResult(resultString);
        }

        private static Notebook GenerateNotebookFromResult(string resultString)
        {
            JObject jo = JObject.Parse(resultString);
            Notebook notebook = jo.ToObject<Notebook>();
            notebook.Brand = (string)jo.SelectToken("result.0.model_info[0].name");
            notebook.CpuBrand = (string)jo.SelectToken("result.0.cpu.prod");
            notebook.CpuModel = (string)jo.SelectToken("result.0.cpu.model");
            notebook.GpuBrand = (string)jo.SelectToken("result.0.gpu.prod");
            notebook.GpuModel = (string)jo.SelectToken("result.0.gpu.model");
            notebook.GpuMemorySize = (string)jo.SelectToken("result.0.gpu.memory_size");
            notebook.ScreenSize = (string)jo.SelectToken("result.0.display.size");
            notebook.RamSize = (string)jo.SelectToken("result.0.memory.size");
            notebook.RamType = (string)jo.SelectToken("result.0.memory.type");
            notebook.StorageType = (string)jo.SelectToken("result.0.primary_storage.model");
            notebook.StorageSize = (string)jo.SelectToken("result.0.primary_storage.cap");
            return notebook;
        }


        private static Notebook GenerateNotebookFromProfession(string profession)
        {
            string file = File.ReadAllText(@".\Specs\SpecsAllJobs.json");
            JObject jo = (JObject)JsonConvert.DeserializeObject(file);
            Notebook notebook = new Notebook();
            notebook.CpuBrand = (string)jo.SelectToken(profession + ".cpu.brand");
            notebook.CpuModel = (string)jo.SelectToken(profession + ".cpu.model");
            notebook.GpuBrand = (string)jo.SelectToken(profession + ".gpu.brand");
            notebook.GpuModel = (string)jo.SelectToken(profession + ".gpu.model");
            notebook.GpuMemorySize = (string)jo.SelectToken(profession + ".gpu.memory_size");
            notebook.ScreenSize = (string)jo.SelectToken(profession + ".display.size");
            notebook.RamSize = (string)jo.SelectToken(profession + ".memory.size");
            notebook.RamType = (string)jo.SelectToken(profession + ".memory.type");
            notebook.StorageType = (string)jo.SelectToken(profession + ".primary_storage.type");
            notebook.StorageSize = (string)jo.SelectToken(profession + ".primary_storage.size");
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

        [HttpGet]
        public async Task<NotebookComparer> CompareNotebookSpecs(string notebookModell, string lehrberuf)
        {
            MultipartFormDataContent content = GenerateContent(notebookModell, "get_model_info");
            var client = GetHttpClient();
            var result = await client.PostAsync("", content);

            var resultString = await result.Content.ReadAsStringAsync();
            Notebook notebookUser = GenerateNotebookFromResult(resultString);
            Notebook notebookProfession = GenerateNotebookFromProfession(lehrberuf);

            NotebookComparer notebookComparer = new NotebookComparer(notebookUser, notebookProfession);

            return notebookComparer;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["Endpoints:NoteB"]);
            return client;
        }
    }
}
