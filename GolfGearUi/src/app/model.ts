export interface IGolfClub {
    make: string;
    model: string;
    type: ClubType;
    loft: number;
}

export enum ClubType {
    Unknown = 0,
    Driver = 1,
    Iron = 2,
    Wedge = 3,
    Putter = 4
}

export interface IShaft {
    make: string;
    model: string;
    type: ShaftType;
    weight: number;
    flex: Flex;
}

export enum Flex {
    Unknown = 0,
    Light = 1,
    Regular = 2,
    Stiff = 3,
    ExtraStiff = 4
}

export enum ShaftType {
    Unknown = 0,
    Steel = 1,
    Graphite = 2
}