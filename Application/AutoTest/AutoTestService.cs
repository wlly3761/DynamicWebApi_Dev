using Application.User;
using ApplicationCommon;
using Microsoft.AspNetCore.Mvc;

namespace Application.AutoTest;

[DynamicApiInterface]
public class AutoTestService:IAutoTestService
{
    private readonly IUserService _userService;
    public AutoTestService(IUserService userService)
    {
        _userService=userService;
    }
    [HttpGet]
    public IEnumerable<int> Getx(int value)
    {
        yield return value;
        yield return 3;
    }
    [HttpPost]
    public int PostTest(string value)
    {
        return Convert.ToInt32(value);
    }
    [HttpGet]
    public string GetUserName(string value)
    {
      return  _userService.GetUserName(value);
    }
    [HttpGet]
    public string GetStaticUserName()
    {
        return _userService.UserName;
    }
}
