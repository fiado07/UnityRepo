using System;
using System.Collections.Generic;
using System.Linq;
using Models.Data;
using UnityRepo.Common;
using System.Linq.Expressions;

namespace BLL.Manager
{
    public class AlunoRepository : Factory.BaseRepository<Models.Data.Aluno>, Contracts.IAluno
    {

        private IQueryable<Aluno> AlunoRepo;

        public AlunoRepository(object context) : base(context)
        {

            AlunoRepo = context.BuildQuery<Models.Data.Aluno>();

        }

        public IEnumerable<Aluno> GetAlunosbyAutomaticBuildPredicate(Aluno aluno)
        {

            // build dynamic predicate according to property values
            var predicate = Helpers.BuildPredicate(aluno);

            return AlunoRepo.Where(predicate).Select(x => x);


        }

        public IEnumerable<Aluno> GetAlunosbyManualBuildPredicate(Aluno aluno)
        {

            IEnumerable<Aluno> AlunoList = Enumerable.Empty<Aluno>();
            Expression<Func<Aluno, bool>> predicate = null;


            // check if properties has values than
            // build conditions

            if (aluno?.alunoID != null)
            {
                predicate = x => x.alunoID == aluno.alunoID;
            }

            if (aluno?.curso != null)
            {
                predicate = Helpers.BuildPredicate(predicate, ExpressionType.And, x => x.curso == aluno.curso);
            }

            if (aluno?.data != null)
            {
                predicate = Helpers.BuildPredicate(predicate, ExpressionType.And, x => x.data == aluno.data);
            }

            // get data
            AlunoList = this.Find(predicate);

            return AlunoList;

        }



        public string isBest()
        {
            return AlunoRepo.FirstOrDefault(x => AlunoRepo.Max(u => u.nota) == x.nota).Nome;
        }




    }
}
