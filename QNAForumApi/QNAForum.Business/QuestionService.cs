using System.Collections.Generic;
using System.Linq;
using QNAForum.Data;
using QNAForum.Data.Model;

namespace QNAForum.Business
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public List<Question> FindAllQuestions()
        {
            return  _questionRepository.FindAll().ToList();
        }
    }
}