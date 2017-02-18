module Data.Contracts {

    export class Room {
        Id: number;
        Name: string;
        Description: string;
        SortOrder: number;
        IsHidden: boolean;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        Devices: Device[];
    }

    export class User {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        Username: string;
        Password: string;
        Email: string;
    }


    export class Ticket {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        Name: string;
        TicketType: string;
        Status: string;
        Description: string;
        Events: Event[];
    }

    export class Device {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        Name: string;
        ReferenceId: number;
        Status: string;
        Value: string;
        HideFromView: boolean;
        TileGroup: string;
        TileSize: number;
        TileImage: string;
    }

    export class Activity {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        TicketId: number;
        Comment: string;
        Room: Room;
    }

    export class Event {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        DeviceId: number;
        OccurredOn: any;
        NewStatus: string;
        OldStatus: string;
        Notes: string;
        Ticket: Ticket;
    }

    export class UIRoomDevice {
        Id: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        UIRoomId: number;
        DeviceId: number;
        SortOrder: number;
        DisplayText: string;
    }
}