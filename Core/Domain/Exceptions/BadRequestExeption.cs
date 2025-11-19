using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BadRequestExeption(List<string> erroes):Exception("Validation Failed")
    {
        public List<string> Errors { get; } = erroes;
    }
}
