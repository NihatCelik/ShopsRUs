using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; }
        public string MethodName { get; set; }
        public int? UserId { get; set; }
        public string DateTime { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
