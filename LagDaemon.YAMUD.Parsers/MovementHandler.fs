namespace LagDaemon.YAMUD.Parsers

open UserCommandParser
open LagDaemon.YAMUD.Model.Map


module MovementHandler =

    open LagDaemon.YAMUD.API.Services
    open LagDaemon.YAMUD.Model.User

    let move (address: RoomAddress) (direction : Direction) =
        match direction with
        | North -> address.Y <- address.Y + 1
        | South -> address.Y <- address.Y - 1
        | East -> address.X <- address.X + 1
        | West -> address.X <- address.X - 1
        | Up -> address.Level <- address.Level + 1
        | Down -> address.Level <- address.Level - 1

        address

    let ExecuteCommand (address: RoomAddress) (cmd: string) =
        let onSuccess (cmd: Command) = 
            match cmd with
            | Movement direction -> 
                let adr = move address direction
                "success"
            | _ -> "not implemented"
            
        let onFail (errMsg: string) = 
            sprintf "Failure: %s" errMsg

        let parser = new CommandParserDriver(onSuccess, onFail)
        parser.parse(cmd)


