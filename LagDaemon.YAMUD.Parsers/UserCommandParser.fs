namespace LagDaemon.YAMUD.Parsers

module UserCommandParser =
    open FParsec

    //type UserState = unit // doesn't have to be unit, of course
    type Parser<'t> = Parser<'t, unit>

    type Identifier = Identifier of string
    type Message = Message of string

    type InformationType =
        | ID
        | Name
        | Description
        | Metadata
        | Type

    type ActorType = 
        | Player of Identifier * InformationType
        | Item of Identifier * InformationType
        | Weapon of Identifier * InformationType
        | Food of Identifier * InformationType
        | Drink of Identifier * InformationType
        | Npc of Identifier * InformationType
        | Room of Identifier * InformationType


    type Direction =
        | North
        | South
        | East
        | West
        | Up
        | Down

    type InteractionType =
        | Look of Identifier
        | Examine of Identifier
        | Take of Identifier
        | Drop of Identifier
        | Inventory of Identifier
        | Equip of Identifier
        | Unequiip of Identifier

    type CommunicationType =
        | Say  of ActorType * Message
        | Tell of ActorType * Message
        | Shout of ActorType * Message
        | Emote of ActorType * Message

    type CombatType =
        | Attack
        | Cast
        | Use

    type MiscellaneousType =
        | Help
        | Quit
        | Save
        | Stats

    type AdminCommandType =
        | Create
        | Destroy
        | Kick
        | Ban
        | Rename
        | Execute


    type Functions =
        | Get of ActorType

    type Command =
        | Movement of Direction
        | Interaction of InteractionType
        | Communication of CommunicationType
        | Combat of CombatType
        | Miscellaneous of MiscellaneousType
        | AdminCommand of AdminCommandType

    
    let ws = spaces
    let str s = pstring s
    let str_r s = stringReturn s
    let str_ws s = pstring s .>> ws

    let direction : Parser<Direction> = choice [ 
        str_r "north" North
        str_r "south" South
        str_r "east"  East
        str_r "west"  West 
     ]

    let goCommand : Parser<_> = str_ws "go" >>. direction |>> Movement 

    let commandParser = choice [
        goCommand
    ]


    let parseCommand (parser : Parser<Command>)   (input : string) =
        match run parser input with
        | Success(result, _, _) -> Some result
        | Failure(errorMsg, _, _) -> 
            printfn "Parsing failed: %s" errorMsg
            None

    let parse = parseCommand commandParser


    type CommandParserDriver (execute: Command -> string, onfail: string -> string) =
        member _.parse(cmd: string) = 
            match parse cmd with
            | Some x -> execute x 
            | None -> onfail "Parse error" 


    

    let ParseCommand (cmd : string)  (onSuccess: Command -> string) (onFail: string -> string)  : string = 
        let drive = new CommandParserDriver(onSuccess, onFail)
        drive.parse(cmd)
        
