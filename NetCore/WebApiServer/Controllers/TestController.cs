using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILog _logger;

        public static Dictionary<string, string> dicRcMsg = null;
        public TestController()
        {
            this._logger = LogManager.GetLogger(Startup.repository.Name, typeof(TestController));
        }

        [HttpPost()]
        public ContentResult Post([FromBody]object content)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(content.ToString());

            _logger.Info($"Post Req {obj}");

            string rcBody = JsonConvert.SerializeObject(obj);
            if (dicRcMsg.ContainsKey("PostReturnBody"))
            {
                rcBody = dicRcMsg["PostReturnBody"];
            }

            _logger.Info($"Post Rsp {obj}");
            return GetBody(rcBody);
        }

        [HttpGet()]
        public ContentResult Get()
        {
            var result = new JObject() { new JProperty("result", "200")};

            string rcBody = JsonConvert.SerializeObject(result);
            if (dicRcMsg.ContainsKey("GetReturnBody"))
            {
                rcBody = dicRcMsg["GetReturnBody"];
            }

            _logger.Info($"Get Rsp {rcBody}");

            return GetBody(rcBody);
        }

        ContentResult GetBody(string sSontent)
        {
            var content = new ContentResult();
            content.Content = sSontent;
            content.ContentType = "application/json";
            return content;
        }
    }
}