namespace Gravatar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Gravatar Helper
    /// Helps generate image url from email address
    /// http://en.gravatar.com/site/implement/images/
    /// </summary>
    public static class GravatarHelper
    {
        public static string GetGravatarHash(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Please provide a valid email address.");
            }

            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
                var hash  = md5.ComputeHash(bytes);
                return hash.Select(b => b.ToString("x2")).Aggregate(String.Empty, (memo, s) => memo + s);
            }
        }
        
        public static string GetGravatarUrl(string email, bool includeProtocol = true)
        {
            var hash = GravatarHelper.GetGravatarHash(email);
            var url = $"//gravatar.com/avatar/{hash}";
            return includeProtocol ? $"https:{url}" : url;
        }

        public static string GetGravatarUrl(string email, GravatarHelperOptions options)
        {
            var url = GravatarHelper.GetGravatarUrl(email);

            var uri = new UriBuilder(url)
            {
                Scheme = options.Protocol,
                Port = -1
            };
            
            var query = new Dictionary<string,string>();
            
            //-- Add size if present
            if (options.Size > 0)
            {
                query.Add("s", options.Size.ToString());
            }

            //-- Add default image
            if (!String.IsNullOrWhiteSpace(options.DefaultImageUrl))
            {
                query.Add("d", options.DefaultImageUrl);
            }

            //-- Force default if set
            if (options.ForceDefault)
            {
                query.Add("f", "y");
            }

            //-- Add rating if present
            if (!String.IsNullOrWhiteSpace(options.Rating))
            {
                query.Add("r", options.Rating);
            }

            var @params = String.Join("&", query.Select(kv => $"{kv.Key}={kv.Value.ToLower()}"));

            if (!String.IsNullOrWhiteSpace(@params))
            {
                uri.Query = $"{@params}";
            }
            
            return uri.ToString();
        }
    }

    public class GravatarHelperOptions
    {
        public int Size { get; set; } = -1;
        public string DefaultImageUrl { get; set; }
        public string Rating { get; set; }

        public string Protocol { get; set; } = "https:";
        public bool ForceDefault { get; set; }
    }
}
