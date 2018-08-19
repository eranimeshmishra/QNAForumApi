using System;
using QNAForum.Business;
using QNAForum.Data;

namespace QNAForum.Business
{
    public class TestClass : ITestInterface
    {
        private IQuestion _question;
        public TestClass(IQuestion question)
        {
            _question = question;
        }
        public string TestMethod()
        {
            throw new ArgumentNullException("Raised from business");
            return _question.TestQuestion();
        }
    }
}