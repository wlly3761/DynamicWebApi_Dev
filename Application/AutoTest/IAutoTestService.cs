using ApplicationCommon;

namespace Application.AutoTest
{
    
    public interface IAutoTestService
    {
        IEnumerable<int> Getx(int value);
        int PostTest(string value);
    }
}
