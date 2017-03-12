CREATE TABLE Activity
(
    Id INTEGER NOT NULL,
    TicketId INTEGER,
    Comment TEXT NOT NULL,
    CreatedOn TEXT NOT NULL,
    CreatedBy INTEGER NOT NULL,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    FOREIGN KEY (TicketId) REFERENCES Ticket (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE Device
(
    Id INTEGER NOT NULL,
    ReferenceId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    Status TEXT,
    CreatedOn TEXT,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    Value TEXT,
    HideFromView INTEGER,
    TileGroup TEXT,
    TileSize INTEGER,
    TileImage TEXT
);
CREATE TABLE DeviceControl
(
    Id INTEGER,
    DeviceId INTEGER,
    DoUpdate INTEGER,
    SingleRangeEntry INTEGER,
    ButtonType INTEGER,
    ButtonCustom TEXT,
    CCIndex INTEGER,
    Range TEXT,
    Label TEXT,
    ControlType INTEGER,
    ControlValue TEXT,
    ControlString TEXT,
    ControlStringList TEXT,
    ControlStringSelected TEXT,
    ControlFlag INTEGER,
    FOREIGN KEY (DeviceId) REFERENCES Device (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE Event
(
    Id INTEGER NOT NULL,
    DeviceId INTEGER,
    OccurredOn TEXT NOT NULL,
    NewStatus TEXT NOT NULL,
    OldStatus TEXT NOT NULL,
    Notes TEXT,
    CreatedOn TEXT,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    FOREIGN KEY (DeviceId) REFERENCES Device (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE EventTicket
(
    Id INTEGER NOT NULL,
    EventId INTEGER NOT NULL,
    TicketId INTEGER NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Event (Id) DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (TicketId) REFERENCES Ticket (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE HSDevice
(
    Name TEXT NOT NULL,
    Ref INTEGER NOT NULL,
    Location TEXT,
    Location2 TEXT,
    Value TEXT,
    Status TEXT,
    HideFromView INTEGER,
    DeviceTypeString TEXT,
    LastChange TEXT,
    Relationship INTEGER,
    DeviceType TEXT,
    DeviceImage TEXT,
    UserNote TEXT,
    UserAccess TEXT,
    StatusImage TEXT,
    Id INTEGER NOT NULL
);
CREATE TABLE Resident
(
    Id INTEGER NOT NULL,
    Firstname TEXT,
    Lastname TEXT,
    Notes TEXT,
    CreatedBy INTEGER,
    CreatedOn TEXT,
    ModifiedBy INTEGER,
    ModifiedOn TEXT,
    UIRoomId INTEGER,
    Birthdate TEXT,
    Gender TEXT,
    FOREIGN KEY (UIRoomId) REFERENCES UIRoom (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE Ticket
(
    Id INTEGER NOT NULL,
    Name TEXT NOT NULL,
    TicketType TEXT NOT NULL,
    Status TEXT NOT NULL,
    Description TEXT,
    CreatedOn TEXT NOT NULL,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    Severity TEXT
);
CREATE TABLE UIRoom
(
    Id INTEGER NOT NULL,
    Name TEXT NOT NULL,
    Description TEXT,
    SortOrder INTEGER,
    IsHidden TEXT,
    CreatedOn TEXT,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    SourceRoom TEXT
);
CREATE TABLE UIRoomDevice
(
    Id INTEGER NOT NULL,
    UIRoomId INTEGER NOT NULL,
    DeviceId INTEGER NOT NULL,
    SortOrder INTEGER,
    DisplayText TEXT,
    CreatedOn TEXT,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    FOREIGN KEY (UIRoomId) REFERENCES UIRoom (Id) DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (DeviceId) REFERENCES Device (Id) DEFERRABLE INITIALLY DEFERRED
);
CREATE TABLE User
(
    Id INTEGER NOT NULL,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL,
    CreatedOn TEXT,
    CreatedBy INTEGER,
    ModifiedOn TEXT,
    ModifiedBy INTEGER,
    Email TEXT
);
CREATE UNIQUE INDEX index_User_1_User ON User (Username);
CREATE UNIQUE INDEX index_User_2_User ON User (Email);
