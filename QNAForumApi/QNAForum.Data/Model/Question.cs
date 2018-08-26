using System.Data;
using Dapper.Contrib.Extensions;
using QNAForum.Core.Data;

namespace QNAForum.Data.Model
{
    public class Question:BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string QuestionTitle { get; set; }

        public string  QuestionDescription { get; set; }
    }
}