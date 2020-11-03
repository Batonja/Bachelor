using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorObjects
{
    public class Holder<T>
    {
        public Holder(T value)
        {
            Value = value;
        }

        public Holder(int errorCode, string description)
        {
            ErrorCode = (HttpStatusCode)errorCode;
            Description = description;
        }

        public static Holder<T> Success(T value)
        {
            Holder<T> holder = new Holder<T>(value)
            {
                ErrorCode = HttpStatusCode.OK
            };

            return holder;

        }

        public static Holder<T> Fail(int errorCode, string description)
        {
            Holder<T> holder = new Holder<T>(errorCode, description);
            return holder;
        }

        public T Value { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
