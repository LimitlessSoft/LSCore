# LSCore
### Free and open-source .NET Api framework

A .NET libraries which makes building your API faster and easier.
Using DI to implement managers, mappers, validators, converts and others for the faster building process.

To effectively use this framework, follow your project infrastructure as described bellow

For whole Framework to work, install LSCore.Contracts, LSCore.Domain, LSCore.Framework and LSCore.Repository (if you have database)

## Api structure
Base structure of API using this framework should contains 4 projects.
 - YourProject.Api (Web Api Project)
 - YourProject.Contracts (Class Library)
 - YourProject.Domain (Class Library)
 - YourProject.Repository (Class Library, if you are communicating with database)
 
### .Api 
Used to build controllers with endpoints which will call implementations from class library.
Should not contain any logic inside it.
This project must reference:
`LSCore.Framework`
`YourProject.Domain`
`YourProject.Repository`

Important: If you do not reference both `Domain` and `Repository` proejcts, Dependency Injection will not find Managers!
Important: Endpoints inside your controllers should only be used to catch request and call managers method which contains all the logic, valiadtions, request handling and building resposne.

Only folder you should have in this project is `Controllers` which contains your controllers.

### .Contracts
Used to declare entity classes, enumerators, manager interfaces, dtos, constants, static classes, dto mappers etc.
This project must reference:
`LSCore.Contracts`

Important: Do not implement any logic here. Classes should be only pure declaration of properties!

Common folders you should have in this project are:
- DtoMappings
- Dtos
- Entities
- Enums
- Helpers
- IManagers
- Requests

and a `Constants.cs` which is used to store all constants used for this API

### .Domain
Used to declare all the logic and implementations in your project.
This project must reference:
`LSCore.Domain`
`YourProject.Contracts`
`YourProject.Repository` (if you have database)

Common folders you should have in this project are:
- Managers
- Validators

### .Repository
Used to implement everything regarding your database. If your API do not include communication with database, you do not need this project.
This proejct must reference:
`LSCore.Repository`

Common folders you should have in this project are:
- DbMappings
- Queries
- Commands
- Filters

and single `[YourDatabase]DbContext.cs` file used to declare database structure.

# Features
- Responses
- Managers (base & database)
- Request Validators

## Responses
Each API endpoint should return some value. You should always return one of these objects:
- LSCoreResponse
- LSCoreResponse<TPayload>
- LSCoreListResponse<TPayload>
- LSCorePaginatedResponse<TPayload> (To be implemented)

All the responses have this JSON structure:
```
{
   status,
   notOk,
   payload,
   errors
}
```

General rule is your API will always return either status 200 or 500 (Internal Server Error) (or other like 404 if route not found or other 500 error). If it returns 200, that means your API did process client application request and then front end application can parse response in structure described above and check `status` to see exact status of that response (200, 201, 400, 404...)

**LSCoreResponse** is used to return response without any `payload` value. It is used to return just simple plain status.
If request is successfull, in inner status you will return 200. If request does not pass validation or you do not want to proceede with request because of some logic annomally, you will return 400 (validation automatically does this for you) with some error messages (see usage of responses bellow).

**LSCoreResponse<TPayload>** is used same way as `LSCoreResponse` however you will use this if you want to return something inside `payload` property.

**LSCoreListResponse<TPayload>** is used same way as `LSCoreResponse<TPayload>`, however if you are returning list of objects, do not return `LSCoreResponse<List<Class>>` but `LSCoreListResponse<class>`.

Example of method which builds response:
```
public LSCoreResponse<string> ToUppercase(StringRequest request)
{
    // This create response object with default properties values like this:
	// status: 200
	// notOk: false,
	// payload: null,
	// errors: null
    var response = new LSCoreResponse<string>();
	
	// This line validates request.
	// Validator is created in separate place (will be covered later) and Automatically
	// Scanned and implemented, so no need for additional code except this.
	// If validation fails, response will be changed like this:
	// status: 200 > 400
	// notOk: false > true
	// errors: new List<string>()
	// {
	//     "Message we declared in validator",
	//     "Message number 2 if more than one validation failed",
	//     ...
	// }
	if(request.IsInvalid(response))
		return response;
		
	// This lanes populate Payload inside response with our logic 
	response.Payload = request.Value.ToString();
	
	return response;
}
```

In controller, implementation would look like this:

```
[HttpGet]
[Route("/string-to-uppercase")]
public LSCoreResponse<string> GetToUppercase(StringRequest request)
{
    return _stringManager.ToUppercase(request);
}
```

All endpoints body should look like this. Only call to managers method.

## Managers
Managers are used to implement logic which will be used by controllers.

To create valid manager you need to do two things.
First is to create interface for the manager inside `.Contracts.IManagers` folder
Second is to create implementation of the manager inside `.Domain.Managers` which inherits the interface.

```
// Location .Contracts.IManagers
public interface IUserManager
{
    LSResponse Logout();
}

// Location .Domain.managers
public class UserManager : LSCoreBaseManager<UserManager>, IUserManager
{
    public UserManager(ILogger<UserManager> logger)
		: base(logger)
	{
	}
		
    public LSCoreResponse Logout()
	{
	    // Implementation
	}
}
```

Important thing is naming convention of manager and its interface. If you create `UserManager`, interface must be same name prefixed with `I` > `IUserManager`. If it differs by 1 letter, Dependency Injection will not automatically work.

Manager will be automatically picked up and available using DI, so to use it simple catch its interface in constructor like this:

```
public class UsersController : ControllerBase
{
	
	private readonly IUserManager _userManager;
	
    public UsersController(IUserManager userManager)
	{
		_userManager = userManager;
	}
	
	[HttpGet]
	[Route("/logout")]
	public LSCoreResponse Logout()
	{
	    return _userManager.Logout();
	}
}
```

If you want to implement manager which communicates with database table, instead of `LSBaseManager` use `LSBaseTableManager` like so:
```
public class UserManager : LSCoreBaseTableManager<UserManager, UserEntity>, IUserManager
{
    public UserManager(ILogger<UserManager> logger, YourDatabaseDbContext dbContext) // dbContextClass is declared inside `.Repository.YourDatabaseDbContext.cs`
		: base(logger, dbContext)
	{
	}
		
    public LSCoreResponse Logout()
	{
	    // Implementation
	}
}
```

Both `BaseManager` and `BaseTableManager` (if you want `BaseManager` to access it, you need to catch DbContext and pass it to `:base()` just like it is done in `BaseTableManager`) and they have similiar methods.
Difference is when using `BaseTableManager` we do not need to specify entity we are working with, since we already declared it when declaring class as second type parameter `: LSCoreBaseTableManager<ManagerClass, EntityClass>.

Bellow is example of some methods available in managers. Non-generic methods are available only to `BaseTableManager` while others are alvailable to both.

```
public class UserManager : LSCoreBaseTableManager<UserManager, UserEntity>, IUserManager
{
    public UserManager(ILogger<UserManager> logger, YourDatabaseDbContext dbContext)
		: base(logger, dbContext)
	{
	}
		
    public LSCoreResponse Logout()
	{
	    // Implementation
	}
	
	public LSCoreResponse<UserDto> Get(IdRequest request) // IdRequest is part of LSCore
	{
		var user = FirstOrDefault(x => x.Id == request.Id); // Method from BaseTableManager
		
		if(user == null)
			return LSCoreResponse<UserDto>.NotFound(); // Static metod from LSCoreResponse
			
		var blockedUser = FirstOrDefault<BlockedUser>(x => x.Id == request.Id); // Generic FirstOrDefault method available in both managers and will work on table specified in generic type.
		if(blockedUser == null) // Selected user is not found in BlockedUser table, we will return it
			return new LSCoreResponse<UserDto>(user.ToDto()); // .ToDto(this UserEntity userEntity) is extension you need to declare in .Contracts.DtoMappings.UserDtoMappings.cs static class
			
		// If came here, then user is found inside BlockedUser
		return LSCoreResponse<UserDto>.BadRequest(string.format(UsersValidationCodes.UVC_001, blockedUser.Reason)); // this will return inner status of 400, with message = new List<string>() { "This user is blocked for the reasion: {0}" }
	}
}
```


## Requests validation
To validate request use `.IsInvalid(responseBuffer)` extension which rules are using FluentValidation.
This extensions work on classes which inherit `ILSCoreRequest` interface, so make sure your request class does so.
To create validator for your request do this:

First create request class inside `.Contracts.Requests` like so:
```
public class StringRequest : ILSCoreRequest
{
    public string Value { get; set; }
}
```

Then create `StringRequestValidator` class inside `.Domain.Validators` like so:

```
// Your class must inherit `LSCoreValidatorBase<TRequestClassToValidate>`
public class StringRequestValidator : LSCoreValidatorBase<StringRequest>
{
    public StringRequestValidator() : base()
	{
	    RuleFor(x => x.Value)
		    .NotEmpty()
			.MinLength(Constants.StringRequestValueMinLength) // Constants class is located in ProjectName.Contracts.Constants.cs
			.WithMessage(string.Format(StringsValidationCodes.SRV_001.GetDescription(), nameof(StringRequest.Value), Constant.StringRequestMinLength)); // This message will be used only for first validator before.
	}
}
```

And then call `IsInvalid(response)` where you want to validate request like so:
```
public class StringManager : LSCoreBaseManager<StringManager>, IStringManager
{

	// ...

    public LSCoreResponse<string> ToLowercase(StringRequest request)
	{
		var response = new LSCoreResponse<string>();
		
		if(request.IsInvalid(response))
			return response;
			
	    // Implementation
	}
}
```

Inside validator we called line `.WithMessage(StringsValidationCodes.SRV_001.GetDescription())`.
This is enum which you sould create inside `ProjectName.Contracts.Enums.ValidationCodes.StringsValidationCodes`  and should look like this:
```
public partial class ValidationCodes // All validations class should share same class
{
	public enum StringsValidationCodes
	{
		[Description("Property '{0}' must have length greater than '{1}'")]
	    SRV_001,
	}
}
```

Note: Fluent validation have good handling with default messages and for the `MinLength` we did not need to declare custom message since it handles it good, however it is just used for example purpose.

// TODO: More documentation