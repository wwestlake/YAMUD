namespace LagDaemon.YAMUD.API

open System
open System.Text.RegularExpressions

module User =
    open LagDaemon.YAMUD.DataModel.User

    let hello name =
        printfn "Hello %s" name

    let createPassword (minEntropy: float) (pw: string) =
        let characterSetSize = 95.0  // Assuming ASCII printable characters
        let passwordLength = float pw.Length
        let entropyPerCharacter = Math.Log(characterSetSize, 2.0)
        let entropy = passwordLength * entropyPerCharacter
        if entropy > minEntropy then  
            (pw |> Password, entropy |> Entropy) |>  PasswordState.ValidPassword 
        else
            (pw |> Password, entropy |> Entropy) |>  PasswordState.InvalidPassword 


    let createEmailAddress (address: string) =
        address |> EmailAddress |> RawEmailAddress

    let validateEmailAddress (state: EmailAddressState) =
        let isValidEmail (email: string) =
            let emailRegex = 
                Regex("^\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Z|a-z]{2,}\\b$", 
                RegexOptions.Compiled ||| RegexOptions.IgnoreCase)
            emailRegex.IsMatch(email)
        match state with
        | RawEmailAddress (EmailAddress address) -> 
                match isValidEmail address with
                | true -> address |> EmailAddress |> ValidEmailAddress |> Some
                | false -> RejectedEmailAddress (address |> EmailAddress, "Invalid address format" |> ErrorMessage) |> Some
        | _ -> None



