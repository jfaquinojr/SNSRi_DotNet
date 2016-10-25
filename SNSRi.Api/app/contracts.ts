module Data.Contracts {

    export class Room {
        id: number;
        name: string;
        description: string;
        sortOrder: number;
        isHidden: boolean;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        devices: Device[];
    }

    export class User {
        id: number;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        username: string;
        password: string;
        email: string;
    }


    export class Ticket {
        id: number;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        name: string;
        ticketType: string;
        status: string;
        description: string;
        events: Event[];
    }

    export class Device {
        id: number;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        name: string;
        referenceId: number;
        status: string;
        value: string;
        hideFromView: boolean;
    }

    export class Activity {
        id: number;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        ticketId: string;
        comment: string;
        room: Room;
    }

    export class Event {
        id: number;
        createdOn: any;
        createdBy: number;
        modifiedOn: any;
        modifiedBy: number;
        deviceId: number;
        occurredOn: any;
        newStatus: string;
        oldStatus: string;
        notes: string;
        ticket: Ticket;
    }
}