using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHotel.Models
{
    public class Response<T>
    {
        public T Data { get; set; }

        public List<ErrorModel> Errors { get; set; }

        public void OnErrorThrowException()
        {
            if (Errors != null && Errors.Any())
            {
                throw new ApplicationException($"Message: {Errors[0].Message} Code: {Errors[0].Code}");
            }
        }
    }
}
