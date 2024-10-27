using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSS_Scoring.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DSS_Scoring.Shared
{
    public class ResponseAPI<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

}
