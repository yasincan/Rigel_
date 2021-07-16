using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Rigel.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Rigel.Services.Services
{
    public class GoogleReCaptchaService : IGoogleReCaptchaService
    {
        private readonly IOptions<GoogleReCaptchaSettings> _options;
        public GoogleReCaptchaService(IOptions<GoogleReCaptchaSettings> options)
        {
            _options = options;
        }
        private readonly string apiUrl = "https://www.google.com/recaptcha/api/siteverify";
        public bool IsReCaptchaValidate(string responseKey, string appSettingSectionKey)
        {
            if (string.IsNullOrWhiteSpace(responseKey))
            {
                return false;
            }

            using var client = new WebClient();
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var result = client.DownloadString($"{apiUrl}?secret={_options.Value.SecretKey[appSettingSectionKey]}&response={responseKey}");
            return ParseValidationResult(result);

        }
        private bool ParseValidationResult(string validationResult) => (bool)JObject.Parse(validationResult).SelectToken("success");
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class GoogleReCaptchaSettings
    {
        public Dictionary<string, string> SecretKey { get; set; }
    }
}
