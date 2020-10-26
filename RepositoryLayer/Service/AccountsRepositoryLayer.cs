//----------------------- ------------------------------------------------
// <copyright file="EmployeeRepositoryLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using CommonLayer.Model;
    using CommonLayer.MSMQ;
    using Experimental.System.Messaging;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using ServiceStack;

    /// <summary>
    /// EmployeeRepositoryLayer Class
    /// </summary>
    public class AccountsRepositoryLayer : IAccountsRL
    {
        private readonly IMongoCollection<Accounts> _Account;

        public AccountsRepositoryLayer(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            this._Account = database.GetCollection<Accounts>(settings.AccountsCollectionName);
        }

        public List<Accounts> GetAccountsDetails()
        {
            return this._Account.Find(employee => true).ToList();
        }

        public Accounts Login(LoginModel model)
        {
            string pass = EncryptPassword(model.Password);
            List<Accounts> validation = _Account.Find(account => account.Email == model.Email && account.Password == model.Password).ToList();

            Accounts accounts = new Accounts();
            accounts.Id = validation[0].Id;
            accounts.EmployeeFirstName = validation[0].EmployeeFirstName;
            accounts.EmployeeLastName = validation[0].EmployeeLastName;
            accounts.PhoneNumber = validation[0].PhoneNumber;
            accounts.Email = validation[0].Email;
            accounts.Password = pass;
            accounts.Token = GenrateJWTToken(model.Email, accounts.Id);

            return accounts;

        }

        private static string EncryptPassword(string Password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
            String encrypted = Convert.ToBase64String(encrypt);
            return encrypted;
        }
        private string GenrateJWTToken(string email, string id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),

                            new Claim("id",userId),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(130),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }


        public bool RegisterAccount(AccountsDetails accountsDetails)
        {
            try
            {
                Accounts accounts = new Accounts()
                {
                    EmployeeFirstName = accountsDetails.EmployeeFirstName,
                    EmployeeLastName = accountsDetails.EmployeeLastName,
                    Email = accountsDetails.Email,
                    Password = accountsDetails.Password,
                    PhoneNumber = accountsDetails.PhoneNumber,
                };
                this._Account.InsertOne(accounts);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAccountById(string id)
        {
            try
            {
                this._Account.DeleteOne(accounts => accounts.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAccountDetails(string id, Accounts accounts)
        {
            try
            {
                this._Account.ReplaceOne(accounts => accounts.Id == id, accounts);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ForgetPassword(ForgetPassword model)
        {
            List<Accounts> details = this._Account.Find(accounts => accounts.Email == model.Email).ToList();
            Accounts accounts = new Accounts();

            accounts.Email = details[0].Email;
            accounts.Id = details[0].Id;

            string Token = GenrateJWTToken(accounts.Email, accounts.Id);
            Msmq msmq = new Msmq();
            msmq.SendToMsmq(Token, accounts.Id);
            

            return Token;
        }

        public bool ResetPassword(ResetPassword resetPassword, string accountId)
        {           
            var filter = Builders<Accounts>.Filter.Eq("Id", accountId);
            var update = Builders<Accounts>.Update.Set("Password", resetPassword.NewPassword);
            _Account.UpdateOne(filter, update);
            return true;              
        }
        
    }
} 