using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Contracts
{
    public interface IAluno
    {

        string isBest();

        IEnumerable<Models.Data.Aluno> GetAlunosbyAutomaticBuildPredicate(Models.Data.Aluno aluno);

        IEnumerable<Models.Data.Aluno> GetAlunosbyManualBuildPredicate(Models.Data.Aluno aluno);
    }
}
