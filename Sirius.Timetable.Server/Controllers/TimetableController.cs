using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sirius.Timetable.Server.Controllers
{
    [Route("[controller]")]
    public class TimetableController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new NoContentResult();
        }

        // GET api/values/5
        [HttpGet("{dateString}")]
        public async Task<IActionResult> Get(string dateString)
        {
            DateTime date;
            if(!DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                return new ObjectResult("");
            }
            try
            {
                var request = WebRequest.Create("http://calendar.talantiuspeh.ru/scheduler2/json.php?key=d2fc437c3f951b20fc7a6efa517a62f6&day=" + dateString);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "application/x-www-form-urlencoded";
                var response = await request.GetResponseAsync();
                byte[] arr = { };
                var stream = response.GetResponseStream();//.Read(arr, 0, (int)response.GetResponseStream().Length);
                //System.Text.Encoding.RegisterProvider(System.Text.UTF8Encoding)
                var streamR = new System.IO.StreamReader(stream, System.Text.UTF8Encoding.UTF8);
                string result = streamR.ReadToEnd();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return new NoContentResult();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
