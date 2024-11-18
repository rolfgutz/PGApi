using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Application.Utils
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public object Data { get; private set; }  // Usando object para armazenar dados

        private Result(bool isSuccess, string errorMessage = null, object data = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Result Success(object data = null)
        {
            return new Result(true, null, data); // Dados passados aqui
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage); // Sem dados em falha
        }
    }
}
