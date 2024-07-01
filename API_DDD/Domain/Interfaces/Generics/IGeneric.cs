namespace Domain.Interfaces.Generics
{
	public interface IGeneric<T> where T : class
	{
		Task Add(T Objeto);
		Task Update(T Objeto);
		Task Remove(T Objeto);
		Task<T> GetEntityById(int Id);
		Task<List<T>> List();
	}
}
