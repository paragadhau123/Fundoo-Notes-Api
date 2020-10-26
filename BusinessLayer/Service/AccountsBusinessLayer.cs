//-----------------------------------------------------------------------
// <copyright file="EmployeeBusinessLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer;
    using RepositoryLayer.Interface;

    /// <summary>
    /// EmployeeBusinessLayer Class
    /// </summary>
    public class AccountsBusinessLayer : IAccountsBL
    {
        private IAccountsRL repositoryLayer;

        public AccountsBusinessLayer(IAccountsRL repositoryLayer)
        {
            this.repositoryLayer = repositoryLayer;
        }

        public List<Accounts> GetAccountsDetails()
        {
            return this.repositoryLayer.GetAccountsDetails();
        }

        public bool RegisterAccount(AccountsDetails accounts)
        {
            return this.repositoryLayer.RegisterAccount(accounts);
        }

        public bool DeleteAccountById(string id)
        {
            return this.repositoryLayer.DeleteAccountById(id);
        }

        public bool UpdateAccountDetails(string id, Accounts accounts)
        {
            return this.repositoryLayer.UpdateAccountDetails(id, accounts);
        }

        public Accounts Login(LoginModel model)
        {
            try
            {
               return this.repositoryLayer.Login(model);
                              
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ForgetPassword(ForgetPassword model)
        {
            string Token = repositoryLayer.ForgetPassword(model);
            return Token;
        }

        public bool ResetPassword( ResetPassword resetPassword, string accountId)
        {
            bool pass = repositoryLayer.ResetPassword(resetPassword, accountId);
            return pass;
        }
    }
}
