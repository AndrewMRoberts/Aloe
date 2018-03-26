using System;
using Microsoft.AspNetCore.Http;

namespace api.DataAccess
{
    public class TransactionUpload 
    {
        public string AccountName;
        public IFormFile UploadFile;
    }
}