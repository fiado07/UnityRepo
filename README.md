

# UnityRepo - Is a unity of work pattern based for .Net

This UnityRepo is a UnityOfWork package gives a lite way to interact with sql server with fluent API. 



#### DataBase support

The UnityRepo supports only sql server database.


#### Archtecture

<img src="\\Gifs\Archtecture.gif" style="width:200px" />

#### Project Structure

<img src="\\Gifs\Project Structure.png" style="width:300px" />

#### Add References

<img src="\\Gifs\Add References.gif" style="width:800px" />

#### Add Context

<img src="\\Gifs\Add Context.gif" style="width:800px" />

#### Add Context Configuration

<img src="\\Gifs\Add Context Config.gif" style="width:800%" />

#### Context configuration

After create a EDMX file context, must be created a context partial class to hold Entity Framework fix error( you can define a constructor parameter if needed ) :

```csharp
 public partial class Entities : System.Data.Entity.DbContext
    {
        public Entities(string connectionString)
            : base(connectionString)
        {
        }

        public void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 							'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly Is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        }
    }
```

#### Define Connection

<img src="\\Gifs\Add Context Connection.gif" style="width:800%" />

#### Extract Context Models from EDMX

<img src="\\Gifs\Add Context Models.gif" style="width:800%" />

#### Add UnityRepo To BLL

<img src="\\Gifs\Add UnityRepo.gif" style="width:800%" />

#### Unity of work Initializer 

<img src="\\Gifs\Add UnityRepo Initializer.gif" style="width:800%" />

For N-Layered project you don't need to add this **UnityRepo** reference to the main project anymore, just add **UnityRepoContracts**(https://www.nuget.org/packages/UnityRepoContracts/) nuget reference to the main project.

```csharp
 public class UnityInitializer
 {
        public UnityRepoContracts.Contracts.IUnitOfWork Unity { get; }
        public UnityInitializer()
        {
            // initialize base unity
            Unity = new UnityOfWork(new DAL.DbContext.Connection().GetEntity());

        }
}
```

#### Add Repository Base to project 

<img src="\\Gifs\Add UnityRepo RepoBase.gif" style="width:800%" />

#### Add **UnityRepoContracts** to UI

<img src="\\Gifs\Add UnityRepo Contracts.gif" style="width:800%" />

#### Unity of work by examples

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
```



#### Add Single Entity

```csharp
// initialize unity
 IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
// set new entity data
Aluno aluno = new Aluno { curso = "Fisic", data = new DateTime(2018, 3, 12), Nome = "Fiado" };
 
// save changes or commit
 UnityOWBLL.Commit();
```



#### Add Multiple Entities

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
 
 // set new entity data - 1
Aluno aluno = new Aluno { curso = "Fisic", data = new DateTime(2018, 3, 12), Nome = "Fiado" };
 
// set new entity data - 2
disciplina disciplina = new disciplina { nome = "pt" };
     
// add  
UnityOWBLL.Repository<Models.Data.Aluno>().AddEntity(pessoa);

// save changes or commit all
UnityOWBLL.Commit();
```



#### Update Entity

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
  
// Get entity
Aluno aluno = UnityOWBLL.Repository<Aluno >().GetEntity(1)  ;
 
// change
aluno.Nome = "Baloi";
  
// save changes or commit
UnityOWBLL.Commit();
```

#### Update Multiple Columns Using Mappers

Mappers is a tool used to copy data from one object to another(https://www.nuget.org/packages/Mappers/).

```csharp
// use proxy now
IUnitOfWork Unity = new BLL.Factory.UnityInitializer().Unity;                            
                       
AlunoRepository alunoCustomRepo = Unity.CustomRepository<AlunoRepository, Aluno>();
 
 // initialize mapper
Mappers.Mapper mapper = new Mappers.Mapper();  

// new data to update
Aluno alunoSource = new Aluno { Nome = "First", nota =20 };

// database data to be updated/changed
Aluno alunoTarget = alunoCustomRepo.GetEntity(20);

// mapps
mapper.Map(alunoSource, alunoTarget);

Unity.Commit(); 
```

#### Get and search

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
 
// Get entity
Aluno aluno = UnityOWBLL.Repository<Aluno >().GetEntity(1);         
 
// get one
var findOne = UnityOWBLL.Repository<Aluno>().Find(x => x.alunoID == 1);
 
// get all
var findAll = UnityOWBLL.Repository<Aluno>().FindAll();
```



#### Call Stored Procedures

```csharp
// initialize commands 
IExecuteCommands executeComands = UnityOWBLL.ExecuteCommands();
```


#### Void Stored Procedure with parameters

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
 
// initialize commands 
IExecuteCommands executeComands = UnityOWBLL.ExecuteCommands();
  
// set stored parameters
var paramters = new List<Parameters>();
  
// add params
paramters.Add(new Parameters { ParameterName = "@nome", Value = "Math" });   
 
// call stored procedure
executeComands.ExecuteStoredProcedure(StoredProcedureName: "InsertDisciplina",ParameterList: paramters);
```



#### Return Stored Procedure 

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
// initialize commands 
IExecuteCommands executeComands = UnityOWBLL.ExecuteCommands();
 
// get data from storedProcedure
var data = executeComands.ExecuteStoredProcedure<Aluno>(StoredProcedureName: "TodosAlunos");
```



## Extend Repository Base

This option provide a way to extend a base repository to your class managers( in case that you need to create your own methods upon existing context).

#### Create your own method contracts

 ```c#
public interface IAluno
{
    // best studant
      string isBestAluno();
 }
 ```

#### You can create your own method contracts Implementation by 2 ways

- Using Repository **instance** to access the base repository functions

```csharp
public class AlunoRepository : Contracts.IAluno
 {
 
       dynamic escolaContext;
 
       public UnityRepoContracts.Contracts.IRepository<Aluno> baseRepository { get; }
 
       public AlunoRepository(DAL.Context.escolaEntities context) 
       { 
           escolaContext = context;
           baseRepository = new UnityRepo.UnityRepo.Repository<Aluno>(context);  
       }
 
 
       public string isBest()
       {
           var alunoContext = ((IQueryable<Aluno>)escolaContext.Set<Aluno>());
            return alunoContext.FirstOrDefault(x => alunoContext.Max(y => y.nota) == x.nota).Nome;
       }
 
 
   }
```



- Using Repository **inheritance** to access the base repository functions



```csharp
 public class AlunoRepository : Factory.BaseRepository<Models.Data.Aluno>, Contracts.IAluno
    {

        private IQueryable<Aluno> AlunoRepo;

        public AlunoRepository(object context) : base(context)
        {

            AlunoRepo = context.BuildQuery<Models.Data.Aluno>();

        }

 		/// <summary>
        /// Builds the automatic query predicate [ Based on property values ].
        /// </summary>
        public IEnumerable<Aluno> GetAlunosbyAutomaticBuildPredicate(Aluno aluno)
        {

            // build dynamic predicate according to property values
            var predicate = Helpers.BuildPredicate(aluno);
            // return da
            return AlunoRepo.Where(predicate).Select(x => x);

        }

		/// <summary>
        /// Builds the manual query predicate [ Based on user definition values ].
        /// </summary>		
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
```

#### Base Repository:

```csharp
public class BaseRepository<T> : UnityRepoContracts.Contracts.IRepository<T> where T : class
   {
 
 
       private UnityRepo.UnityRepo.Repository<T> baseRepo { set; get; }
 
       public BaseRepository(object context)
       {
 
           baseRepo = new UnityRepo.UnityRepo.Repository<T>(context);
 
       }
 
 
       public void AddEntity(T entity)
       {
           baseRepo.AddEntity(entity);
       }
 
       public void AddEntityRange(List<T> entityList)
       {
           baseRepo.AddEntityRange(entityList);
       }
 
       public bool Any()
       {
           return baseRepo.Any();
       }
 
       public bool Any(Expression<Func<T, bool>> predicate)
       {
           return baseRepo.Any(predicate);
       }
 
       public int Count()
       {
           return baseRepo.Count();
       }
 
       public int Count(Expression<Func<T, bool>> predicate)
       {
           return baseRepo.Count(predicate);
       }
 
       public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
       {
           return baseRepo.Find(predicate);
       }
 
       public IEnumerable<T> FindAll()
       {
           return baseRepo.FindAll();
       }
 
       public T GetEntity(object id)
       {
           return baseRepo.GetEntity(id);
       }
 
       public IEnumerable<T> Pagination(int pageIndex, int pageSize = 10)
       {
           return baseRepo.Pagination(pageIndex, pageSize);
       }
 
       public void RemoveEntity(Expression<Func<T, bool>> predicate)
       {
           baseRepo.RemoveEntity(predicate);
       }
 
       public void RemoveEntity(T entity)
       {
           baseRepo.RemoveEntity(entity);
       }
 
       public void RemoveEntityRange(List<T> entityList)
       {
           baseRepo.RemoveEntityRange(entityList);
       }
   }
```



#### Extended Method Call

```csharp
// initialize unity
IUnitOfWork UnityOWBLL = new BLL.Factory.UnityInitializer().IUnity;
 
// Extend Repository
IAluno AlunoRepo = UnityOWBLL.CustomRepository<AlunoRepository>();
 
// get data
string bestStudante = AlunoRepo.isBestAluno();
```



#### Notes:

This library runs upon Entity Framework 6.0.0 to up.