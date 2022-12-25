﻿using Dapper;
using FinalProject.Core.Common;
using FinalProject.Core.Data;
using FinalProject.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FinalProject.Infra.Repository
{
    public class BankRepository :IBankRepository
    {
        private readonly IDbContext dbContext;
        public BankRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Bankaccount> GetAllBank()
        {
            IEnumerable<Bankaccount> result = dbContext.Connection.Query<Bankaccount>("BankAccount_p.getallAccount", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public void CreateBank(Bankaccount bankaccount)
        {
            var p = new DynamicParameters();
            p.Add("BalanceP",bankaccount.Balance , dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("AccountNumP", bankaccount.Accountnum, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Execute("BankAccount_p.createAccount", p, commandType: CommandType.StoredProcedure);

        }
        public void UpdateBank(Bankaccount bankaccount)
        {
            var p = new DynamicParameters();
            p.Add("id", bankaccount.Accountid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("BalanceP", bankaccount.Balance, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("AccountNumP", bankaccount.Accountnum, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Execute("BankAccount_p.updateAccount", p, commandType: CommandType.StoredProcedure);
        }
        public void DeleteBank(int id)
        {
            var p = new DynamicParameters();

            p.Add("id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = dbContext.Connection.Execute("BankAccount_p.deleteAccount", p, commandType: CommandType.StoredProcedure);
        }


        public Bankaccount GetBankPageById(int id)
        {
            var p = new DynamicParameters();
            p.Add("id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<Bankaccount> result = dbContext.Connection.Query<Bankaccount>("BankAccount_p.getaccountbyid", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public Bankaccount checkforcard(string cardnumber, DateTime expdate, string cvvv, string fullname)
        {
            var p = new DynamicParameters();
            p.Add("AccountNo", cardnumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("exDate", expdate, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("cvvv", cvvv, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("fname", fullname, dbType: DbType.Int32, direction: ParameterDirection.Input);

            IEnumerable<Bankaccount> result = dbContext.Connection.Query<Bankaccount>("BankAccount_p.checkforcard", p, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();

        }
    }
}
