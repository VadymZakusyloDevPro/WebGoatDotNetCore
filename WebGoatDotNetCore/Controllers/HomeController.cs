using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using WebGoatDotNetCore.Models;

namespace WebGoatDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private byte[] cryptKey;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About(string xss)
        {
            ViewData["Message"] = "Your application description page." + xss;            
            return View();
        }
                
        public string Xss(string param)
        {
            return "value " + param;
        }

        public IActionResult Contact(string url)
        {
            ViewData["Message"] = "Your contact page.";

            if (string.IsNullOrEmpty(url))
            {
                url = "https://www.google.com/";
            }
            return Redirect(url);
        }

        public IActionResult Privacy()
        {
            using (var aes = new AesManaged
            {
                KeySize = 100,
                BlockSize = 100,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                                //Use random IV
                aes.GenerateIV();
                var iv = aes.IV;
                using (var encrypter = aes.CreateEncryptor(cryptKey, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        //Encrypt Data
                        binaryWriter.Write("secretMessage");
                    }
                    var cipherText = cipherStream.ToArray();
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string url)
        {           

            byte[] encrypted;
            ICryptoTransform encryptor = null;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt,
                                                                 encryptor,
                                                                 CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write('a');
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
}
}
