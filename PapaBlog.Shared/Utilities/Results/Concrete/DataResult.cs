using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace PapaBlog.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            this.ResultStatus = resultStatus;
            this.Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Data = data;
            this.Exception = exception;
        }

        public T Data { get; }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
