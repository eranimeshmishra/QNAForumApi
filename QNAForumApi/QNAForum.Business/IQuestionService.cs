using System.Collections.Generic;
using QNAForum.Data.Model;

namespace QNAForum.Business
{
    public interface IQuestionService
    {
        List<Question> FindAllQuestions();
    }
}