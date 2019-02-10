using MessageMicroservice.DataAccess;
using MessageMicroservice.Models;
using System;
using System.Collections.Generic;
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

            return Ok(poruka);
        }

    }
}