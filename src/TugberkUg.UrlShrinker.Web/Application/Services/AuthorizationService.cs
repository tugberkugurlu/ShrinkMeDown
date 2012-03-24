using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;

namespace TugberkUg.UrlShrinker.Web.Application.Services {

    public class AuthorizationService : IAuthorizationService {

        private readonly IDocumentSession _ravenSession;

        public AuthorizationService(IDocumentSession ravenSession) {

            _ravenSession = ravenSession;
        }

        //temporary
        public AuthorizationService() {

            _ravenSession = MvcApplication.Store.OpenSession(Constants.UrlShortenerDbName);
        }

        public bool Authorize(string userName, string password) {

            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {

            throw new NotImplementedException();
        }

        public Data.DataAccess.User CreateUser(string userName, string password, string email) {

            _ravenSession.Store(new TugberkUg.UrlShrinker.Data.DataAccess.User { 
                IsApproved = true,
                UserName = userName,
                Password = getHashedPassword(password),
                HashAlgorithm = "SHA512"
            });

            throw new NotImplementedException();
        }

        //private helpers
        private string getHashedPassword(string rawPassword) {

            string id = Guid.Parse("8681941A-76C2-4120-BC34-F800B5AAB5A5".ToLower()).ToString();
            string date = DateTime.Today.ToString("yyyy-MM-dd");

            Console.WriteLine(id);
            Console.WriteLine(date);

            using (System.Security.Cryptography.SHA512Managed hashTool =
                new System.Security.Cryptography.SHA512Managed()) {

                Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(id, date));
                Byte[] EncryptedBytes = hashTool.ComputeHash(PasswordAsByte);
                hashTool.Clear();

                Console.WriteLine(Convert.ToBase64String(EncryptedBytes));

            }

            using (System.Security.Cryptography.SHA1Managed hashTool = new System.Security.Cryptography.SHA1Managed()) {

                Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes("dslfdkjLK85kldhnv$n000#knf");
                Byte[] EncryptedBytes = hashTool.ComputeHash(PasswordAsByte);
                hashTool.Clear();

                Console.WriteLine(Convert.ToBase64String(EncryptedBytes));
            }

            return "";
        }

        private bool isEqual(string rawPassword, string hashedPasword) {

            return false;
        }
    }
}