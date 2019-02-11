using MessageMicroservice.DataAccess;
using MessageMicroservice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Util.Logger;

namespace MessageMicroservice.Controllers
{
    public class MessageController : ApiController
    {
        [Route("api/message"), HttpGet]
        public IEnumerable<Message> GetAllMessages()
        {
            List<Message> lista = new List<Message>();

            lista = MessageDB.GetAllMessages();
            if (lista == null)
                BadRequest();

            return lista;
        }

        [Route("api/message/{id}"), HttpGet]
        public IHttpActionResult GetMessage(int id)
        {
           
            Message poruka = MessageDB.GetMessageById(id);

            if (poruka == null)
                return BadRequest();
            LogHelper.Log(LogTarget.File, poruka.ToString(), DateTime.Now.ToString());
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

        [Route("api/message/{id}"), HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            bool result = MessageDB.DeleteUser(id);

            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}