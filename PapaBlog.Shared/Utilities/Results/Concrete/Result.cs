using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace PapaBlog.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            this.ResultStatus = resultStatus;
        }

        public Result(ResultStatus resultStatus, string message)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
        }

        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Exception = exception;
        }

        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
