import { Byte } from "@angular/compiler/src/util";

export interface Users {
    id: number;
    userName: string;
    passwordHash: Byte[];
    passwordSalt: Byte[];
}