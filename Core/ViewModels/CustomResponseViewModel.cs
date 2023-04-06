using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CustomResponseViewModel<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
        public bool Succes { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }

        public CustomResponseViewModel()
        {
            Errors = new List<string>();
            Succes = false;
        }
    }
}
