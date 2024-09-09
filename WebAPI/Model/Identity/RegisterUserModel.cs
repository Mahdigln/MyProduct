namespace WebAPI.Model.Identity;

public sealed class RegisterUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

//public sealed class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
//{
//    public RegisterUserModelValidator()
//    {
//        RuleFor(x => x.FirstName)
//            .NotEmpty().WithMessage("نام الزامی است.")
//            .Length(2, 25).WithMessage("نام باید بین 2 تا 50 کاراکتر باشد.");

//        RuleFor(x => x.LastName)
//            .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
//            .Length(2, 25).WithMessage("نام خانوادگی باید بین 2 تا 50 کاراکتر باشد.");

//        RuleFor(x => x.Username)
//            .NotEmpty().WithMessage("نام کاربری الزامی است.")
//            .Length(5, 20).WithMessage("نام کاربری باید بین 5 تا 20 کاراکتر باشد.")
//            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("نام کاربری فقط می‌تواند شامل حروف و اعداد باشد.");

//        RuleFor(x => x.Password)
//            .NotEmpty().WithMessage("کلمه عبور الزامی است.")
//            .MinimumLength(8).WithMessage("کلمه عبور باید حداقل 8 کاراکتر باشد.")
//            .Matches("[A-Z]").WithMessage("کلمه عبور باید حداقل شامل یک حرف بزرگ باشد.")
//            .Matches("[a-z]").WithMessage("کلمه عبور باید حداقل شامل یک حرف کوچک باشد.")
//            .Matches("[0-9]").WithMessage("کلمه عبور باید حداقل شامل یک عدد باشد.")
//            .Matches("[^a-zA-Z0-9]").WithMessage("کلمه عبور باید حداقل شامل یک کاراکتر خاص باشد.");

//        RuleFor(x => x.Email)
//            .NotEmpty().WithMessage("ایمیل الزامی است.")
//            .EmailAddress().WithMessage("ایمیل باید معتبر باشد.");
//    }
//}
