using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Enum
{
    public enum StatusCode
    {
        Ok = 200,
        Created = 201,
        BadRequest = 400,
        ServerError = 500,
        DuplicateCode = 409
    }
}
