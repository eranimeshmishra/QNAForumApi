using System.Collections.Generic;
using QNAForum.Core.Data;
using QNAForum.Data.Model;

namespace QNAForum.Data
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}