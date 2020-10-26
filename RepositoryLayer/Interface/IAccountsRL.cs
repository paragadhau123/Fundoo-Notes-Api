//-----------------------------------------------------------------------
// <copyright file="IRepositoryLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;

    public interface IAccountsRL
    {
        List<Accounts> GetAccountsDetails();

        bool RegisterAccount(AccountsDetails accounts);

        public bool DeleteAccountById(string id);

        public bool UpdateAccountDetails(string id, Accounts accounts);

        Accounts Login(LoginModel model);

        string ForgetPassword(ForgetPassword model);

        bool ResetPassword( ResetPassword resetPassword, string accountId);
    }
}
