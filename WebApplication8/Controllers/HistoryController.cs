using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using FireSharp.Extensions;

namespace WebApplication8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(ILogger<HistoryController> logger)
        {
            _logger = logger;
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "0gPlkFs7Dtft2rPcPPwBzWr7wbsO1kGFPYRHpsmc",
            BasePath = "https://netcoreapi-72866.firebaseio.com"
        };

        [HttpGet]
        public string Get()
        {
            List<History> histories = new List<History>();
            var client = new FireSharp.FirebaseClient(config);
            FirebaseResponse responce = client.Get("firetest");
            return JsonConvert.SerializeObject(responce.Body);
        }

        [HttpPost]
        public void Addistoria(History history)
        {
            var client = new FireSharp.FirebaseClient(config);
            PushResponse responce = client.Push("firetest/", history);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var client = new FireSharp.FirebaseClient(config);
            FirebaseResponse responce = client.Delete("firetest/"+id);
        }
    }
}
