namespace LibrarySystem.Domain.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository AuthorRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IBookRepository BookRepository { get; }

        Task SaveChangesAsync();
    }
}
