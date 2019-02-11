using MessageMicroservice.DataAccess;
using MessageMicroservice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MessageMicroservice.Controllers
{
    public class MessageController : ApiController
    {
        public IEnumerable<Message> GetAllMessages()
        {
            List<Message> lista = new List<Message>();

            return lista;
        }

        [Route("api/message/{id}"), HttpGet]
        public IHttpActionResult GetMessage(int id)
        {
            Message poruka = MessageDB.GetMessageById(id);

            if (poruka == null)
                return BadRequest();
            Console.WriteLine("cao");
            return Ok(poruka);
        }

        [Route("api/message"), HttpPost]
        public IHttpActionResult CreateMessageAsync([FromBody] Message message)
        {
            Message result = MessageDB.CreateMessage(message);
            if ((result) == null)
                return BadRequest();
            return Ok(result);
        }

    }
}