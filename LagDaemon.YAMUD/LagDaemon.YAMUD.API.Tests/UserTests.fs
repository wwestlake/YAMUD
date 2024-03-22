module Tests

open Xunit
open LagDaemon.YAMUD.API.User
open LagDaemon.YAMUD.DataModel.User

[<Theory>]
[<InlineData(10.0, "password123")>]
[<InlineData(20.0, "My$tr0ngP@ssw0rd")>]
let ``createPassword calculates entropy and designates a pw as invalid if entropy below spec`` (minEntropy: float) (testPassword: string) =
    let checkPassword = createPassword minEntropy testPassword
    match checkPassword with
    | ValidPassword (Password pw, Entropy entropy) -> Assert.True(entropy >= minEntropy  && pw = testPassword)
    | InvalidPassword (Password pw, Entropy entropy) -> Assert.True(entropy < minEntropy && pw = testPassword)

[<Theory>]
[<InlineData("john.doe@example.com")>]
[<InlineData("jane_smith123@example.co.uk")>]
[<InlineData("alice-123@example.net")>]
[<InlineData("bob+smith@example.org")>]
[<InlineData("info@subdomain.example.com")>]
[<InlineData("user@email.example")>]

[<InlineData("not_an_email_address")>]
[<InlineData("@missinglocalpart.com")>]
[<InlineData("missingdomainpart@")>]
[<InlineData("john.doe@.com")>]
[<InlineData("jane@invalid..net")>]
[<InlineData("bob@no_domain_part.")>]
let ``createEmailAddress produces valid and rejected emails based on format`` (email: string) =
    let checkEmail = createEmailAddress email
    match validateEmailAddress checkEmail with
    | Some (ValidEmailAddress (EmailAddress address)) -> Assert.Equal(email, address)
    | Some (RejectedEmailAddress (EmailAddress address, _)) -> Assert.Equal(email, address)
    | _ -> Assert.Fail($"Invalid email address {email}")

