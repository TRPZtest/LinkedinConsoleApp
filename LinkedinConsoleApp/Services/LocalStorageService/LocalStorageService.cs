using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.LocalStorageService
{
    public class LocalStorageService
    {
        private LocalStorage _storage;
        public LocalStorageService() 
        {
            _storage = new LocalStorage();

        }

        public string GetToken()
        {
            var isTokenExists = _storage.Exists("token");

            if (!isTokenExists)
                return string.Empty;
            else
                return _storage.Get<string>("token");
        }

        public void SetToken(string token)
        {
            _storage.Store("token", token);
            _storage.Persist();
        }
    }
}
