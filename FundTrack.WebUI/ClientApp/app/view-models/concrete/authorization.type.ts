/**
 * model which used when return authorize user
 */
export class AuthorizeUserModel {
    constructor(
        public login: string,
        public id: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public address: string,
        public photoUrl: string,
        public role: string
    ) { }
}
export class AuthorizationType {
    constructor(
        public userModel: AuthorizeUserModel,
        public access_token: string,
        public errorMessage: string
    )
    { }
}
