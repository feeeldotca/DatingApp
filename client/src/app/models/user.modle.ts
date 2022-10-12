import { Byte } from "@angular/compiler/src/util";

export interface User {
    id: number;
    userName: string;
    passwordHash: Byte[];
    passwordSalt: Byte[];
}