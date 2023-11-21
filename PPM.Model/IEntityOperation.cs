using ConsoleTables;
namespace IEntityOperation
{
    public interface IEntity<T>
    {
         void Add(T entity);
         ConsoleTable ListAll();
         ConsoleTable ListById(int entity);
         bool DeleteById(int entity);

    }
}
