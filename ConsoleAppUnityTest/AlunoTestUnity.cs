using System;
using System.Collections.Generic;

namespace ConsoleAppUnityTest
{
    public class AlunoTestUnity
    {

        #region Basic Repository Calls
        /// <summary>
        /// Adds this instance.
        /// </summary>
        public static void Add()
        {

            // set data
            var pessoa = new Models.Data.Aluno { curso = "mail", Nome = "Fiado" };


            // initialize unity
            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                // add and commit 
                unity.Repository<Models.Data.Aluno>().AddEntity(pessoa);
                unity.Commit();


            }


        }

        /// <summary>
        /// Adds the multiple.
        /// </summary>
        public static void AddMultiple()
        {


            var pessoa = new Models.Data.Aluno { curso = "mail", Nome = "Fiado" };
            var pessoa2 = new Models.Data.Aluno { curso = "mail2", Nome = "Fiado2" };

            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {


                unity.Repository<Models.Data.Aluno>().AddEntity(pessoa);
                unity.Repository<Models.Data.Aluno>().AddEntity(pessoa2);

                unity.Commit();


            }



        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public static void Update()
        {


            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {


                var pessoaUpdate = unity.Repository<Models.Data.Aluno>().GetEntity(13);

                pessoaUpdate.Nome = "FiadoII";
                pessoaUpdate.curso = "email2";

                unity.Commit();


            }


        }

        /// <summary>
        /// Add And Update Data With Single Commit.
        /// </summary>
        public static void AddAndUpdateWithSingleCommit()
        {


            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                var pessoa = new Models.Data.Aluno { curso = "mail3", Nome = "Fiado3" };

                unity.Repository<Models.Data.Aluno>().AddEntity(pessoa);


                var pessoaUpdate = unity.Repository<Models.Data.Aluno>().GetEntity(13);

                pessoaUpdate.Nome = "FiadoII";
                pessoaUpdate.curso = "email2";


                unity.Commit();


            }



        }

        /// <summary>
        /// Gets the aluno.
        /// </summary>
        public static void GetAluno()
        {

            var pessoa = new Models.Data.Aluno();

            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                pessoa = unity.Repository<Models.Data.Aluno>().GetEntity(10);

            }



        }

        /// <summary>
        /// Finds this instance.
        /// </summary>
        public static void Find()
        {


            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                var pessoaList = unity.Repository<Models.Data.Aluno>().Find(x => x.curso == "email" && x.Nome == "fia");

            }


        }

        /// <summary>
        /// Finds all.
        /// </summary>
        public static void FindAll()
        {

            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                var pessoaList = unity.Repository<Models.Data.Aluno>().FindAll();

            }


        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public static void Remove()
        {

            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                var pessoa = unity.Repository<Models.Data.Aluno>().GetEntity(10);

                unity.Repository<Models.Data.Aluno>().RemoveEntity(pessoa);

            }


        }

        /// <summary>
        /// Removes the by condition.
        /// </summary>
        public static void RemoveByCondition()
        {

            using (UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity)
            {

                var pessoa = unity.Repository<Models.Data.Aluno>().GetEntity(10);

                unity.Repository<Models.Data.Aluno>().RemoveEntity(y => y.alunoID == 2);

            }

        }
        #endregion


        #region Dynamic Queries 

        /// <summary>
        /// Builds the automatic query predicate [ Based on property values ].
        /// </summary>
        public void BuildAutomaticQueryPredicate()
        {

            // init unity
            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            BLL.Manager.AlunoRepository AlunoRepo;


            // initialize CustomRepository
            AlunoRepo = unity.CustomRepository<BLL.Manager.AlunoRepository>();


            // set data on object to search on DB
            var AlunoPredicate = new Models.Data.Aluno { data = DateTime.Now, alunoID = 10, curso = "mail", Nome = "Fiado" };


            // This will build a condition query autommaticaly
            // then search data
            var ListAluno = AlunoRepo.GetAlunosbyAutomaticBuildPredicate(AlunoPredicate);



        }

        /// <summary>
        /// Builds the manual query predicate [ Based on user definition values ].
        /// </summary>
        public void BuildManualQueryPredicate()
        {

            // init unity
            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            BLL.Manager.AlunoRepository AlunoRepo;


            // initialize CustomRepository
            AlunoRepo = unity.CustomRepository<BLL.Manager.AlunoRepository>();


            // set data on object to search on DB
            var AlunoPredicate = new Models.Data.Aluno { data = DateTime.Now, alunoID = 10, curso = "mail", Nome = "Fiado" };


            // This will build a condition query manualy by user define
            // then search data
            var ListAluno = AlunoRepo.GetAlunosbyManualBuildPredicate(AlunoPredicate);



        }


        #endregion


        #region StoredProcedure

        /// <summary>
        /// Storedprocedure call void.
        /// </summary>
        public void StoredProcedureCall_void()
        {

            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            UnityRepoContracts.Contracts.IExecuteCommands executeCommands = unity.ExecuteCommands();
            List<UnityRepoContracts.Common.Parameters> ParameterList = new List<UnityRepoContracts.Common.Parameters>();


            ParameterList.Add(new UnityRepoContracts.Common.Parameters { ParameterName = "@Nome", ParameterValue = "Baloi" });
            ParameterList.Add(new UnityRepoContracts.Common.Parameters { ParameterName = "@Curso", ParameterValue = "Psic" });

            executeCommands.ExecuteStoredProcedure("InsertAluno", ParameterList);


        }


        /// <summary>
        /// Storedprocedure call with return.
        /// </summary>
        public void StoredProcedureCall_Return()
        {

            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            UnityRepoContracts.Contracts.IExecuteCommands executeCommands = unity.ExecuteCommands();
            IEnumerable<Models.Data.Aluno> AllAlunos = System.Linq.Enumerable.Empty<Models.Data.Aluno>();


            AllAlunos = executeCommands.ExecuteStoredProcedureGetList<Models.Data.Aluno>("AllAluno");


        }

        /// <summary>
        /// Storedprocedure call with parameter return.
        /// </summary>
        public void StoredProcedureCallWithParameter_Return()
        {

            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            UnityRepoContracts.Contracts.IExecuteCommands executeCommands = unity.ExecuteCommands();
            List<UnityRepoContracts.Common.Parameters> ParameterList = new List<UnityRepoContracts.Common.Parameters>();
            Models.Data.Aluno aluno = new Models.Data.Aluno();

            ParameterList.Add(new UnityRepoContracts.Common.Parameters { ParameterName = "@ID", ParameterValue = 10 });
            aluno = executeCommands.ExecuteStoredProcedure<Models.Data.Aluno>("GetAluno", ParameterList);

        }

        #endregion


        #region Call CustomRepository


        /// <summary>
        /// Customs the repository.
        /// </summary>
        public void CustomRepository()
        {

            UnityRepoContracts.Contracts.IUnitOfWork unity = new BLL.Factory.UnityInitializer().Unity;
            BLL.Manager.AlunoRepository AlunoRepo;
            

            // initialize CustomRepository
            AlunoRepo = unity.CustomRepository<BLL.Manager.AlunoRepository>();


            // use
            var Aluno = AlunoRepo.GetEntity(10);

            
        }

        #endregion

    }

    
}


