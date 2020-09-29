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
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApplication8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        Dictionary<string, History> Historydictionary;
        List<History> Histories = new List<History>();
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
        public string Get(string User)
        {
            List<History> histories = new List<History>();
            var client = new FireSharp.FirebaseClient(config);
            FirebaseResponse responce = client.Get("firetest");
            Historydictionary = responce.ResultAs<Dictionary<string, History>>();
            
            if (Historydictionary != null)
            {
                foreach(var item in Historydictionary)
                {
                    if (item.Value.User == User)
                    {
                        History history = new History();
                        history.Id = item.Key;
                        history.Date = item.Value.Date;
                        history.User = item.Value.User;
                        history.Screen = item.Value.Screen;

                        Histories.Insert(0,history);
                    }
                }
            }
            return JsonConvert.SerializeObject(Histories);
        }

        [HttpPost]
        public void Post([FromBody]History history)
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
