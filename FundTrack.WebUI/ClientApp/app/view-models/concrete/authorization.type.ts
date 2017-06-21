/**
 * model which used when return authorize user
 */
export class AuthorizationType {
    constructor(
        public access_token: string,
        public login: string,
        public id: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public address: string,
        public photoUrl:string,
        public errorMessage:string
    )
    { }
}
