

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBarcodeGeneratorService
    {
        string GenerateShortBarcode(string prefix = "BC");
    }
}
