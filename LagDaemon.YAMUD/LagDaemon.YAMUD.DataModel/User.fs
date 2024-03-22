namespace LagDaemon.YAMUD.DataModel

open System

module User =

    type ErrorMessage = ErrorMessage of string
    type Password = Password of string
    type Entropy = Entropy of float
    type Duration = Duration of TimeSpan
    type ActionDate = ActionDate of DateTime

    type PasswordState =
        | ValidPassword of Password * Entropy
        | InvalidPassword of Password * Entropy

    type EmailAddress = EmailAddress of string
    type EmailAddressState =
        | RawEmailAddress of EmailAddress
        | ValidEmailAddress of EmailAddress
        | InvalidEmailAddress of EmailAddress
        | VerifiedEmailAddress of EmailAddress * ActionDate
        | RejectedEmailAddress of EmailAddress * ErrorMessage
        | BlockedEmailAddress of EmailAddress * ErrorMessage * Duration


    type User = {
        ID: Guid;
        DisplayName: string;
        EmailAddress: EmailAddressState;
        ValidationToken: string option;
        Metadata: string option;
    }

