--
-- File generated with SQLiteStudio v3.1.0 on Sun Mar 12 12:07:44 2017
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Ticket
CREATE TABLE Ticket (
    Id          INTEGER  NOT NULL,
    Name        VARCHAR  NOT NULL,
    TicketType  VARCHAR  NOT NULL,
    Status      VARCHAR  NOT NULL,
    Description TEXT     NOT NULL,
    CreatedOn   DATETIME NOT NULL,
    CreatedBy   BIGINT,
    ModifiedOn  DATETIME,
    ModifiedBy  BIGINT,
    CONSTRAINT sqlite_master_PK_Ticket PRIMARY KEY (
        Id
    )
);


-- Table: Resident
CREATE TABLE Resident (
    Id         INTEGER  NOT NULL,
    Firstname  TEXT,
    Lastname   TEXT,
    Notes      TEXT,
    CreatedBy  BIGINT,
    CreatedOn  DATETIME,
    ModifiedBy BIGINT,
    ModifiedOn DATETIME,
    UIRoomId   BIGINT,
    Birthdate  DATE,
    Gender     STRING,
    CONSTRAINT sqlite_master_PK_Resident PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        UIRoomId
    )
    REFERENCES UIRoom (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION
);


-- Table: UIRoom
CREATE TABLE UIRoom (
    Id          INTEGER  NOT NULL,
    Name        TEXT     NOT NULL,
    Description TEXT,
    SortOrder   BIGINT,
    IsHidden    BIT,
    CreatedOn   DATETIME,
    CreatedBy   BIGINT,
    ModifiedOn  DATETIME,
    ModifiedBy  BIGINT,
    SourceRoom  TEXT,
    CONSTRAINT sqlite_master_PK_UIRoom PRIMARY KEY (
        Id
    )
);


-- Table: User
CREATE TABLE User (
    Id         INTEGER  NOT NULL,
    Username   TEXT     NOT NULL,
    Password   TEXT     NOT NULL,
    CreatedOn  DATETIME,
    CreatedBy  BIGINT,
    ModifiedOn DATETIME,
    ModifiedBy BIGINT,
    Email      TEXT,
    CONSTRAINT sqlite_master_PK_User PRIMARY KEY (
        Id
    )
);


-- Table: UIRoomDevice
CREATE TABLE UIRoomDevice (
    Id          INTEGER  NOT NULL,
    UIRoomId    BIGINT   NOT NULL,
    DeviceId    BIGINT   NOT NULL,
    SortOrder   BIGINT,
    DisplayText TEXT,
    CreatedOn   DATETIME,
    CreatedBy   BIGINT,
    ModifiedOn  DATETIME,
    ModifiedBy  BIGINT,
    CONSTRAINT sqlite_master_PK_UIRoomDevice PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        DeviceId
    )
    REFERENCES Device (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION,
    FOREIGN KEY (
        UIRoomId
    )
    REFERENCES UIRoom (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION
);


-- Table: SchemaVersions
CREATE TABLE SchemaVersions (
    SchemaVersionID INTEGER  CONSTRAINT PK_SchemaVersions_SchemaVersionID PRIMARY KEY AUTOINCREMENT
                             NOT NULL,
    ScriptName      TEXT     NOT NULL,
    Applied         DATETIME NOT NULL
);


-- Table: Activity
CREATE TABLE Activity (
    Id         INTEGER  NOT NULL,
    TicketId   BIGINT,
    Comment    TEXT     NOT NULL,
    CreatedOn  DATETIME NOT NULL,
    CreatedBy  BIGINT   NOT NULL,
    ModifiedOn DATETIME,
    ModifiedBy BIGINT,
    CONSTRAINT sqlite_master_PK_Activity PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        TicketId
    )
    REFERENCES Ticket (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION
);


-- Table: HSDevice
CREATE TABLE HSDevice (
    Name             TEXT    NOT NULL,
    Ref              BIGINT  NOT NULL,
    Location         TEXT,
    Location2        TEXT,
    Value            TEXT,
    Status           TEXT,
    HideFromView     BIGINT,
    DeviceTypeString TEXT,
    LastChange       TEXT,
    Relationship     BIGINT,
    DeviceType       TEXT,
    DeviceImage      TEXT,
    UserNote         TEXT,
    UserAccess       TEXT,
    StatusImage      TEXT,
    Id               INTEGER NOT NULL,
    CONSTRAINT sqlite_master_PK_HSDevice PRIMARY KEY (
        Id
    )
);


-- Table: DeviceControl
CREATE TABLE DeviceControl (
    Id                    INTEGER PRIMARY KEY AUTOINCREMENT,
    DeviceId              INTEGER REFERENCES Device (Id),
    DoUpdate              BOOLEAN,
    SingleRangeEntry      BOOLEAN,
    ButtonType            INTEGER,
    ButtonCustom          TEXT,
    CCIndex               INTEGER,
    Range                 TEXT,
    Label                 TEXT,
    ControlType           INTEGER,
    ControlValue          TEXT,
    ControlString         TEXT,
    ControlStringList     TEXT,
    ControlStringSelected TEXT,
    ControlFlag           BOOLEAN
);


-- Table: Device
CREATE TABLE Device (
    Id           INTEGER NOT NULL,
    ReferenceId  BIGINT  NOT NULL,
    Name         TEXT    NOT NULL,
    Status       TEXT,
    CreatedOn    TEXT,
    CreatedBy    BIGINT,
    ModifiedOn   TEXT,
    ModifiedBy   BIGINT,
    Value        TEXT,
    HideFromView BIGINT,
    TileGroup    TEXT,
    TileSize     BIGINT,
    TileImage    TEXT,
    CONSTRAINT sqlite_master_PK_Device PRIMARY KEY (
        Id
    )
);


-- Table: Event
CREATE TABLE Event (
    Id         INTEGER  NOT NULL,
    DeviceId   BIGINT,
    OccurredOn DATETIME NOT NULL,
    NewStatus  TEXT     NOT NULL,
    OldStatus  TEXT     NOT NULL,
    Notes      TEXT,
    CreatedOn  DATETIME,
    CreatedBy  BIGINT,
    ModifiedOn DATETIME,
    ModifiedBy BIGINT,
    CONSTRAINT sqlite_master_PK_Event PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        DeviceId
    )
    REFERENCES Device (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION
);


-- Table: EventTicket
CREATE TABLE EventTicket (
    Id       INTEGER NOT NULL,
    EventId  BIGINT  NOT NULL,
    TicketId BIGINT  NOT NULL,
    CONSTRAINT sqlite_master_PK_EventTicket PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        TicketId
    )
    REFERENCES Ticket (Id) ON DELETE NO ACTION
                           ON UPDATE NO ACTION,
    FOREIGN KEY (
        EventId
    )
    REFERENCES Event (Id) ON DELETE NO ACTION
                          ON UPDATE NO ACTION
);


-- Index: index_User_1_User
CREATE UNIQUE INDEX index_User_1_User ON User (
    Username ASC
);


-- Index: index_User_2_User
CREATE UNIQUE INDEX index_User_2_User ON User (
    Email ASC
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
