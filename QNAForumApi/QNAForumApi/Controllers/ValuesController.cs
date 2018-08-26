using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net.Repository.Hierarchy;
using log4net.Util;
using QNAForum.Business;
using QNAForum.Core.Logging;
using Logger = QNAForum.Core.Logging.Logger;

namespace QNAForumApi.Controllers
{
    public class ValuesController : ApiController
    {
        private IQuestionService _questionService;
        private ILogger _logger;

        public ValuesController(IQuestionService questionService, ILogger logger)
        {
            _questionService = questionService;
            _logger = logger;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            var questions = _questionService.FindAllQuestions();
            foreach (var question in questions)
            {
                yield return question.QuestionTitle;
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
