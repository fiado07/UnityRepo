using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityRepo.UnityRepo;

namespace BLL.Factory
{
    public class UnityInitializer
    {

        public UnityRepoContracts.Contracts.IUnitOfWork Unity { get; }


        public UnityInitializer()
        {

            // initialize base unity
            Unity = new UnityOfWork(new DAL.DbContext.Connection().GetEntity());


        }


    }


}
