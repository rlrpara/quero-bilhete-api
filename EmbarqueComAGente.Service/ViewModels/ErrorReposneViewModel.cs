using System;
using System.Collections.Generic;

namespace QueroBilhete.Service.ViewModels
{
    public class ErrorReposneViewModel
    {
        public string TraceId { get; private set; }
        public List<ErrorDetailsView> Errors { get; set; }

        public ErrorReposneViewModel(string logRef, string message)
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetailsView>();
            AddError(logRef, message);
        }

        public class ErrorDetailsView
        {
            public string LogRef { get; set; }
            public string Message { get; set; }

            public ErrorDetailsView(string logRef, string message)
            {
                LogRef = logRef;
                Message = message;
            }
        }

        public void AddError(string logRef, string message)
            => Errors.Add(new ErrorDetailsView(logRef, message));
    }
}
