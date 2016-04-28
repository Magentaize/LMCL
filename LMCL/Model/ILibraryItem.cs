namespace Magentaize.Net.LMCL.Model
{
    public interface ILibraryItem
    {
        LibraryItemName Name { get; }

        string Path { get; }

        string Url { get; }
    }
}