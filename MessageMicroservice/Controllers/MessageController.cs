using MessageMicroservice.DataAccess;
using MessageMicroservice.Models;
using MessageUtil.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using MessageUtil;

namespace MessageMicroservice.Controllers
{
    public class MessageController : ApiController
    {
        [Route("api/message"), HttpGet]
        public IEnumerable<Message> GetAllMessages()
        {
            //FileLogger.Instance.Log("api/message HttpGet request", DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            List<Message> lista = new List<Message>();

            lista = MessageDB.GetAllMessages();
            if (lista == null)
                NotFound();

            return lista;
        }

        [Route("api/message/{id}"), HttpGet]
        public IHttpActionResult GetMessage(int id)
        {
            //FileLogger.Instance.Log("api/message/id HttpGet request", DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            Message poruka = MessageDB.GetMessageById(id);

            if (poruka == null)
                return NotFound();
            return Ok(poruka);
        }

        [Route("api/message"), HttpPost]
        public IHttpActionResult CreateMessageAsync([FromBody] Message message)
        {
            //FileLogger.Instance.Log("api/message HttpPost request", DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            Message result = MessageDB.CreateMessage(message);
            if ((result) == null)
                return BadRequest();
            return Ok(result);
        }

        [Route("api/message/{id}"), HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            //FileLogger.Instance.Log("api/message/id HttpDelete request", DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            bool result = MessageDB.DeleteUser(id);

            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}