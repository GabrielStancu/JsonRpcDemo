namespace RpcService
{
    public interface IPersonService
    {
        bool Add(PersonDTO person);
        void Delete(int id);
        PersonDTO Get(int id);
        System.Collections.Generic.IEnumerable<PersonDTO> GetAll();
    }
}