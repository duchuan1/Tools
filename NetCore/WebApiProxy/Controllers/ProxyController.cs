using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace WebApiProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory = null;
        ILog _logger = null;
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = LogManager.GetLogger(Startup.repository.Name, typeof(ProxyController));
        }

        [HttpGet("test")]
        public string Test(string test)
        {
            try
            {
                _logger.Info($"Get {test}");
                return test;
            }
            catch (Exception ex)
            {
                _logger.Error($"Get Error： {ex.Message}");
                throw ex;
            }
        }

        [HttpGet]
        public ContentResult Get(string url)
        {
            try
            {
                _logger.Info($"Get Req Url: {url}");
                _logger.Info($"Get Req Headers: {JsonConvert.SerializeObject(Request.Headers)}");

                HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Clear();
                foreach (var header in Request.Headers)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToString());
                }
                HttpResponseMessage httpResponseMessage =  httpClient.GetAsync(url).GetAwaiter().GetResult();
                string sContent = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                _logger.Info($"Get Rsp Content: {sContent}");

                return GetBody(sContent);
            }
            catch(Exception ex)
            {
                _logger.Error($"Get【{url}】Error： {ex.Message}");
                throw ex;
            }
        }

        [HttpPost]
        public  ContentResult Post(string url)
        {
            try
            {
                _logger.Info($"Post Req Url: {url}");
                _logger.Info($"Post Req Headers: {JsonConvert.SerializeObject(Request.Headers)}");

                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Clear();
                foreach (var header in Request.HttpContext.Request.Headers)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToString());
                }

                //HttpContext httpContext = new StreamContent(Request.Body); 
                string sBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();
                _logger.Info($"Post Req Body: {sBody}");

                StringContent content = new StringContent(sBody);
                content.Headers.Clear();
                //StreamContent content = new StreamContent(Request.Body);

                foreach (var header in Request.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString());
                }

                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, content).GetAwaiter().GetResult();

                string sContent = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                _logger.Info($"Post Rsp Headers: {JsonConvert.SerializeObject(httpResponseMessage.Headers)}");
                _logger.Info($"Post Rsp Content: {sContent}");

                return GetBody(sContent) ;
            }
            catch(Exception ex)
            {
                _logger.Error($"Post【{url}】Error： {ex.Message}");
                throw ex;

            }
        }
        ContentResult GetBody(string sSontent)
        {
            var content = new ContentResult();
            content.Content = sSontent;
            content.ContentType = "application/json;charset=utf-8";

            return content;
        }
        //public async Task<HttpResponseMessage> Put(string url)
        //{
        //}

        //public async Task<HttpResponseMessage> Delete(string url)
        //{
        //}
    }
}
