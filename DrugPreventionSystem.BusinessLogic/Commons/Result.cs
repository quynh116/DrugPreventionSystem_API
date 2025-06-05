using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Commons
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public ResultStatus ResultStatus { get; set; } // Changed from string to enum
        public string[]? Messages { get; set; }

        // Constructor for success with data
        public static Result<T> Success(T data, string message = "Success.")
        {
            return new Result<T>
            {
                Data = data,
                ResultStatus = ResultStatus.Success,
                Messages = new[] { message }
            };
        }

        // Constructor for success without data
        public static Result<T> Success(string message = "Success.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Success,
                Messages = new[] { message }
            };
        }

        // Constructor for Not Found error
        public static Result<T> NotFound(string message = "Resource not found.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.NotFound,
                Messages = new[] { message }
            };
        }

        // Constructor for duplicated data error
        public static Result<T> Duplicated(string message = "Duplicate data.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Duplicated,
                Messages = new[] { message }
            };
        }

        // Constructor for general error (server error, exception)
        public static Result<T> Error(string message = "An unknown error has occurred.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Error,
                Messages = new[] { message }
            };
        }

        // Constructor for invalid data
        public static Result<T> Invalid(string message = "Invalid data.", params string[]? errors)
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Invalid,
                Messages = errors?.Any() == true ? errors : new[] { message }
            };
        }

        // Constructor for failed operation (e.g., authentication failed, unauthorized operation)
        public static Result<T> Failed(string message = "Operation failed.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Failed,
                Messages = new[] { message }
            };
        }

        // Constructor for unverified account
        public static Result<T> NotVerified(string message = "Account not verified.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.NotVerified,
                Messages = new[] { message }
            };
        }

        // Constructor for failure (alternative to Failed for other contexts)
        public static Result<T> Failure(string message = "Operation failed.")
        {
            return new Result<T>
            {
                Data = default(T),
                ResultStatus = ResultStatus.Failure,
                Messages = new[] { message }
            };
        }
    }
}
