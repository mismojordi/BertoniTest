using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public interface IHttpClient
    {

        Task<T> GetAsync<T>(Uri url);

        Task<T> PostAsync<T>(Uri url, Object body);

    }
}
