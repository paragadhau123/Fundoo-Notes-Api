//-----------------------------------------------------------------------
// <copyright file="IBusinessLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer;

    public interface IAccountsBL
    {     
        List<Accounts> GetAccountsDetails();

        public bool RegisterAccount(AccountsDetails accounts);

        public bool DeleteAccountById(string id);

        public bool UpdateAccountDetails(string id, Accounts employee);

        Accounts Login(LoginModel model);
        string ForgetPassword(ForgetPassword model);
        bool ResetPassword( ResetPassword resetPassword,string accountId);
    }
}
